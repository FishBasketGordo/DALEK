using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DALEK.WinForms.Properties;
using DALEK.Generator;
using System.Diagnostics;
using System.IO;

namespace DALEK.WinForms
{
    public partial class MainForm : Form
    {
        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtConnectionString.Text = Settings.Default.ConnectionString;
            txtEntitiesOutputDirectory.Text = Settings.Default.EntitiesOutputDirectory;
            txtScriptsOutputDirectory.Text = Settings.Default.ScriptsOutputDirectory;
            chkGenerateEntities.Checked = Settings.Default.GenerateEntities;
            chkGenerateScripts.Checked = Settings.Default.GenerateScripts;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.ConnectionString = txtConnectionString.Text;
            Settings.Default.EntitiesOutputDirectory = txtEntitiesOutputDirectory.Text;
            Settings.Default.ScriptsOutputDirectory = txtScriptsOutputDirectory.Text;
            Settings.Default.GenerateEntities = chkGenerateEntities.Checked;
            Settings.Default.GenerateScripts = chkGenerateScripts.Checked;
            Settings.Default.Save();
        }

        private void btnBuildConnectionString_Click(object sender, EventArgs e)
        {
            using (ConnectionStringForm form = ConnectionStringForm.Create(txtConnectionString.Text))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    txtConnectionString.Text = form.ConnectionString;
                }
            }
        }

        private void btnBrowseOutputDirectory_Click(object sender, EventArgs e)
        {
            if (dlgBrowseOutput.ShowDialog() != DialogResult.Cancel)
            {
                txtEntitiesOutputDirectory.Text = dlgBrowseOutput.SelectedPath;
            }
        }

        private void btnBrowseScriptOutput_Click(object sender, EventArgs e)
        {
            if (dlgBrowseOutput.ShowDialog() != DialogResult.Cancel)
            {
                txtScriptsOutputDirectory.Text = dlgBrowseOutput.SelectedPath;
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            (new DalekGen()).Generate(
                txtConnectionString.Text,
                chkGenerateScripts.Checked ? txtScriptsOutputDirectory.Text : null,
                chkGenerateEntities.Checked ? txtEntitiesOutputDirectory.Text : null
            );
            
            this.Enabled = true;
        }

        private void chkGenerateEntities_CheckedChanged(object sender, EventArgs e)
        {
            txtEntitiesOutputDirectory.Enabled = chkGenerateEntities.Checked;
            btnBrowseEntitiesOutput.Enabled = chkGenerateEntities.Checked;
            btnGoToEntitiesDirectory.Enabled = chkGenerateEntities.Checked;

            btnGenerate.Enabled = chkGenerateEntities.Checked || chkGenerateScripts.Checked;
        }

        private void chkGenerateScripts_CheckedChanged(object sender, EventArgs e)
        {
            txtScriptsOutputDirectory.Enabled = chkGenerateScripts.Checked;
            btnBrowseScriptOutput.Enabled = chkGenerateScripts.Checked;
            btnGoToScriptsDirectory.Enabled = chkGenerateScripts.Checked;

            btnGenerate.Enabled = chkGenerateScripts.Checked || chkGenerateEntities.Checked;
        }

        private void btnGoToEntitiesDirectory_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtEntitiesOutputDirectory.Text))
            {
                Process.Start(txtEntitiesOutputDirectory.Text);
            }
        }

        private void btnGoToScriptsDirectory_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtScriptsOutputDirectory.Text))
            {
                Process.Start(txtScriptsOutputDirectory.Text);
            }
        }

        #endregion        
    }
}
