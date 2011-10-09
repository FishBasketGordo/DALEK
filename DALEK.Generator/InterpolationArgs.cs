using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DALEK.Generator
{
    internal class InterpolationArgs
    {
        #region Fields

        private string _connectionString = null;
        private SqlConnectionStringBuilder _connectionStringBuilder = null;
        private int _indentSpaces = 0;
        private string _indentString = null;

        #endregion

        #region Properties

        public string TemplateName { get; set; }

        public string TableName { get; set; }

        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                if (value != _connectionString)
                    _connectionStringBuilder = null;

                _connectionString = value;
            }
        }

        public SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get
            {
                if (_connectionStringBuilder == null)
                {
                    _connectionStringBuilder = new SqlConnectionStringBuilder(ConnectionString);
                }

                return _connectionStringBuilder;
            }
        }

        public string OutputFilePath { get; set; }
        
        public List<DataRow> AllTables { get; set; }
                
        public int ColumnIndex { get; set; }
        
        public List<DataRow> Columns { get; set; }
        
        public List<DataRow> PrimaryKeys { get; set; }

        public DataRow Column
        {
            get
            {
                if (Columns == null || Columns.Count < 1 || ColumnIndex < 0 || ColumnIndex > Columns.Count)
                {
                    throw new InvalidOperationException("The Columns and/or ColumnIndex properties are not set properly.");
                }

                return Columns[ColumnIndex];
            }
        }

        public int IndentSpaces
        {
            get { return _indentSpaces; }
            set
            {
                if (value != _indentSpaces)
                    _indentString = null;

                _indentSpaces = value;
            }
        }

        public string IndentString
        {
            get
            {
                if (_indentString == null)
                {
                    if (IndentSpaces == 0)
                    {
                        _indentString = string.Empty;
                    }
                    else
                    {
                        _indentString = new string(' ', IndentSpaces);
                    }
                }

                return _indentString;
            }
        }

        #endregion

        #region Constructors

        public InterpolationArgs()
        {
            TemplateName = "UNKNOWN";
            ColumnIndex = 0;
        }

        #endregion

        #region Methods

        public bool IsPrimaryKey(string column)
        {
            return PrimaryKeys.Find(dr => dr["COLUMN_NAME"].ToString() == column) != null;
        }

        public bool IsFirstPrimaryKey(string column)
        {
            if (!IsPrimaryKey(column))
            {
                return false;
            }
            else
            {
                int index = Columns.FindIndex(dr => dr["COLUMN_NAME"].ToString() == column);
                return !(from c in Columns
                         where Columns.IndexOf(c) < index && IsPrimaryKey(c["COLUMN_NAME"].ToString())
                         select c).Any();
            }
        }

        public bool IsLastPrimaryKey(string column)
        {
            if (!IsPrimaryKey(column))
            {
                return false;
            }
            else
            {
                int index = Columns.FindIndex(dr => dr["COLUMN_NAME"].ToString() == column);
                return !(from c in Columns
                         where Columns.IndexOf(c) > index && IsPrimaryKey(c["COLUMN_NAME"].ToString())
                         select c).Any();
            }
        }

        public bool IsIdentity(string column)
        {
            return PrimaryKeys.Find(dr => dr["COLUMN_NAME"].ToString() == column && Convert.ToBoolean(dr["IS_IDENTITY"])) != null;
        }

        public bool IsFirstNonIdentity(string column)
        {
            if (IsIdentity(column))
            {
                return false;
            }
            else
            {
                int index = Columns.FindIndex(dr => dr["COLUMN_NAME"].ToString() == column);
                return !(from c in Columns
                         where Columns.IndexOf(c) < index && !IsIdentity(c["COLUMN_NAME"].ToString())
                         select c).Any();
            }
        }

        public bool IsLastNonIdentity(string column)
        {
            if (IsIdentity(column))
            {
                return false;
            }
            else
            {
                int index = Columns.FindIndex(dr => dr["COLUMN_NAME"].ToString() == column);
                return !(from c in Columns
                         where Columns.IndexOf(c) > index && !IsIdentity(c["COLUMN_NAME"].ToString())
                         select c).Any();
            }
        }

        #endregion
    }
}
