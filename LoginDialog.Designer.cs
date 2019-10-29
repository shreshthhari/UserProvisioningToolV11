namespace Infor.FSCM.Analytics
{
    partial class LoginDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginDialog));
            this.ButtonLogon = new System.Windows.Forms.Button();
            this.LabelAccountAdminName = new System.Windows.Forms.Label();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.TextboxUser = new System.Windows.Forms.TextBox();
            this.TextboxPassword = new System.Windows.Forms.TextBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.LabelURL = new System.Windows.Forms.Label();
            this.TextboxURL = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ButtonLogon
            // 
            this.ButtonLogon.AutoSize = true;
            this.ButtonLogon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonLogon.Location = new System.Drawing.Point(248, 207);
            this.ButtonLogon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ButtonLogon.Name = "ButtonLogon";
            this.ButtonLogon.Size = new System.Drawing.Size(115, 35);
            this.ButtonLogon.TabIndex = 22;
            this.ButtonLogon.Text = "Sign in";
            this.ButtonLogon.UseVisualStyleBackColor = true;
            this.ButtonLogon.Click += new System.EventHandler(this.ButtonLogon_Click);
            // 
            // LabelAccountAdminName
            // 
            this.LabelAccountAdminName.AutoSize = true;
            this.LabelAccountAdminName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAccountAdminName.Location = new System.Drawing.Point(12, 72);
            this.LabelAccountAdminName.Name = "LabelAccountAdminName";
            this.LabelAccountAdminName.Size = new System.Drawing.Size(44, 18);
            this.LabelAccountAdminName.TabIndex = 19;
            this.LabelAccountAdminName.Text = "User:";
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPassword.Location = new System.Drawing.Point(12, 137);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(79, 18);
            this.LabelPassword.TabIndex = 20;
            this.LabelPassword.Text = "Password:";
            // 
            // TextboxUser
            // 
            this.TextboxUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxUser.Location = new System.Drawing.Point(15, 96);
            this.TextboxUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextboxUser.Name = "TextboxUser";
            this.TextboxUser.Size = new System.Drawing.Size(469, 24);
            this.TextboxUser.TabIndex = 18;
            // 
            // TextboxPassword
            // 
            this.TextboxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxPassword.Location = new System.Drawing.Point(15, 159);
            this.TextboxPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextboxPassword.Name = "TextboxPassword";
            this.TextboxPassword.PasswordChar = '*';
            this.TextboxPassword.Size = new System.Drawing.Size(469, 24);
            this.TextboxPassword.TabIndex = 21;
            // 
            // ButtonClose
            // 
            this.ButtonClose.AutoSize = true;
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClose.Location = new System.Drawing.Point(369, 207);
            this.ButtonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(115, 35);
            this.ButtonClose.TabIndex = 23;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // LabelURL
            // 
            this.LabelURL.AutoSize = true;
            this.LabelURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelURL.Location = new System.Drawing.Point(9, 11);
            this.LabelURL.Name = "LabelURL";
            this.LabelURL.Size = new System.Drawing.Size(81, 18);
            this.LabelURL.TabIndex = 25;
            this.LabelURL.Text = "Birst Cloud";
            // 
            // TextboxURL
            // 
            this.TextboxURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextboxURL.Location = new System.Drawing.Point(12, 35);
            this.TextboxURL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TextboxURL.Name = "TextboxURL";
            this.TextboxURL.Size = new System.Drawing.Size(472, 24);
            this.TextboxURL.TabIndex = 24;
            this.TextboxURL.Text = "https://login.bws.birst.com/CommandWebService.asmx?wsdl";
            // 
            // LoginDialog
            // 
            this.AcceptButton = this.ButtonLogon;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(495, 255);
            this.Controls.Add(this.LabelURL);
            this.Controls.Add(this.TextboxURL);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ButtonLogon);
            this.Controls.Add(this.LabelAccountAdminName);
            this.Controls.Add(this.LabelPassword);
            this.Controls.Add(this.TextboxUser);
            this.Controls.Add(this.TextboxPassword);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sign In";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonLogon;
        private System.Windows.Forms.Label LabelAccountAdminName;
        private System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.TextBox TextboxUser;
        private System.Windows.Forms.TextBox TextboxPassword;
        private System.Windows.Forms.Button ButtonClose;
        private System.Windows.Forms.Label LabelURL;
        private System.Windows.Forms.TextBox TextboxURL;
    }
}