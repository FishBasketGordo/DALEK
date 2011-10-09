using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Creelio.Framework.Extensions;
using System.IO;

namespace DALEK.Generator
{
    internal class SqlInterpolator : Interpolator
    {
        #region Methods

        public override string GetOutputFileName(InterpolationArgs args)
        {
            if (args.TemplateName == "ExecuteScripts")
            {
                return "ExecuteAll.bat";
            }
            else
            {
                return GetProcedure(args);
            }
        }

        protected override void PopulatePlaceHolderDictionary(Dictionary<string, Func<InterpolationArgs, string>> dictionary)
        {
            dictionary.Add("Catalog"                    , (args) => GetCatalog(args)                               );
            dictionary.Add("Column"                     , (args) => GetColumn(args)                                );
            dictionary.Add("ColumnList"                 , (args) => GetList(args, GetColumnListItem)               );
            dictionary.Add("ColumnType"                 , (args) => GetColumnType(args)                            );
            dictionary.Add("ColumnWithTableAlias"       , (args) => GetColumnWithTableAlias(args)                  );
            dictionary.Add("ExecuteScriptCommandList"   , (args) => GetExecuteScriptCommandListItem(args)          );
            dictionary.Add("Parameter"                  , (args) => GetParameter(args)                             );
            dictionary.Add("ParameterList"              , (args) => GetList(args, GetParameterListItem)            );
            dictionary.Add("PrimaryKeyWhereClause"      , (args) => GetPrimaryKeyWhereClause(args)                 );
            dictionary.Add("Procedure"                  , (args) => GetProcedure(args)                             );
            dictionary.Add("QualifiedProcedure"         , (args) => GetQualifiedProcedure(args)                    );
            dictionary.Add("QualifiedTable"             , (args) => GetQualifiedTable(args)                        );
            dictionary.Add("QualifiedTableWithAlias"    , (args) => GetQualifiedTableWithAlias(args)               );
            dictionary.Add("Schema"                     , (args) => GetSchema(args)                                );
            dictionary.Add("Table"                      , (args) => GetTable(args)                                 );
            dictionary.Add("TableAlias"                 , (args) => GetTableAlias(args)                            );
            dictionary.Add("ValueList"                  , (args) => GetList(args, GetValueListItem)                );
            dictionary.Add("WhereClause"                , (args) => GetList(args, GetWhereClauseItem)              );
        }

        #region Catalog

        private static string GetCatalog(InterpolationArgs args)
        {
            return args.Columns[args.ColumnIndex]["TABLE_CATALOG"].ToString();
        } 
        
        #endregion

        #region Column

        private static string GetColumn(InterpolationArgs args)
        {
            return args.Column["COLUMN_NAME"].ToString();
        }

        #endregion

        #region Column List Item

        private static string GetColumnListItem(InterpolationArgs args)
        {
            if (string.Compare(args.TemplateName, "SELECT", true) == 0)
            {
                return string.Format(
                    "{1}{2}{3}{0}",
                    args.ColumnIndex == args.Columns.Count - 1 ? string.Empty : Environment.NewLine,
                    args.ColumnIndex == 0 ? string.Empty : args.IndentString,
                    args.ColumnIndex == 0 ? " " : ",",
                    GetColumnWithTableAlias(args)
                );
            }
            else if (string.Compare(args.TemplateName, "INSERT", true) == 0)
            {
                string column = GetColumn(args);
                string item = string.Format(
                    "{1}{2}[{3}]{0}",
                    args.IsLastNonIdentity(column) ? string.Empty : Environment.NewLine,
                    args.IsFirstNonIdentity(column) ? string.Empty : args.IndentString,
                    args.IsFirstNonIdentity(column) ? " " : ",",
                    column
                );

                return args.IsIdentity(column) ? string.Empty : item;
            }
            else if (string.Compare(args.TemplateName, "UPDATE", true) == 0)
            {
                string column = GetColumn(args);
                string item = string.Format(
                    "{1}{2}[{3}] = {4}{0}",
                    args.IsLastNonIdentity(column) ? string.Empty : Environment.NewLine,
                    args.IsFirstNonIdentity(column) ? string.Empty : args.IndentString,
                    args.IsFirstNonIdentity(column) ? " " : ",",
                    column,
                    GetParameter(args)
                );

                return args.IsIdentity(column) ? string.Empty : item;
            }
            else
            {
                return "??ColumnListItem??";
            }
        }

        #endregion

        #region Column Type

