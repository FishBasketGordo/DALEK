using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DALEK.WinForms
{
    public partial class ConnectionStringForm : Form
    {
        #region Fields

        private Dictionary<string, Func<string>> _getters = null;
        private Dictionary<string, Action<string>> _setters = null;

        #endregion

        #region Properties

        public string ConnectionString
        {
            get 
            { 
                return lblConnectionString.Text; 
            }
            private set 
            { 
                lblConnectionString.Text = value; 
            }
        }

        private Dictionary<string, Func<string>> Getters
        {
            get
            {
                if (_getters == null)
                {
                    _getters = new Dictionary<string, Func<string>>
                    {
                        { "Data Source"           , () => txtDataSource.Text                        },
                        { "Initial Catalog"       , () => txtInitialCatalog.Text                    },
                        { "User ID"               , () => txtUserID.Text                            },
                        { "Password"              , () => txtPassword.Text                          },
                        { "Persist Security Info" , () => chkPersistSecurityInfo.Checked.ToString() },
                    };
                }

                return _getters;
            }
        }

        private Dictionary<string, Action<string>> Setters
        {
            get
            {
                if (_setters == null)
                {
                    _setters = new Dictionary<string, Action<string>>
                    {
                        { "Data Source"           , (val) => txtDataSource.Text = val                                },
                        { "Initial Catalog"       , (val) => txtInitialCatalog.Text = val                            },
                        { "User ID"               , (val) => txtUserID.Text = val                                    },
                        { "Password"              , (val) => txtPassword.Text = val                                  },
                        { "Persist Security Info" , (val) => chkPersistSecurityInfo.Checked = Convert.ToBoolean(val) },
                    };
                }

                return _setters;
            }
        }

        #endregion

        #region Constructors

        private ConnectionStringForm()
        {
            InitializeComponent();
        }

        public static ConnectionStringForm Create()
        {
            return new ConnectionStringForm();
        }

        public static ConnectionStringForm Create(string connectionString)
        {
            var form = new ConnectionStringForm();
            SetConnectionString(form, connectionString);
            return form;
        }

        #endregion

        #region Event Handlers

        private void ConnectionStringTextChanged(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        private void ConnectionStringCheckedChanged(object sender, EventArgs e)
        {
            SetConnectionString();
        }

        #endregion

        #region Methods

        private void SetConnectionString()
        {
            List<string> components = new List<string>();

            foreach (var kvp in Getters)
            {
                string value = kvp.Value();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    components.Add(string.Format("{0}={1}", kvp.Key, kvp.Value()));
                }
            }

            ConnectionString = string.Join(";", components);
        }

        private static void SetConnectionString(ConnectionStringForm form, string connectionString)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                var components = connectionString.Split(';');
                foreach (var component in components)
                {
                    var kvp = component.Split('=');
                    if (form.Setters.ContainsKey(kvp[0].Trim()))
                    {
                        form.Setters[kvp[0].Trim()](kvp[1]);
                    }
                }
            }

            form.SetConnectionString();
        }

        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
