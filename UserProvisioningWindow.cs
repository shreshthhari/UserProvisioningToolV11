using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Infor.FSCM.Analytics
{
    /// <summary>
    /// This class implements the main window for user provisioning.
    /// </summary>
    public partial class UserProvisioningWindow : Form
    {
        //private string BirstFarmURL = "https://login.bws.birst.com/CommandWebService.asmx?wsdl";
        //private string BirstFarmURL = "https://nginx-feature-003.ensayo.birst.cc/CommandWebService.asmx?wsdl";

        private class DasboardType
        {
            public string Text { get; private set; }
            public string Id { get; private set; }

            public DasboardType(string text, string id)
            {
                Text = text;
                Id = id;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private Dictionary<string, BirstUser> importedUsers = new Dictionary<string, BirstUser>();
        private Dictionary<string, BirstUser> spaceUsers = new Dictionary<string, BirstUser>();

        private const string Error = "ERROR___";
        private const string Success = "SUCCESS_";

        private const string GeneralError = "GENERAL_ERROR_____";
        private const string CreateUser = "CREATE_USER_______";
        private const string AddSpaceUser = "ADD_SPACE_USER____";
        private const string AddSpaceGroup = "ADD_SPACE_GROUP___";
        private const string AddUserToGroup = "ADD_USER_TO_GROUP_";

        private readonly Color existingItemColor = Color.DarkGreen;
        private readonly Color spaceOnlyItemColor = Color.DarkRed;

        private string _birstServiceURL;
        private BirstService _birstService;

        private readonly string _logFilePath;
        private string _user;
        private string _password;
        private string _space;
        private string _spaceId;
        private bool _loggedOn;

        /// <summary>
        /// Initialies a new instance of class MainWindow.
        /// </summary>
        public UserProvisioningWindow()
        {
            InitializeComponent();

            Logon(false, string.Empty, string.Empty, string.Empty);
            _logFilePath = Path.Combine(Application.StartupPath, "user-provisioning-log.txt");
            dropdownDashboard.Items.Clear();
            dropdownDashboard.Items.Add(new DasboardType("Flash", "flash"));
            dropdownDashboard.Items.Add(new DasboardType("Dashboard 2.0", "dashboard2.0"));
            dropdownDashboard.Items.Add(new DasboardType("HTML", "html"));
            dropdownDashboard.SelectedIndex = 1;
        }

        /// <summary>
        /// Initialies a new instance of class MainWindow.
        /// </summary>
        public UserProvisioningWindow(string user, string password, string url)
        {
            InitializeComponent();

            ButtonSignin.Visible = false;
            ButtonSignout.Visible = false;
            _logFilePath = Path.Combine(Application.StartupPath, "user-provisioning-log.txt");
            Logon(true, user, password, url);
            dropdownDashboard.Items.Clear();
            dropdownDashboard.Items.Add(new DasboardType("Flash", "flash"));
            dropdownDashboard.Items.Add(new DasboardType("Dashboard 2.0", "dashboard2.0"));
            dropdownDashboard.Items.Add(new DasboardType("HTML", "html"));
            dropdownDashboard.SelectedIndex = 1;
        }

        /// <summary>
        /// This method signs into or out of Birst and sets the application to the login or logoff status to ensure that users can only do what is allowed in the corresponding situation.
        /// </summary>
        /// <param name="logon">If true, UI apears as logged in, otherwise logged off.</param>
        /// <param name="user">The user name to connect.</param>
        /// <param name="password"></param>
        /// <param name="url"></param>
        private void Logon(bool logon, string user, string password, string url)
        {
            if (logon)
            {
                _birstServiceURL = url;
                _user = user;
                _password = password;

                _birstService = new BirstService(_birstServiceURL);
                _birstService.Login(_user, _password);
                _loggedOn =
                ButtonSignout.Enabled =
                ButtonSelectSpace.Enabled = true;
                LoginStatus.Text = "Signed in";
                StatusUser.Text = "User: " + user;
                StatusSpace.Text = "Space: select";
                StatusSpace.Text = string.Empty;
                _space = string.Empty;
                _spaceId = string.Empty;
            }
            else
            {
                try
                {
                    if (_birstService != null)
                    {
                        _birstService.Logout();
                    }
                }
                catch
                {
                }
                finally
                {
                    _birstService = null;
                    _loggedOn =
                    ButtonSignout.Enabled =
                    ButtonSelectSpace.Enabled =
                    ButtonImportFile.Enabled =
                    ButtonUpdateSpace.Enabled =
                    ButtonExportFile.Enabled = false;
                    _user = string.Empty;
                    _password = string.Empty;
                    LoginStatus.Text = "Signed out";
                    StatusUser.Text = string.Empty;
                    StatusSpace.Text = string.Empty;
                    StatusSpaceID.Text = string.Empty;
                    _space = string.Empty;
                    _spaceId = string.Empty;

                    ListViewUsers.Items.Clear();
                    ListViewGroups.Items.Clear();
                    ListViewAssignments.Items.Clear();

                    ListViewUsers.Groups.Clear();
                    ListViewGroups.Groups.Clear();
                    ListViewAssignments.Groups.Clear();
                }
            }
        }

        /// <summary>
        /// Birst closed connections after 15 minutes. Relogin simply reconnects if the connection is down.
        /// </summary>
        private void EnsureConnection()
        {
            try
            {
                // try getting spaces. if connection is closed it will reconnect in exception handler
                _birstService.GetUserSpaces();
            }
            catch
            {
                _birstService = new BirstService(_birstServiceURL);
                _birstService.Login(_user, _password);
            }
        }

        /// <summary>
        /// Handles the sign in button click event.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ButtonSignInClick(object sender, EventArgs e)
        {
            try
            {
                using (LoginDialog dialog = new LoginDialog())
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        Logon(true, dialog.User, dialog.Password, dialog.URL);
                    }
                }
            }
            catch
            {
                MessageBox.Show(this, "An error has occured when logging in. Please check your credentials.");
            }
        }

        /// <summary>
        /// Handles the sign out button click event.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ButtonSignout_Click(object sender, EventArgs e)
        {
            try
            {
                Logon(false, string.Empty, string.Empty, string.Empty);
            }
            catch
            {
                // ignore logout errors
            }
        }

        /// <summary>
        /// Handles the select space button click event.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ButtonSelectSpace_Click(object sender, EventArgs e)
        {
            try
            {
                EnsureConnection();
                using (DialogSpaceSelector dialog = new DialogSpaceSelector(_birstService))
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        _space = dialog.SelectedSpace.name;
                        _spaceId = dialog.SelectedSpace.id;
                        StatusSpace.Text = "Space: " + _space;
                        StatusSpaceID.Text = "Space ID: " + _spaceId;
                        ButtonExportFile.Enabled = ButtonImportFile.Enabled = ButtonUpdateSpace.Enabled = true;

                        ListViewUsers.Items.Clear();
                        ListViewGroups.Items.Clear();
                        ListViewAssignments.Items.Clear();

                        ListViewUsers.Groups.Clear();
                        ListViewGroups.Groups.Clear();
                        ListViewAssignments.Groups.Clear();

                        LoadSpaceIntoUI();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, "An error has occured: " + exc.Message);
            }
        }

        /// <summary>
        /// Handles the import file button click event.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void ButtonImportFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (Font boldGreen = new Font(ListViewUsers.Font.FontFamily, ListViewUsers.Font.SizeInPoints, FontStyle.Regular))
                {
                    EnsureConnection();
                    using (ImportUserGroups dlg = new ImportUserGroups())
                    {
                        if (dlg.ShowDialog(this) == DialogResult.OK)
                        {
                            string filename = dlg.TempFile;

                            if (!File.Exists(filename))
                            {
                                MessageBox.Show(this, "An error has occured. The import file does not exist or cannot be opened.");
                                return;
                            }
                            SpaceSetup space = LoadSpaceSetup();
                            List<string> groups = new List<string>();
                            List<string> users = new List<string>();
                            importedUsers = new Dictionary<string, BirstUser>();
                            Dictionary<string, List<string>> assignments = new Dictionary<string, List<string>>();
                            bool useTenantSchema = dlg.UseTenantSchema;
                            string tenantId = dlg.TenantID;

                            StatusProgress.ProgressBar.Value = 0;

                            SuspendLayout();

                            ListViewUsers.SuspendLayout();
                            ListViewGroups.SuspendLayout();
                            ListViewAssignments.SuspendLayout();

                            ListViewUsers.BeginUpdate();
                            ListViewGroups.BeginUpdate();
                            ListViewAssignments.BeginUpdate();

                            ListViewUsers.Items.Clear();
                            ListViewGroups.Items.Clear();
                            ListViewAssignments.Items.Clear();

                            ListViewUsers.Groups.Clear();
                            ListViewGroups.Groups.Clear();
                            ListViewAssignments.Items.Clear();

                            ListViewUsers.Groups.Add(new ListViewGroup("sync", "Synchronized"));
                            ListViewUsers.Groups.Add(new ListViewGroup("import", "Import only"));
                            ListViewUsers.Groups.Add(new ListViewGroup("space", "Space only"));

                            ListViewUsers.Groups[0].HeaderAlignment = HorizontalAlignment.Left;

                            ListViewGroups.Groups.Add(new ListViewGroup("sync", "Synchronized"));
                            ListViewGroups.Groups.Add(new ListViewGroup("import", "Import only"));
                            ListViewGroups.Groups.Add(new ListViewGroup("space", "Space only"));

                            ListViewAssignments.Groups.Add(new ListViewGroup("sync", "Synchronized"));
                            ListViewAssignments.Groups.Add(new ListViewGroup("import", "Import only"));
                            ListViewAssignments.Groups.Add(new ListViewGroup("space", "Space only"));

                            // insert all information from import file and check whether this exist in the space
                            using (TextFieldParser parser = new TextFieldParser(filename))
                            {
                                parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                                parser.SetDelimiters(",");
                                string[] header = null;
                                bool skipheader = true;
                                int userColumn = -1;
                                int userGuidColumn = -1;
                                int userEmailColumn = -1;

                                while (!parser.EndOfData)
                                {
                                    if (skipheader)
                                    {
                                        header = parser.ReadFields();
                                        skipheader = false;
                                        for (int i = 0; i < header.Length; i++)
                                        {
                                            if (header[i].ToLowerInvariant().StartsWith("userguid"))
                                            {
                                                userGuidColumn = i;
                                            }
                                            if (header[i].ToLowerInvariant().StartsWith("user name"))
                                            {
                                                userColumn = i;
                                            }
                                            if (header[i].ToLowerInvariant().StartsWith("client principal name"))
                                            {
                                                userEmailColumn = i;
                                            }
                                        }
                                        continue;
                                    }

                                    string[] fields = parser.ReadFields();
                                    string userName = fields[userColumn];
                                    string userGuid = fields[userGuidColumn];
                                    string userEmail = fields[userEmailColumn];
                                    string group = string.Empty;

                                    BirstUser birstUser = new BirstUser(userGuid, userEmail, userName, tenantId);

                                    for (int i = 1; i < fields.Length; i++)
                                    {
                                        if (!header[i].ToLowerInvariant().Trim().StartsWith("securityrole"))
                                        {
                                            continue;
                                        }
                                        group = fields[i];
                                        if (string.IsNullOrWhiteSpace(group))
                                        {
                                            continue;
                                        }
                                        if (!groups.Contains(group))
                                        {
                                            if (!string.IsNullOrWhiteSpace(group))
                                            {
                                                bool exists = space.GroupExists(group);
                                                ListViewItem lv = new ListViewItem(group)
                                                {
                                                    Tag = false,
                                                    Checked = !exists
                                                };
                                                if (exists)
                                                {
                                                    lv.Group = ListViewGroups.Groups["sync"];
                                                    lv.ForeColor = existingItemColor;
                                                    lv.Font = boldGreen;
                                                    lv.Tag = true;
                                                }
                                                else
                                                {
                                                    lv.Group = ListViewGroups.Groups["import"];
                                                }
                                                groups.Add(group);

                                                ListViewGroups.Items.Add(lv);
                                            }
                                        }

                                        if (!assignments.ContainsKey(group))
                                        {
                                            assignments.Add(group, new List<string>());
                                        }

                                        assignments[group].Add(birstUser.BirstUserID);
                                    }

                                    StatusProgress.ProgressBar.Value = 33;

                                    if (!users.Contains(birstUser.BirstUserID))
                                    {
                                        if (!string.IsNullOrWhiteSpace(birstUser.BirstUserID))
                                        {
                                            importedUsers.Add(birstUser.BirstUserID.ToLowerInvariant(), birstUser);
                                            users.Add(birstUser.BirstUserID.ToLowerInvariant());
                                            bool exists = space.UserExists(birstUser.BirstUserID);
                                            ListViewItem lv = new ListViewItem(birstUser.UserName);
                                            lv.SubItems.Add(string.Empty);
                                            lv.SubItems.Add(birstUser.BirstUserID);

                                            lv.Tag = false;
                                            lv.Checked = !exists;
                                            if (exists)
                                            {
                                                lv.Group = ListViewUsers.Groups["sync"];

                                                lv.ForeColor = existingItemColor;
                                                lv.Font = boldGreen;
                                                lv.Tag = true;
                                            }
                                            else
                                            {
                                                lv.Group = ListViewUsers.Groups["import"];
                                            }
                                            ListViewUsers.Items.Add(lv);
                                        }
                                    }

                                    StatusProgress.ProgressBar.Value = 66;
                                }

                                foreach (string group in assignments.Keys)
                                {
                                    foreach (string user in assignments[group])
                                    {
                                        bool exists = space.UserInGroup(user, group);

                                        ListViewItem lv = new ListViewItem(importedUsers[user.ToLowerInvariant()].Email)
                                        {
                                            Tag = false
                                        };
                                        lv.SubItems.Add(group);
                                        lv.SubItems.Add(importedUsers[user.ToLowerInvariant()].BirstUserID);

                                        if (exists)
                                        {
                                            lv.Group = ListViewAssignments.Groups["sync"];
                                            lv.ForeColor = existingItemColor;
                                            lv.Font = boldGreen;
                                            lv.Tag = true;
                                        }
                                        else
                                        {
                                            lv.Group = ListViewAssignments.Groups["import"];
                                        }
                                        ListViewAssignments.Items.Add(lv);
                                    }
                                }
                            }

                            // check what is in the space and does not in the import and add it as space only

                            foreach (var user in space.Users)
                            {
                                if (!users.Contains(user.ToLowerInvariant()))
                                {
                                    ListViewItem lv = new ListViewItem(user)
                                    {
                                        Group = ListViewUsers.Groups["space"],
                                        ForeColor = spaceOnlyItemColor,
                                        Font = boldGreen
                                    };
                                    lv.SubItems.Add(""); // todo admin check
                                    lv.SubItems.Add(user);
                                    ListViewUsers.Items.Add(lv);
                                }
                            }

                            foreach (var group in space.Groups)
                            {
                                if (!groups.Contains(group))
                                {
                                    ListViewItem lv = new ListViewItem(group)
                                    {
                                        Group = ListViewGroups.Groups["space"],
                                        ForeColor = spaceOnlyItemColor,
                                        Font = boldGreen
                                    };
                                    ListViewGroups.Items.Add(lv);
                                }
                            }

                            foreach (var group in space.AssignedGroups)
                            {
                                List<string> usersInGroup = space.AssignedUsersInGroups(group);
                                foreach (var user in usersInGroup)
                                {
                                    if (assignments.ContainsKey(group))
                                    {
                                        if (string.IsNullOrWhiteSpace(assignments[group].Find(item => item.ToLowerInvariant().Equals(user.ToLowerInvariant()))))
                                        {
                                            ListViewItem lv = new ListViewItem(user);
                                            lv.SubItems.Add(group);
                                            lv.SubItems.Add(user);
                                            lv.Group = ListViewAssignments.Groups["space"];
                                            lv.ForeColor = spaceOnlyItemColor;
                                            lv.Font = boldGreen;
                                            ListViewAssignments.Items.Add(lv);
                                        }
                                    }
                                    else
                                    {
                                        ListViewItem lv = new ListViewItem(user);
                                        lv.SubItems.Add(group);
                                        lv.SubItems.Add(user);
                                        lv.Group = ListViewAssignments.Groups["space"];
                                        lv.ForeColor = spaceOnlyItemColor;
                                        lv.Font = boldGreen;
                                        ListViewAssignments.Items.Add(lv);
                                    }
                                }
                            }

                            string[] adminNames = { "FSMBirst-SpaceAdministrator", "GHRBirst-SpaceAdministrator" };

                            foreach (string adminName in adminNames)
                            {
                                if (assignments.ContainsKey(adminName))
                                {
                                    foreach (var user in assignments[adminName])
                                    {
                                        foreach (ListViewItem item in ListViewUsers.Items)
                                        {
                                            if (item.SubItems[2].Text.ToLowerInvariant().Equals(user.ToLowerInvariant()))
                                            {
                                                item.SubItems[1].Text = "Yes";
                                            }
                                        }
                                    }
                                }
                                if (space.GroupIsAssigned(adminName))
                                {
                                    foreach (var user in space.AssignedUsersInGroups(adminName))
                                    {
                                        foreach (ListViewItem item in ListViewUsers.Items)
                                        {
                                            if (item.SubItems[2].Text.ToLowerInvariant().Equals(user.ToLowerInvariant()))
                                            {
                                                item.SubItems[1].Text = "Yes";
                                            }
                                        }
                                    }
                                }
                            }
                            StatusProgress.ProgressBar.Value = 100;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                StatusProgress.ProgressBar.Value = 0;
                MessageBox.Show(this, "An error has occured: " + exc.Message);
            }
            finally
            {
                ResumeLayout();
                ListViewUsers.EndUpdate();
                ListViewGroups.EndUpdate();
                ListViewAssignments.EndUpdate();

                ListViewUsers.ResumeLayout();
                ListViewGroups.ResumeLayout();
                ListViewAssignments.ResumeLayout();

                ListViewUsers.Columns[0].Width = -2;
                ListViewUsers.Columns[1].Width = -2;
                ListViewUsers.Columns[2].Width = -2;
                ListViewGroups.Columns[0].Width = -2;
                ListViewAssignments.Columns[0].Width = -2;
                ListViewAssignments.Columns[1].Width = -2;
                ListViewAssignments.Columns[2].Width = -2;

                StatusProgress.ProgressBar.Value = 0;
            }
        }

        #region Methods

        /// <summary>
        /// This method loads the current configuration of the selected space.
        /// </summary>
        /// <returns>The space setup with user, groups and assignments.</returns>
        private SpaceSetup LoadSpaceSetup()
        {
            SpaceSetup spaceSetup = new SpaceSetup
            {
                Users = _birstService.GetUsersInSpace(_spaceId),
                Groups = _birstService.GetGroupsInSpace(_spaceId)
            };
            Dictionary<string, List<string>> assignments = new Dictionary<string, List<string>>();

            foreach (string group in spaceSetup.Groups)
            {
                assignments.Add(group, new List<string>());
            }

            foreach (string user in spaceSetup.Users)
            {
                string[] userGroups = _birstService.GetUserGroupAssignments(_spaceId, user, false);
                foreach (string userGroup in userGroups)
                {
                    if (assignments.ContainsKey(userGroup))
                    {
                        assignments[userGroup].Add(user);
                    }
                }
            }
            spaceSetup.SetAssignments(assignments);
            return spaceSetup;
        }

        /// <summary>
        /// This method loads the space into the UI to fill up the three tables for users, groups, and assignments.
        /// </summary>
        private void LoadSpaceIntoUI()
        {
            try
            {
                string[] users = _birstService.GetUsersInSpace(_spaceId);
                string[] groups = _birstService.GetGroupsInSpace(_spaceId);

                StatusProgress.ProgressBar.Value = 0;

                SuspendLayout();

                ListViewUsers.SuspendLayout();
                ListViewGroups.SuspendLayout();
                ListViewAssignments.SuspendLayout();

                ListViewUsers.BeginUpdate();
                ListViewGroups.BeginUpdate();
                ListViewAssignments.BeginUpdate();

                ListViewUsers.Items.Clear();
                ListViewGroups.Items.Clear();
                ListViewAssignments.Items.Clear();

                foreach (string user in users)
                {
                    ListViewItem lv = ListViewUsers.Items.Add(user);
                    lv.SubItems.Add(""); // TODO need to read if user is admin
                    lv.SubItems.Add(user);
                }
                ListViewUsers.Columns[0].Width = -2;

                StatusProgress.ProgressBar.Value = 33;

                foreach (string group in groups)
                {
                    ListViewGroups.Items.Add(group);
                }

                ListViewGroups.Columns[0].Width = -2;

                StatusProgress.ProgressBar.Value = 66;

                foreach (string user in users)
                {
                    string[] assignments = _birstService.GetUserGroupAssignments(_spaceId, user, false);
                    foreach (string assignment in assignments)
                    {
                        ListViewItem lv = new ListViewItem(user);
                        lv.SubItems.Add(assignment);
                        lv.SubItems.Add(user);
                        ListViewAssignments.Items.Add(lv);
                    }
                }

                StatusProgress.ProgressBar.Value = 100;
            }
            catch (Exception exc)
            {
                MessageBox.Show(this, "An error has occured: " + exc.Message);
            }
            finally
            {
                ListViewUsers.EndUpdate();
                ListViewGroups.EndUpdate();
                ListViewAssignments.EndUpdate();

                ListViewUsers.ResumeLayout();
                ListViewGroups.ResumeLayout();
                ListViewAssignments.ResumeLayout();

                ResumeLayout();

                ListViewUsers.Columns[0].Width = -2;
                ListViewUsers.Columns[1].Width = -2;
                ListViewUsers.Columns[2].Width = -2;
                ListViewGroups.Columns[0].Width = -2;
                ListViewAssignments.Columns[0].Width = -2;
                ListViewAssignments.Columns[1].Width = -2;
                ListViewAssignments.Columns[2].Width = -2;

                StatusProgress.ProgressBar.Value = 0;
            }
        }

        private void FilterSelectedGroups()
        {
            if (MessageBox.Show(this, "Remove the selected groups and corresponding user assigments?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                List<string> removeGroups = new List<string>();
                List<ListViewItem> removeItems = new List<ListViewItem>();

                foreach (ListViewItem item in ListViewGroups.Items)
                {
                    if (item.Checked)
                    {
                        removeGroups.Add(item.Text);
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }

                foreach (ListViewItem item in ListViewAssignments.Items)
                {
                    if (removeGroups.Contains(item.SubItems[1].Text))
                    {
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }
            }
        }

        private void FilterDeselectedGroups()
        {
            if (MessageBox.Show(this, "Remove the deselected groups and corresponding user assigments?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                List<string> removeGroups = new List<string>();
                List<ListViewItem> removeItems = new List<ListViewItem>();

                foreach (ListViewItem item in ListViewGroups.Items)
                {
                    if (!item.Checked)
                    {
                        removeGroups.Add(item.Text);
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }

                foreach (ListViewItem item in ListViewAssignments.Items)
                {
                    if (removeGroups.Contains(item.SubItems[1].Text))
                    {
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }
            }
        }

        private void FilterSelectedUsers()
        {
            if (MessageBox.Show(this, "Remove the selected users and corresponding group assigments?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                List<string> removeUsers = new List<string>();
                List<ListViewItem> removeItems = new List<ListViewItem>();

                foreach (ListViewItem item in ListViewUsers.Items)
                {
                    if (item.Checked)
                    {
                        removeUsers.Add(item.Text);
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }

                foreach (ListViewItem item in ListViewAssignments.Items)
                {
                    if (removeUsers.Contains(item.SubItems[0].Text))
                    {
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }
            }
        }

        private void FilterDeselectedUsers()
        {
            if (MessageBox.Show(this, "Remove the deselected users and corresponding group assigments?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                List<string> removeUsers = new List<string>();
                List<ListViewItem> removeItems = new List<ListViewItem>();

                foreach (ListViewItem item in ListViewUsers.Items)
                {
                    if (!item.Checked)
                    {
                        removeUsers.Add(item.Text);
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }

                foreach (ListViewItem item in ListViewAssignments.Items)
                {
                    if (removeUsers.Contains(item.SubItems[0].Text))
                    {
                        removeItems.Add(item);
                    }
                }

                while (removeItems.Count > 0)
                {
                    removeItems[0].Remove();
                    removeItems.RemoveAt(0);
                }
            }
        }

        private void UpdateSpace()
        {
            File.Delete(_logFilePath);

            bool errors = false;
            Cursor currentCursor = Cursor;

            try
            {
                Cursor = Cursors.WaitCursor;
                string user = string.Empty;
                string group = string.Empty;

                StatusProgress.ProgressBar.Value = 0;

                if (CheckboxUploadUsers.Checked)
                {
                    foreach (ListViewItem item in ListViewUsers.Items)
                    {
                        if (!item.Checked)
                        {
                            continue;
                        }
                        try
                        {
                            user = item.SubItems[2].Text;
                            BirstUser birstUser = importedUsers[user.ToLowerInvariant()];
                            if (_birstService.BirstUserExists(birstUser.BirstUserID))
                            {
                                CreateLogEntry(Success, CreateUser, user);
                            }
                            else
                            {
                                _birstService.CreateBirstUser(birstUser.BirstUserID, "", birstUser.Email);
                                CreateLogEntry(Success, CreateUser, user);
                                _birstService.SetUserDashboardType(birstUser.BirstUserID, (dropdownDashboard.SelectedItem as DasboardType).Id);
                                CreateLogEntry(Success, "Set dashboard view to", (dropdownDashboard.SelectedItem as DasboardType).Id);
                            }
                        }
                        catch (Exception exc)
                        {
                            errors = true;
                            CreateLogEntry(Error, CreateUser, user + " - Message: " + exc.Message);
                        }

                        try
                        {
                            user = item.SubItems[2].Text;
                            _birstService.AddUserToSpace(_spaceId, user, item.SubItems[1].Text.ToLowerInvariant().Equals("yes"));
                            item.Group = ListViewUsers.Groups["sync"];
                            item.ForeColor = existingItemColor;
                            CreateLogEntry(Success, AddSpaceUser, user);
                        }
                        catch (Exception exc)
                        {
                            errors = true;
                            CreateLogEntry(Error, AddSpaceUser, user + " - Message: " + exc.Message);
                        }
                    }
                }

                StatusProgress.ProgressBar.Value = 33;

                if (CheckboxUploadGroups.Checked)
                {
                    foreach (ListViewItem item in ListViewGroups.Items)
                    {
                        if (!item.Checked)
                        {
                            continue;
                        }
                        group = item.Text;
                        if (!_birstService.GroupExistsInSpace(_spaceId, group))
                        {
                            try
                            {
                                _birstService.AddGroupToSpace(_spaceId, group);
                                item.Group = ListViewGroups.Groups["sync"];
                                item.ForeColor = existingItemColor;
                                CreateLogEntry(Success, AddSpaceGroup, group);
                            }
                            catch (Exception exc)
                            {
                                errors = true;
                                CreateLogEntry(Success, AddSpaceGroup, group + " - Message: " + exc.Message);
                            }
                        }
                    }
                }

                StatusProgress.ProgressBar.Value = 66;

                if (CheckboxUploadAssignments.Checked)
                {
                    foreach (ListViewItem item in ListViewAssignments.Items)
                    {
                        try
                        {
                            user = item.SubItems[2].Text;
                            group = item.SubItems[1].Text;

                            _birstService.AddUserToGroupInSpace(_spaceId, user, group);
                            item.Group = ListViewAssignments.Groups["sync"];
                            item.ForeColor = existingItemColor;
                            CreateLogEntry(Success, AddUserToGroup, user + "," + group);
                        }
                        catch (Exception exc)
                        {
                            errors = true;
                            CreateLogEntry(Error, AddUserToGroup, user + ", " + group + " - Message: " + exc.Message);
                        }
                    }
                }

                StatusProgress.ProgressBar.Value = 100;
                Cursor = currentCursor;
            }
            catch (Exception exc)
            {
                Cursor = currentCursor;
                CreateLogEntry(Error, GeneralError, "Message: " + exc.Message);
            }

            StatusProgress.ProgressBar.Value = 0;

            if (errors)
            {
                if (MessageBox.Show(this, "Errors have occured during the updates. Open the log file?", "Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(_logFilePath);
                }
            }
        }

        private void CreateLogEntry(string type, string action, string message)
        {
            StringBuilder logMessage = new StringBuilder();
            logMessage.Append(DateTime.Now.ToShortTimeString());
            logMessage.Append("\t");
            logMessage.Append(type);
            logMessage.Append("\t");
            logMessage.Append(action);
            logMessage.Append("\t");
            logMessage.Append(message);
            logMessage.Append("\r\n");

            File.AppendAllText(_logFilePath, logMessage.ToString());
        }

        #endregion Methods

        #region Events

        private void ButtonFilterUser_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            UserMenu.Show(ptLowerLeft);
        }

        private void ButtonFilterGroups_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            GroupMenu.Show(ptLowerLeft);
        }

        private void ButtonUpdateSpace_Click(object sender, EventArgs e)
        {
            string message = "The space {0} will be updated using the following options:\r\n\r\n  - Users will{1} be added. \r\n  - Groups will{2} be added. \r\n  - User to group assignments will{3} be added. \r\n\r\nDo you want to proceed?";

            message = string.Format(message, _space, CheckboxUploadUsers.Checked ? "" : " not", CheckboxUploadGroups.Checked ? "" : " not", CheckboxUploadAssignments.Checked ? "" : " not");

            if (MessageBox.Show(this, message, "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    EnsureConnection();
                    UpdateSpace();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(this, "An error has occured: " + exc.Message);
                }
            }
        }

        private void SelectAllUsers_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewUsers.Items)
            {
                item.Checked = true;
            }
        }

        private void DeselectAllUsers_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewUsers.Items)
            {
                item.Checked = false;
            }
        }

        private void SelectExistingUsers_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewUsers.Items)
            {
                if (item.Tag is bool)
                {
                    if (((bool)item.Tag))
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        private void SelectNonexistingUsers_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewUsers.Items)
            {
                if (item.Tag is bool)
                {
                    if (!((bool)item.Tag))
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        private void RemoveSelectedUsers_Click(object sender, EventArgs e)
        {
            FilterSelectedUsers();
        }

        private void SelectAllGroups_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewGroups.Items)
            {
                item.Checked = true;
            }
        }

        private void DeselectAllGroups_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewGroups.Items)
            {
                item.Checked = false;
            }
        }

        private void SelectExistingGroups_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewGroups.Items)
            {
                if (item.Tag is bool)
                {
                    if (((bool)item.Tag))
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        private void SelectNonexistingGroups_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ListViewGroups.Items)
            {
                if (item.Tag is bool)
                {
                    if (!((bool)item.Tag))
                    {
                        item.Checked = true;
                    }
                }
            }
        }

        private void RemoveSelectedGroups_Click(object sender, EventArgs e)
        {
            FilterSelectedGroups();
        }

        private void About_Click(object sender, EventArgs e)
        {
            string message = "Analytics Configuration and User provisioning for Birst.";
            MessageBox.Show(this, message, "About", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void RemoveDeselectedUsers_Click(object sender, EventArgs e)
        {
            FilterDeselectedUsers();
        }

        private void RemoveDeselectedGroups_Click(object sender, EventArgs e)
        {
            FilterDeselectedGroups();
        }

        #endregion Events

        private void ButtonExportFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog
            {
                CheckFileExists = false,
                RestoreDirectory = true
            })
            {
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    int maxLineCount = 0;

                    if (File.Exists(sfd.FileName))
                    {
                        File.Delete(sfd.FileName);
                    }

                    foreach (ListViewItem lv in ListViewUsers.Items)
                    {
                        if (!lv.Checked)
                        {
                            continue;
                        }
                        int currentLineCount = 1;
                        foreach (ListViewItem group in ListViewAssignments.Items)
                        {
                            if (group.Text.Equals(lv.Text))
                            {
                                currentLineCount++;
                            }
                        }
                        if (maxLineCount < currentLineCount)
                        {
                            maxLineCount = currentLineCount;
                        }
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append("User Name,");
                    sb.Append("EmailId,");
                    sb.Append("UserGuid");

                    for (int i = 0; i < maxLineCount; i++)
                    {
                        sb.Append(",SecurityRole" + (i + 1));
                    }
                    sb.Append("\r\n");

                    File.AppendAllText(sfd.FileName, sb.ToString());

                    foreach (ListViewItem lv in ListViewUsers.Items)
                    {
                        if (!lv.Checked)
                        {
                            continue;
                        }
                        List<string> line = new List<string>();
                        string user = lv.Text;
                        string id = lv.SubItems[2].Text;
                        string tenant = "";
                        string idguid = "";

                        if (id.Contains("_"))
                        {
                            string[] idparts = id.Split('_');
                            tenant = idparts[0];
                            idguid = idparts[1];
                        }
                        line.Add(user);
                        line.Add(user);
                        line.Add(idguid);

                        foreach (ListViewItem group in ListViewAssignments.Items)
                        {
                            if (group.Text.Equals(user))
                            {
                                line.Add(group.SubItems[1].Text);
                            }
                        }

                        sb = new StringBuilder();
                        for (int i = 0; i < line.Count; i++)
                        {
                            sb.Append(line[i]);
                            if (i < maxLineCount - 1)
                            {
                                sb.Append(",");
                            }
                        }

                        for (int i = line.Count - 1; i < maxLineCount; i++)
                        {
                            sb.Append((i == maxLineCount - 1) ? "" : ",");
                        }
                        sb.Append("\r\n");
                        File.AppendAllText(sfd.FileName, sb.ToString());
                    }
                }
            }
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}