        private static string GetColumnType(InterpolationArgs args)
        {
            var dataType = args.Column["DATA_TYPE"].ToString().ToUpper();
            var sb = new StringBuilder(dataType);

            switch (dataType)
            {
                case "CHAR":
                case "VARCHAR":
                case "NCHAR":
                case "NVARCHAR":
                case "BINARY":
                    int length = Convert.ToInt32(args.Column["CHARACTER_MAXIMUM_LENGTH"]);
                    sb.AppendFormat("({0})", length == -1 ? "MAX" : length.ToString());
                    break;

                case "DECIMAL":
                case "NUMERIC":
                    int precision = Convert.ToInt32(args.Column["NUMERIC_PRECISION"]);
                    int scale = Convert.ToInt32(args.Column["NUMERIC_PRECISION"]);
                    sb.AppendFormat("({0},{1})", precision, scale);
                    break;

                default:
                    break;
            }

            return sb.ToString();
        }

        #endregion

        #region Column With Table Alias

        private static string GetColumnWithTableAlias(InterpolationArgs args)
        {
            return string.Format("{0}.[{1}]", GetTableAlias(args), GetColumn(args));
        }

        #endregion

        #region Execute Script Command List Item

        public string GetExecuteScriptCommandListItem(InterpolationArgs args)
        {
            StringBuilder sb = new StringBuilder();
            
            string dir = Path.GetDirectoryName(args.OutputFilePath);
            string[] files = Directory.GetFiles(dir, "*.sql");

            for (int ii = 0; ii < files.Length; ii++)
            {
                sb.AppendFormat(
                    "{1}sqlcmd -U {2} -P {3} -S {4} -i \"{5}\"{0}",
                    /*  0 */ Environment.NewLine,
                    /*  1 */ args.IndentString,
                    /*  2 */ args.ConnectionStringBuilder.UserID,
                    /*  3 */ args.ConnectionStringBuilder.Password,
                    /*  4 */ args.ConnectionStringBuilder.DataSource,
                    /*  5 */ files[ii]
                );
            }

            return sb.ToString();
        }

        #endregion

        #region Parameter

        private static string GetParameter(InterpolationArgs args)
        {
            return string.Format("@{0}", GetColumn(args));
        }

        #endregion

        #region Parameter List Item

        private static string GetParameterListItem(InterpolationArgs args)
        {
            string column = GetColumn(args);

            string conjunction, indent, endLine;
            GetParameterListItemPunc(args, column, out conjunction, out indent, out endLine);

            string item = string.Format(
                "{1}{2}{3} {4} = NULL{0}",
                /*  0 */ endLine,
                /*  1 */ indent,
                /*  2 */ conjunction,
                /*  2 */ GetParameter(args),
                /*  3 */ GetColumnType(args)
            );

            return GetParameterListItem(args, column, item);
        }

        private static void GetParameterListItemPunc(InterpolationArgs args, string column, out string conjunction, out string indent, out string endLine)
        {
            if (args.TemplateName == "DELETE")
            {
                conjunction = GetConjunction(args, dr => args.IsPrimaryKey(dr["COLUMN_NAME"].ToString()), string.Empty, " ", ",");
                indent = args.IsFirstPrimaryKey(column) ? string.Empty : args.IndentString;
                endLine = args.IsLastPrimaryKey(column) ? string.Empty : Environment.NewLine;
            }
            else if (args.TemplateName == "INSERT")
            {
                conjunction = GetConjunction(args, dr => !args.IsIdentity(dr["COLUMN_NAME"].ToString()), string.Empty, " ", ",");
                indent = args.IsFirstNonIdentity(column) ? string.Empty : args.IndentString;
                endLine = args.IsLastNonIdentity(column) ? string.Empty : Environment.NewLine;
            }
            else
            {
                conjunction = GetConjunction(args, string.Empty, " ", ",");
                indent = args.ColumnIndex == 0 ? string.Empty : args.IndentString;                
                endLine = args.ColumnIndex == args.Columns.Count - 1 ? string.Empty : Environment.NewLine;
            }
        }

        private static string GetParameterListItem(InterpolationArgs args, string column, string item)
        {
            if (args.TemplateName == "INSERT")
            {
                return args.IsIdentity(column) ? string.Empty : item;
            }
            else if (args.TemplateName == "DELETE")
            {
                return args.IsPrimaryKey(column) ? item : string.Empty;
            }
            else
            {
                return item;
            }
        }

        #endregion

        #region Primary Key Where Clause

        private static string GetPrimaryKeyWhereClause(InterpolationArgs args)
        {
            var pkArgs = new InterpolationArgs 
            { 
                TemplateName = args.TemplateName, 
                Columns = args.PrimaryKeys,
                IndentSpaces = args.IndentSpaces
            };
            return GetList(pkArgs, GetWhereClauseItem);
        }

        #endregion

        #region Procedure

        private static string GetProcedure(InterpolationArgs args)
        {
            return GetProcedure(args.Column["TABLE_NAME"].ToString(), args.TemplateName);
        }

