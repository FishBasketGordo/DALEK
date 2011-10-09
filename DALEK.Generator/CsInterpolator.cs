using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Creelio.Framework.Extensions;

namespace DALEK.Generator
{
    internal class CsInterpolator : Interpolator
    {
        #region Properties

        private Dictionary<string, string[]> _typeDictionary = null;

        public Dictionary<string, string[]> TypeDictionary
        {
            get
            {
                if (_typeDictionary == null)
                {
                    _typeDictionary = new Dictionary<string, string[]>
                    {
                       // Sql Data Type ///////////////////// CS Nullable /////// CS Non-nullable /////
                        { "BIGINT"           , new string[] { "long?"           , "long"           } },
                        { "BINARY"           , new string[] { "byte?[]"         , "byte[]"         } },
                        { "BIT"              , new string[] { "bool?"           , "bool"           } },
                        { "DATE"             , new string[] { "DateTime?"       , "DateTime"       } },
                        { "DATETIME"         , new string[] { "DateTime?"       , "DateTime"       } },
                        { "DATETIME2"        , new string[] { "DateTime?"       , "DateTime"       } },
                        { "DATETIMEOFFSET"   , new string[] { "DateTimeOffset?" , "DateTimeOffset" } },
                        { "DECIMAL"          , new string[] { "decimal?"        , "decimal"        } },
                        { "FLOAT"            , new string[] { "double?"         , "double"         } },
                        { "INT"              , new string[] { "int?"            , "int"            } },
                        { "MONEY"            , new string[] { "decimal?"        , "decimal"        } },
                        { "NCHAR"            , new string[] { "string"          , "string"         } },
                        { "NUMERIC"          , new string[] { "decimal?"        , "decimal"        } },
                        { "NVARCHAR"         , new string[] { "string"          , "string"         } },
                        { "REAL"             , new string[] { "float?"          , "float"          } },
                        { "ROWVERSION"       , new string[] { "byte?[]"         , "byte[]"         } },
                        { "SMALLINT"         , new string[] { "short?"          , "short"          } },
                        { "SMALLMONEY"       , new string[] { "decimal?"        , "decimal"        } },
                        { "SQL_VARIANT"      , new string[] { "object"          , "object"         } },
                        { "TIME"             , new string[] { "TimeSpan?"       , "TimeSpan"       } },
                        { "TINYINT"          , new string[] { "byte?"           , "byte"           } },
                        { "UNIQUEIDENTIFIER" , new string[] { "Guid?"           , "Guid"           } },
                        { "VARBINARY"        , new string[] { "byte?[]"         , "byte[]"         } },
                        { "VARCHAR"          , new string[] { "string"          , "string"         } },
                    };
                }

                return _typeDictionary;
            }
        }

        #endregion

        #region Methods

        public override string GetOutputFileName(InterpolationArgs args)
        {
            if (args.TemplateName == "Entities")
            {
                return GetEntitiesDataAccessorClass(args);
            }
            else
            {
                return GetEntity(args);
            }
        }

        protected override void PopulatePlaceHolderDictionary(Dictionary<string, Func<InterpolationArgs, string>> dictionary)
        {
            dictionary.Add("Connection"                 , (args) => GetConnection(args)                 );
            dictionary.Add("DataAccessorPropertiesList" , (args) => GetDataAccessorPropertiesList(args) );
            dictionary.Add("Database"                   , (args) => GetDatabase(args)                   );
            dictionary.Add("EntitiesDataAccessorClass"  , (args) => GetEntitiesDataAccessorClass(args)  );
            dictionary.Add("EntitiesNamespace"          , (args) => GetEntitiesNamespace(args)          );
            dictionary.Add("Entity"                     , (args) => GetEntity(args)                     );
            dictionary.Add("PropertiesList"             , (args) => GetList(args, GetProperty)          );
            dictionary.Add("PropertyType"               , (args) => GetPropertyType(args)               );
        }

        private string GetEntitiesDataAccessorClass(InterpolationArgs args)
        {
            return string.Format("{0}Entities", GetDatabase(args));
        }

        private string GetConnection(InterpolationArgs args)
        {
            return string.Format("_{0}ConnectionName", GetDatabase(args).ToCamelCase());
        }

        private string GetDataAccessorPropertiesList(InterpolationArgs args)
        {
            StringBuilder sb = new StringBuilder();
            string indent = string.Empty;

            foreach (DataRow row in args.AllTables)
            {
                string tableName = row["TABLE_NAME"].ToString();
                string tableNameCamelCase = tableName.ToCamelCase();

                sb.AppendFormat(
                    "{1}private static {5}<{3}> _{4} = null;   {0}" +
                    "{2}public static {5}<{3}> {3}             {0}" +
                    "{2}{{                                     {0}" +
                    "{2}    get                                {0}" +
                    "{2}    {{                                 {0}" +
                    "{2}        if (_{4} == null)              {0}" +
                    "{2}        {{                             {0}" +
                    "{2}            _{4} = new {5}<{3}>({6});  {0}" +
                    "{2}        }}                             {0}" +
                    "{2}        return _{4};                   {0}" +
                    "{2}    }}                                 {0}" +
                    "{2}}}                                     {0}" +
                    "{0}",
                    Environment.NewLine,
                    indent,
                    args.IndentString,
                    tableName,
                    tableNameCamelCase,
                    tableName.EndsWith("History") ? "ReadOnlyDataAccessor" : "DataAccessor",
                    tableName.EndsWith("History") 
                        ? string.Format("new DataAccessor<{0}>({1})", tableName, GetConnection(args)) 
                        : GetConnection(args)
                );

                indent = args.IndentString;
            }

            return sb.ToString();
        }

        private string GetDatabase(InterpolationArgs args)
        {
            return args.Column["TABLE_CATALOG"].ToString();
        }

        private string GetEntitiesNamespace(InterpolationArgs args)
        {
            return string.Format("{0}.Framework.DAL", GetDatabase(args));
        }

        private static string GetEntity(InterpolationArgs args)
        {
            return args.Column["TABLE_NAME"].ToString();
        }

        private string GetProperty(InterpolationArgs args)
        {
            return string.Format(
                    "{1}public {2} {3} {{ get; set; }}{0}",
                    args.ColumnIndex == args.Columns.Count - 1 ? string.Empty : Environment.NewLine,
                    args.ColumnIndex == 0 ? string.Empty : args.IndentString,                
                    GetPropertyType(args),
                    args.Column["COLUMN_NAME"]
                );
        }

        private string GetPropertyType(InterpolationArgs args)
        {
            string sqlDataType = args.Column["DATA_TYPE"].ToString().ToUpper();
            bool isNullable = string.Compare(args.Column["IS_NULLABLE"].ToString(), "YES", true) == 0;

            if (TypeDictionary.ContainsKey(sqlDataType))
            {
                // TODO: Also return nullable type if column is identity.
                return TypeDictionary[sqlDataType][isNullable ? 0 : 1];
            }
            else
            {
                return "??PropertyType??";
            }
        }

        #endregion
    }
}
