using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infor.FSCM.Analytics
{
    public partial class LoginDialog : Form
    {
        public string URL { get; private set; }
        public string User { get; private set; }
        public string Password { get; private set; }

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonLogon_Click(object sender, EventArgs e)
        {
            Cursor c = this.Cursor;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                User = TextboxUser.Text;
                Password = TextboxPassword.Text;
                URL = TextboxURL.Text;
                BirstService _birstService = new BirstService(URL);
                _birstService.Login(TextboxUser.Text, TextboxPassword.Text);
                DialogResult = DialogResult.OK;
                Close();
                Cursor = c;
            }
            catch
            {
                Cursor = c;
                MessageBox.Show(this, "An error has occured when signing in. Please check your credentials.");
            }
        }
    }
}