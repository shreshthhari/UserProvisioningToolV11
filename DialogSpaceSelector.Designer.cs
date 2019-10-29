namespace Infor.FSCM.Analytics
{
    partial class DialogSpaceSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSpaceSelector));
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ListViewUserSpaces = new System.Windows.Forms.ListView();
            this.NameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OwnerColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SpaceIDColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelUserSpaces = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(563, 441);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(126, 35);
            this.ButtonCancel.TabIndex = 0;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOK
            // 
            this.ButtonOK.Location = new System.Drawing.Point(431, 441);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(126, 35);
            this.ButtonOK.TabIndex = 1;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ListViewUserSpaces
            // 
            this.ListViewUserSpaces.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameColumn,
            this.OwnerColumn,
            this.SpaceIDColumn});
            this.ListViewUserSpaces.FullRowSelect = true;
            this.ListViewUserSpaces.GridLines = true;
            this.ListViewUserSpaces.Location = new System.Drawing.Point(12, 29);
            this.ListViewUserSpaces.MultiSelect = false;
            this.ListViewUserSpaces.Name = "ListViewUserSpaces";
            this.ListViewUserSpaces.Size = new System.Drawing.Size(677, 389);
            this.ListViewUserSpaces.TabIndex = 2;
            this.ListViewUserSpaces.UseCompatibleStateImageBehavior = false;
            this.ListViewUserSpaces.View = System.Windows.Forms.View.Details;
            // 
            // NameColumn
            // 
            this.NameColumn.Text = "Name";
            this.NameColumn.Width = 120;
            // 
            // OwnerColumn
            // 
            this.OwnerColumn.Text = "Owner";
            this.OwnerColumn.Width = 120;
            // 
            // SpaceIDColumn
            // 
            this.SpaceIDColumn.Text = "Space ID";
            this.SpaceIDColumn.Width = 317;
            // 
            // LabelUserSpaces
            // 
            this.LabelUserSpaces.AutoSize = true;
            this.LabelUserSpaces.Location = new System.Drawing.Point(12, 9);
            this.LabelUserSpaces.Name = "LabelUserSpaces";
            this.LabelUserSpaces.Size = new System.Drawing.Size(93, 17);
            this.LabelUserSpaces.TabIndex = 3;
            this.LabelUserSpaces.Text = "User Spaces:";
            // 
            // DialogSpaceSelector
            // 
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(703, 488);
            this.Controls.Add(this.LabelUserSpaces);
            this.Controls.Add(this.ListViewUserSpaces);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.ButtonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogSpaceSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Space";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.ListView ListViewUserSpaces;
        private System.Windows.Forms.ColumnHeader NameColumn;
        private System.Windows.Forms.ColumnHeader OwnerColumn;
        private System.Windows.Forms.ColumnHeader SpaceIDColumn;
        private System.Windows.Forms.Label LabelUserSpaces;
    }
}