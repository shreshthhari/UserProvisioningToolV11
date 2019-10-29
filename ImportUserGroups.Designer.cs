namespace Infor.FSCM.Analytics

{
    partial class ImportUserGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportUserGroups));
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.LabelUserName = new System.Windows.Forms.Label();
            this.TextboxSelectedFile = new System.Windows.Forms.TextBox();
            this.ButtonSelectModelSpaceGold = new System.Windows.Forms.Button();
            this.ListviewCSV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CheckBoxGuidUsersId = new System.Windows.Forms.CheckBox();
            this.TextboxTenant = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextboxSelectedRolesFile = new System.Windows.Forms.TextBox();
            this.LabelRoles = new System.Windows.Forms.Label();
            this.ButtonSelectRolesFile = new System.Windows.Forms.Button();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ButtonOK
            // 
            this.ButtonOK.Location = new System.Drawing.Point(598, 661);
            this.ButtonOK.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(151, 42);
            this.ButtonOK.TabIndex = 3;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(767, 661);
            this.ButtonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(151, 42);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // LabelUserName
            // 
            this.LabelUserName.AutoSize = true;
            this.LabelUserName.Location = new System.Drawing.Point(14, 25);
            this.LabelUserName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelUserName.Name = "LabelUserName";
            this.LabelUserName.Size = new System.Drawing.Size(127, 20);
            this.LabelUserName.TabIndex = 15;
            this.LabelUserName.Text = "Import-User File:";
            // 
            // TextboxSelectedFile
            // 
            this.TextboxSelectedFile.Location = new System.Drawing.Point(14, 49);
            this.TextboxSelectedFile.Margin = new System.Windows.Forms.Padding(4);
            this.TextboxSelectedFile.Name = "TextboxSelectedFile";
            this.TextboxSelectedFile.Size = new System.Drawing.Size(843, 26);
            this.TextboxSelectedFile.TabIndex = 14;
            // 
            // ButtonSelectModelSpaceGold
            // 
            this.ButtonSelectModelSpaceGold.AutoSize = true;
            this.ButtonSelectModelSpaceGold.Location = new System.Drawing.Point(865, 47);
            this.ButtonSelectModelSpaceGold.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonSelectModelSpaceGold.Name = "ButtonSelectModelSpaceGold";
            this.ButtonSelectModelSpaceGold.Size = new System.Drawing.Size(54, 32);
            this.ButtonSelectModelSpaceGold.TabIndex = 16;
            this.ButtonSelectModelSpaceGold.Text = "...";
            this.ButtonSelectModelSpaceGold.UseVisualStyleBackColor = true;
            this.ButtonSelectModelSpaceGold.Click += new System.EventHandler(this.ButtonSelectModelSpaceGold_Click);
            // 
            // ListviewCSV
            // 
            this.ListviewCSV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10});
            this.ListviewCSV.GridLines = true;
            this.ListviewCSV.HideSelection = false;
            this.ListviewCSV.Location = new System.Drawing.Point(14, 249);
            this.ListviewCSV.Margin = new System.Windows.Forms.Padding(4);
            this.ListviewCSV.Name = "ListviewCSV";
            this.ListviewCSV.Size = new System.Drawing.Size(904, 394);
            this.ListviewCSV.TabIndex = 17;
            this.ListviewCSV.UseCompatibleStateImageBehavior = false;
            this.ListviewCSV.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Windows Account Name";
            this.columnHeader1.Width = 194;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "User Group";
            this.columnHeader2.Width = 123;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Role Description";
            this.columnHeader3.Width = 149;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Email Address";
            this.columnHeader4.Width = 133;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Identity";
            this.columnHeader5.Width = 113;
            // 
            // CheckBoxGuidUsersId
            // 
            this.CheckBoxGuidUsersId.AutoSize = true;
            this.CheckBoxGuidUsersId.Checked = true;
            this.CheckBoxGuidUsersId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBoxGuidUsersId.Location = new System.Drawing.Point(274, 199);
            this.CheckBoxGuidUsersId.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxGuidUsersId.Name = "CheckBoxGuidUsersId";
            this.CheckBoxGuidUsersId.Size = new System.Drawing.Size(356, 24);
            this.CheckBoxGuidUsersId.TabIndex = 18;
            this.CheckBoxGuidUsersId.Text = "Create users using tenant_userguid schema. ";
            this.CheckBoxGuidUsersId.UseVisualStyleBackColor = true;
            // 
            // TextboxTenant
            // 
            this.TextboxTenant.Location = new System.Drawing.Point(13, 197);
            this.TextboxTenant.Margin = new System.Windows.Forms.Padding(4);
            this.TextboxTenant.Name = "TextboxTenant";
            this.TextboxTenant.Size = new System.Drawing.Size(249, 26);
            this.TextboxTenant.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 173);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Tenant ID:";
            // 
            // TextboxSelectedRolesFile
            // 
            this.TextboxSelectedRolesFile.Location = new System.Drawing.Point(14, 125);
            this.TextboxSelectedRolesFile.Margin = new System.Windows.Forms.Padding(4);
            this.TextboxSelectedRolesFile.Name = "TextboxSelectedRolesFile";
            this.TextboxSelectedRolesFile.Size = new System.Drawing.Size(843, 26);
            this.TextboxSelectedRolesFile.TabIndex = 14;
            // 
            // LabelRoles
            // 
            this.LabelRoles.AutoSize = true;
            this.LabelRoles.Location = new System.Drawing.Point(14, 102);
            this.LabelRoles.Name = "LabelRoles";
            this.LabelRoles.Size = new System.Drawing.Size(134, 20);
            this.LabelRoles.TabIndex = 22;
            this.LabelRoles.Text = "Import-Roles File:";
            // 
            // ButtonSelectRolesFile
            // 
            this.ButtonSelectRolesFile.AutoSize = true;
            this.ButtonSelectRolesFile.Location = new System.Drawing.Point(865, 122);
            this.ButtonSelectRolesFile.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonSelectRolesFile.Name = "ButtonSelectRolesFile";
            this.ButtonSelectRolesFile.Size = new System.Drawing.Size(54, 32);
            this.ButtonSelectRolesFile.TabIndex = 16;
            this.ButtonSelectRolesFile.Text = "...";
            this.ButtonSelectRolesFile.UseVisualStyleBackColor = true;
            this.ButtonSelectRolesFile.Click += new System.EventHandler(this.ButtonSelectRolesFile_Click);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Identity 2";
            this.columnHeader6.Width = 141;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "User Principal Name";
            this.columnHeader7.Width = 154;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Client Principal Name";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Common Name";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Status";
            // 
            // ImportUserGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(940, 716);
            this.Controls.Add(this.ButtonSelectRolesFile);
            this.Controls.Add(this.LabelRoles);
            this.Controls.Add(this.TextboxSelectedRolesFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextboxTenant);
            this.Controls.Add(this.CheckBoxGuidUsersId);
            this.Controls.Add(this.ListviewCSV);
            this.Controls.Add(this.LabelUserName);
            this.Controls.Add(this.TextboxSelectedFile);
            this.Controls.Add(this.ButtonSelectModelSpaceGold);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.ButtonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportUserGroups";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Label LabelUserName;
        private System.Windows.Forms.TextBox TextboxSelectedFile;
        private System.Windows.Forms.Button ButtonSelectModelSpaceGold;
        private System.Windows.Forms.ListView ListviewCSV;
        private System.Windows.Forms.CheckBox CheckBoxGuidUsersId;
        private System.Windows.Forms.TextBox TextboxTenant;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextboxSelectedRolesFile;
        private System.Windows.Forms.Label LabelRoles;
        private System.Windows.Forms.Button ButtonSelectRolesFile;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
    }
}