        private static string GetProcedure(string tableName, string templateName)
        {
            return string.Format("{0}_{1}", tableName, templateName);
        }

        #endregion

        #region Qualified Procedure

        private static string GetQualifiedProcedure(InterpolationArgs args)
        {
            return string.Format("[{0}].[{1}]", GetSchema(args), GetProcedure(args));
        }

        #endregion

        #region Qualified Table

        private static string GetQualifiedTable(InterpolationArgs args)
        {
            return string.Format("[{0}].[{1}]", GetSchema(args), GetTable(args));
        }

        #endregion

        #region Qualified Table With Alias

        private static string GetQualifiedTableWithAlias(InterpolationArgs args)
        {
            return string.Format("{0} {1}", GetQualifiedTable(args), GetTableAlias(args));
        }

        #endregion

        #region Schema

        private static string GetSchema(InterpolationArgs args)
        {
            return args.Column["TABLE_SCHEMA"].ToString();
        }

        #endregion

        #region Table

        private static string GetTable(InterpolationArgs args)
        {
            return args.Column["TABLE_NAME"].ToString();
        }

        #endregion

        #region Table Alias

        private static string GetTableAlias(InterpolationArgs args)
        {
            return GetTable(args)[0].ToString().ToLower();
        }

        #endregion

        #region Value List Item

        private static string GetValueListItem(InterpolationArgs args)
        {
            string column = GetColumn(args);

            if (args.IsIdentity(column))
            {
                return string.Empty;
            }

            string conjunction = GetConjunction(args, dr => !args.IsIdentity(dr["COLUMN_NAME"].ToString()), string.Empty, " ", ",");
            string indent = args.IsFirstNonIdentity(column) ? string.Empty : args.IndentString;
            string endLine = args.IsLastNonIdentity(column) ? string.Empty : Environment.NewLine;

            return string.Format(
                "{1}{2}{3}{0}",
                /*  0 */ endLine,
                /*  1 */ indent,
                /*  2 */ conjunction,
                /*  3 */ GetParameter(args)
            );
        }

        #endregion

        #region Where Clause Item

        private static string GetWhereClauseItem(InterpolationArgs args)
        {
            if (args.TemplateName == "SELECT" || args.TemplateName == "COUNT")
            {
                string conjunction = GetConjunction(args, string.Empty, new string(' ', 4), "AND ");            

                return string.Format(
                    "{1}{2}({3} IS NULL OR {3} = {4}){0}",
                    args.ColumnIndex == args.Columns.Count - 1 ? string.Empty : Environment.NewLine,
                    args.ColumnIndex == 0 ? string.Empty : args.IndentString,
                    conjunction,
                    GetParameter(args),
                    GetColumnWithTableAlias(args)
                );
            }
            else if (args.TemplateName == "UPDATE")
            {
                string conjunction = GetConjunction(args, string.Empty, new string(' ', 4), "AND ");            

                return string.Format(
                    "{1}{2}({3} = {4}){0}",
                    args.ColumnIndex == args.Columns.Count - 1 ? string.Empty : Environment.NewLine,
                    args.ColumnIndex == 0 ? string.Empty : args.IndentString,
                    conjunction,
                    GetParameter(args),
                    GetColumn(args)
                );
            }
            else if (args.TemplateName == "DELETE")
            {
                string column = GetColumn(args);

                string conjunction = GetConjunction(
                    args, 
                    dr => args.IsPrimaryKey(dr["COLUMN_NAME"].ToString()), 
                    string.Empty, 
                    new string(' ', 4), 
                    "AND "
                );

                string endLine = args.IsLastPrimaryKey(column) ? string.Empty : Environment.NewLine;

                string item = string.Format(
                    "{1}{2}({3} = {4}){0}",
                    endLine,
                    args.ColumnIndex == 0 ? string.Empty : args.IndentString,
                    conjunction,
                    GetParameter(args),
                    GetColumn(args)
                );

                return args.IsPrimaryKey(column) ? item : string.Empty;
            }
            else
            {
                return "??WhereClauseItem??";
            }
        }

        #endregion

        #region Conjunction

        private static string GetConjunction(InterpolationArgs args, string onlyOne, string firstOne, string other)
        {
            return GetConjunction(args, dr => true, onlyOne, firstOne, other);
        }

        private static string GetConjunction(InterpolationArgs args, Func<DataRow, bool> filter, string onlyOne, string firstOne, string other)
        {
            var columns = args.Columns.Where(filter);
            var column = args.Columns.Where(filter).FirstOrDefault();

            if (columns.Count() == 1)
            {
                return onlyOne;
            }
            else if (column != null && column["COLUMN_NAME"].ToString() == args.Column["COLUMN_NAME"].ToString())
            {
                return firstOne;
            }
            else
            {
                return other;
            }
        }

        #endregion

        #endregion
    }
}
