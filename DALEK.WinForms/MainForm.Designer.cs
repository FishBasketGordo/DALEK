namespace DALEK.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.lblEntitiesOutputDirectory = new System.Windows.Forms.Label();
            this.txtEntitiesOutputDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseEntitiesOutput = new System.Windows.Forms.Button();
            this.btnBuildConnectionString = new System.Windows.Forms.Button();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dlgBrowseOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowseScriptOutput = new System.Windows.Forms.Button();
            this.lblScriptsOutputDirectory = new System.Windows.Forms.Label();
            this.txtScriptsOutputDirectory = new System.Windows.Forms.TextBox();
            this.chkGenerateScripts = new System.Windows.Forms.CheckBox();
            this.chkGenerateEntities = new System.Windows.Forms.CheckBox();
            this.btnGoToScriptsDirectory = new System.Windows.Forms.Button();
            this.btnGoToEntitiesDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(160, 12);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(308, 20);
            this.txtConnectionString.TabIndex = 0;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(33, 15);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.lblConnectionString.TabIndex = 1;
            this.lblConnectionString.Text = "&Connection String";
            // 
            // lblEntitiesOutputDirectory
            // 
            this.lblEntitiesOutputDirectory.AutoSize = true;
            this.lblEntitiesOutputDirectory.Location = new System.Drawing.Point(33, 41);
            this.lblEntitiesOutputDirectory.Name = "lblEntitiesOutputDirectory";
            this.lblEntitiesOutputDirectory.Size = new System.Drawing.Size(121, 13);
            this.lblEntitiesOutputDirectory.TabIndex = 5;
            this.lblEntitiesOutputDirectory.Text = "&Entities Output Directory";
            // 
            // txtEntitiesOutputDirectory
            // 
            this.txtEntitiesOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntitiesOutputDirectory.Location = new System.Drawing.Point(160, 38);
            this.txtEntitiesOutputDirectory.Name = "txtEntitiesOutputDirectory";
            this.txtEntitiesOutputDirectory.Size = new System.Drawing.Size(308, 20);
            this.txtEntitiesOutputDirectory.TabIndex = 4;
            // 
            // btnBrowseEntitiesOutput
            // 
            this.btnBrowseEntitiesOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseEntitiesOutput.Location = new System.Drawing.Point(474, 36);
            this.btnBrowseEntitiesOutput.Name = "btnBrowseEntitiesOutput";
            this.btnBrowseEntitiesOutput.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseEntitiesOutput.TabIndex = 6;
            this.btnBrowseEntitiesOutput.Text = "...";
            this.btnBrowseEntitiesOutput.UseVisualStyleBackColor = true;
            this.btnBrowseEntitiesOutput.Click += new System.EventHandler(this.btnBrowseOutputDirectory_Click);
            // 
            // btnBuildConnectionString
            // 
            this.btnBuildConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuildConnectionString.Location = new System.Drawing.Point(474, 10);
            this.btnBuildConnectionString.Name = "btnBuildConnectionString";
            this.btnBuildConnectionString.Size = new System.Drawing.Size(24, 23);
            this.btnBuildConnectionString.TabIndex = 7;
            this.btnBuildConnectionString.Text = "...";
            this.btnBuildConnectionString.UseVisualStyleBackColor = true;
            this.btnBuildConnectionString.Click += new System.EventHandler(this.btnBuildConnectionString_Click);
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnGenerate.Location = new System.Drawing.Point(233, 91);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 9;
            this.btnGenerate.Text = "&Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnBrowseScriptOutput
            // 
            this.btnBrowseScriptOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseScriptOutput.Location = new System.Drawing.Point(474, 63);
            this.btnBrowseScriptOutput.Name = "btnBrowseScriptOutput";
            this.btnBrowseScriptOutput.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseScriptOutput.TabIndex = 13;
            this.btnBrowseScriptOutput.Text = "...";
            this.btnBrowseScriptOutput.UseVisualStyleBackColor = true;
            this.btnBrowseScriptOutput.Click += new System.EventHandler(this.btnBrowseScriptOutput_Click);
            // 
            // lblScriptsOutputDirectory
            // 
            this.lblScriptsOutputDirectory.AutoSize = true;
            this.lblScriptsOutputDirectory.Location = new System.Drawing.Point(33, 68);
            this.lblScriptsOutputDirectory.Name = "lblScriptsOutputDirectory";
            this.lblScriptsOutputDirectory.Size = new System.Drawing.Size(119, 13);
            this.lblScriptsOutputDirectory.TabIndex = 12;
            this.lblScriptsOutputDirectory.Text = "&Scripts Output Directory";
            // 
            // txtScriptsOutputDirectory
            // 
            this.txtScriptsOutputDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScriptsOutputDirectory.Location = new System.Drawing.Point(160, 65);
            this.txtScriptsOutputDirectory.Name = "txtScriptsOutputDirectory";
            this.txtScriptsOutputDirectory.Size = new System.Drawing.Size(308, 20);
            this.txtScriptsOutputDirectory.TabIndex = 11;
            // 
            // chkGenerateScripts
            // 
            this.chkGenerateScripts.AutoSize = true;
            this.chkGenerateScripts.Checked = true;
            this.chkGenerateScripts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGenerateScripts.Location = new System.Drawing.Point(12, 68);
            this.chkGenerateScripts.Name = "chkGenerateScripts";
            this.chkGenerateScripts.Size = new System.Drawing.Size(15, 14);
            this.chkGenerateScripts.TabIndex = 14;
            this.chkGenerateScripts.UseVisualStyleBackColor = true;
            this.chkGenerateScripts.CheckedChanged += new System.EventHandler(this.chkGenerateScripts_CheckedChanged);
            // 
            // chkGenerateEntities
            // 
            this.chkGenerateEntities.AutoSize = true;
            this.chkGenerateEntities.Checked = true;
            this.chkGenerateEntities.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGenerateEntities.Location = new System.Drawing.Point(12, 41);
            this.chkGenerateEntities.Name = "chkGenerateEntities";
            this.chkGenerateEntities.Size = new System.Drawing.Size(15, 14);
            this.chkGenerateEntities.TabIndex = 15;
            this.chkGenerateEntities.UseVisualStyleBackColor = true;
            this.chkGenerateEntities.CheckedChanged += new System.EventHandler(this.chkGenerateEntities_CheckedChanged);
            // 
            // btnGoToScriptsDirectory
            // 
            this.btnGoToScriptsDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToScriptsDirectory.Location = new System.Drawing.Point(504, 63);
            this.btnGoToScriptsDirectory.Name = "btnGoToScriptsDirectory";
            this.btnGoToScriptsDirectory.Size = new System.Drawing.Size(24, 23);
            this.btnGoToScriptsDirectory.TabIndex = 16;
            this.btnGoToScriptsDirectory.Text = ">";
            this.btnGoToScriptsDirectory.UseVisualStyleBackColor = true;
            this.btnGoToScriptsDirectory.Click += new System.EventHandler(this.btnGoToScriptsDirectory_Click);
            // 
            // btnGoToEntitiesDirectory
            // 
            this.btnGoToEntitiesDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGoToEntitiesDirectory.Location = new System.Drawing.Point(504, 36);
            this.btnGoToEntitiesDirectory.Name = "btnGoToEntitiesDirectory";
            this.btnGoToEntitiesDirectory.Size = new System.Drawing.Size(24, 23);
            this.btnGoToEntitiesDirectory.TabIndex = 17;
            this.btnGoToEntitiesDirectory.Text = ">";
            this.btnGoToEntitiesDirectory.UseVisualStyleBackColor = true;
            this.btnGoToEntitiesDirectory.Click += new System.EventHandler(this.btnGoToEntitiesDirectory_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 126);
            this.Controls.Add(this.btnGoToEntitiesDirectory);
            this.Controls.Add(this.btnGoToScriptsDirectory);
            this.Controls.Add(this.chkGenerateEntities);
            this.Controls.Add(this.chkGenerateScripts);
            this.Controls.Add(this.btnBrowseScriptOutput);
            this.Controls.Add(this.lblScriptsOutputDirectory);
            this.Controls.Add(this.txtScriptsOutputDirectory);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.btnBuildConnectionString);
            this.Controls.Add(this.btnBrowseEntitiesOutput);
            this.Controls.Add(this.lblEntitiesOutputDirectory);
            this.Controls.Add(this.txtEntitiesOutputDirectory);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.txtConnectionString);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(10000, 164);
            this.MinimumSize = new System.Drawing.Size(300, 164);
            this.Name = "MainForm";
            this.Text = "D.A.L.E.K. - Database Access Layer & Entity Kreator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Label lblEntitiesOutputDirectory;
        private System.Windows.Forms.TextBox txtEntitiesOutputDirectory;
        private System.Windows.Forms.Button btnBrowseEntitiesOutput;
        private System.Windows.Forms.Button btnBuildConnectionString;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowseOutput;
        private System.Windows.Forms.Button btnBrowseScriptOutput;
        private System.Windows.Forms.Label lblScriptsOutputDirectory;
        private System.Windows.Forms.TextBox txtScriptsOutputDirectory;
        private System.Windows.Forms.CheckBox chkGenerateScripts;
        private System.Windows.Forms.CheckBox chkGenerateEntities;
        private System.Windows.Forms.Button btnGoToScriptsDirectory;
        private System.Windows.Forms.Button btnGoToEntitiesDirectory;
    }
}

