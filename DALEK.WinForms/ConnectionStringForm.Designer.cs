namespace DALEK.WinForms
{
    partial class ConnectionStringForm
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
            this.pnlTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblDatasource = new System.Windows.Forms.Label();
            this.lblInitialCatalog = new System.Windows.Forms.Label();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.txtInitialCatalog = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.chkPersistSecurityInfo = new System.Windows.Forms.CheckBox();
            this.lblPersistSecurityInfo = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlTableLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTableLayout
            // 
            this.pnlTableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTableLayout.ColumnCount = 2;
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.pnlTableLayout.Controls.Add(this.lblDatasource, 0, 0);
            this.pnlTableLayout.Controls.Add(this.lblInitialCatalog, 0, 1);
            this.pnlTableLayout.Controls.Add(this.txtDataSource, 1, 0);
            this.pnlTableLayout.Controls.Add(this.txtInitialCatalog, 1, 1);
            this.pnlTableLayout.Controls.Add(this.lblUserName, 0, 2);
            this.pnlTableLayout.Controls.Add(this.chkPersistSecurityInfo, 1, 4);
            this.pnlTableLayout.Controls.Add(this.lblPersistSecurityInfo, 0, 4);
            this.pnlTableLayout.Controls.Add(this.txtUserID, 1, 2);
            this.pnlTableLayout.Controls.Add(this.txtPassword, 1, 3);
            this.pnlTableLayout.Controls.Add(this.lblPassword, 0, 3);
            this.pnlTableLayout.Controls.Add(this.lblConnectionString, 0, 5);
            this.pnlTableLayout.Location = new System.Drawing.Point(12, 12);
            this.pnlTableLayout.Name = "pnlTableLayout";
            this.pnlTableLayout.RowCount = 6;
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.pnlTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.pnlTableLayout.Size = new System.Drawing.Size(460, 163);
            this.pnlTableLayout.TabIndex = 0;
            // 
            // lblDatasource
            // 
            this.lblDatasource.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDatasource.AutoSize = true;
            this.lblDatasource.Location = new System.Drawing.Point(36, 6);
            this.lblDatasource.Name = "lblDatasource";
            this.lblDatasource.Size = new System.Drawing.Size(67, 13);
            this.lblDatasource.TabIndex = 0;
            this.lblDatasource.Text = "Data Source";
            // 
            // lblInitialCatalog
            // 
            this.lblInitialCatalog.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInitialCatalog.AutoSize = true;
            this.lblInitialCatalog.Location = new System.Drawing.Point(33, 32);
            this.lblInitialCatalog.Name = "lblInitialCatalog";
            this.lblInitialCatalog.Size = new System.Drawing.Size(70, 13);
            this.lblInitialCatalog.TabIndex = 2;
            this.lblInitialCatalog.Text = "Initial Catalog";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDataSource.Location = new System.Drawing.Point(109, 3);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(348, 20);
            this.txtDataSource.TabIndex = 1;
            this.txtDataSource.TextChanged += new System.EventHandler(this.ConnectionStringTextChanged);
            // 
            // txtInitialCatalog
            // 
            this.txtInitialCatalog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInitialCatalog.Location = new System.Drawing.Point(109, 29);
            this.txtInitialCatalog.Name = "txtInitialCatalog";
            this.txtInitialCatalog.Size = new System.Drawing.Size(348, 20);
            this.txtInitialCatalog.TabIndex = 3;
            this.txtInitialCatalog.TextChanged += new System.EventHandler(this.ConnectionStringTextChanged);
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(43, 58);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 13);
            this.lblUserName.TabIndex = 4;
            this.lblUserName.Text = "User Name";
            // 
            // chkPersistSecurityInfo
            // 
            this.chkPersistSecurityInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkPersistSecurityInfo.AutoSize = true;
            this.chkPersistSecurityInfo.Location = new System.Drawing.Point(109, 107);
            this.chkPersistSecurityInfo.Name = "chkPersistSecurityInfo";
            this.chkPersistSecurityInfo.Size = new System.Drawing.Size(15, 14);
            this.chkPersistSecurityInfo.TabIndex = 9;
            this.chkPersistSecurityInfo.UseVisualStyleBackColor = true;
            this.chkPersistSecurityInfo.CheckedChanged += new System.EventHandler(this.ConnectionStringCheckedChanged);
            // 
            // lblPersistSecurityInfo
            // 
            this.lblPersistSecurityInfo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPersistSecurityInfo.AutoSize = true;
            this.lblPersistSecurityInfo.Location = new System.Drawing.Point(3, 107);
            this.lblPersistSecurityInfo.Name = "lblPersistSecurityInfo";
            this.lblPersistSecurityInfo.Size = new System.Drawing.Size(100, 13);
            this.lblPersistSecurityInfo.TabIndex = 8;
            this.lblPersistSecurityInfo.Text = "Persist Security Info";
            // 
            // txtUserID
            // 
            this.txtUserID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserID.Location = new System.Drawing.Point(109, 55);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(348, 20);
            this.txtUserID.TabIndex = 5;
            this.txtUserID.TextChanged += new System.EventHandler(this.ConnectionStringTextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(109, 81);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(348, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.TextChanged += new System.EventHandler(this.ConnectionStringTextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(50, 84);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password";
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConnectionString.AutoEllipsis = true;
            this.lblConnectionString.AutoSize = true;
            this.pnlTableLayout.SetColumnSpan(this.lblConnectionString, 2);
            this.lblConnectionString.Location = new System.Drawing.Point(127, 137);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(206, 13);
            this.lblConnectionString.TabIndex = 10;
            this.lblConnectionString.Text = "This is where the connection string will go.";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(245, 181);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.Location = new System.Drawing.Point(164, 181);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ConnectionStringForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 216);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pnlTableLayout);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "ConnectionStringForm";
            this.Text = "Connection String";
            this.pnlTableLayout.ResumeLayout(false);
            this.pnlTableLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlTableLayout;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.TextBox txtInitialCatalog;
        private System.Windows.Forms.CheckBox chkPersistSecurityInfo;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblDatasource;
        private System.Windows.Forms.Label lblInitialCatalog;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPersistSecurityInfo;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}