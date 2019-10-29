using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Infor.FSCM.Analytics.BirstWebService;

namespace Infor.FSCM.Analytics
{
    public partial class DialogSpaceSelector : Form
    {
        public UserSpace SelectedSpace { get; private set; }

        public DialogSpaceSelector(BirstService birstService)
        {
            InitializeComponent();

            UserSpace[] spaces = birstService.GetUserSpaces();

            foreach (var space in spaces)
            {
                ListViewItem item = new ListViewItem(space.name);
                item.SubItems.Add(space.owner);
                item.SubItems.Add(space.id);
                item.Tag = space;

                ListViewUserSpaces.Items.Add(item);
            }

            ListViewUserSpaces.Columns[0].Width = -2;
            ListViewUserSpaces.Columns[1].Width = -2;
            ListViewUserSpaces.Columns[2].Width = -2;

            if (ListViewUserSpaces.Items.Count > 0)
            {
                ListViewUserSpaces.SelectedIndices.Add(0);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            SelectedSpace = ListViewUserSpaces.SelectedItems[0].Tag as UserSpace;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}