using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace Infor.FSCM.Analytics
{

    public partial class ImportUserGroups : Form
    {
        public class A
        {
            public string Identity { get; set; }
            public string Identity2 { get; set; }
            public string WindowsAccountName { get; set; }
            public string UserPrincipalName { get; set; }
            public string ClientPrincipalName { get; set; }
            public string CommonName { get; set; }
            public string EmailAddress { get; set; }
            public string Status { get; set; }
        }

        public class B
        {
            public string AttributeServiceCaller1 { get; set; }
            public string CalleroftheIFSAttributeService1 { get; set; }
            public string INFORBCionservice1 { get; set; }
        }

        public class C
        {
            public string Identity12 { get; set; }
            public string Identity22 { get; set; }
            public string UserPrincipalName2 { get; set; }
            public string ClientPrincipalName2 { get; set; }
            public string CommonName2 { get; set; }
            public string AttributeServiceCaller2 { get; set; }
            public string CalleroftheIFSAttributeService2 { get; set; }
            public string INFORBCionservice2 { get; set; }
            public string EmailAddress2 { get; set; }
            public string Status2 { get; set; }
        }

        public const string V = "\\Temp.csv";
        public List<A> UserFileData = new List<A>();
        public string[] UserFileArray;
        public List<B> RolesFileData = new List<B>();
        public List<C> UserRolesFile = new List<C>();
        public StringBuilder dataToBeWritten;
        public StringBuilder Netfile;
        public FileStream fs;
        public string TempFile
        {
            get
            {
                //Console.WriteLine("The file name is: " + V);
                //Console.WriteLine(TextboxSelectedFile.Text);
                string c = Path.GetDirectoryName(UserFilename).ToString() + V;
                    Console.WriteLine("The directory is:" + @c);
                return @c;
                //return "C:\\Users\\shari1\\OneDrive - Infor\\Desktop\\Temp.csv";
            }
        }
        public ImportUserGroups()
        {
            InitializeComponent();
        }

        public string UserFilename
        {
            get
            {
                return TextboxSelectedFile.Text;
            }
        }

        public string RolesFileName
        {
            get
            {
                return TextboxSelectedRolesFile.Text;
            }
        }
        public bool UseTenantSchema
        {
            get
            {
                return CheckBoxGuidUsersId.Checked;
            }
        }

        public string TenantID
        {
            get
            {
                return TextboxTenant.Text;
            }
        }

        private void ButtonSelectModelSpaceGold_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd1 = new OpenFileDialog
            {
                CheckFileExists = true,
                DefaultExt = "xml",
                RestoreDirectory = true
            })
            {
                if (ofd1.ShowDialog(this) == DialogResult.OK)
                {
                    TextboxSelectedFile.Text = ofd1.FileName;
                    TextboxSelectedFile.Refresh();
                    FileConvert_XMLToCSV(TextboxSelectedFile.Text);
                }
            }
        }
        private void ButtonSelectRolesFile_Click(object sender, EventArgs e)
        {
            if (TextboxSelectedFile.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "An error has occured. Enter the user file first.");
                return;
            }
            else
            {
                using (OpenFileDialog ofd2 = new OpenFileDialog
                {
                    CheckFileExists = true,
                    DefaultExt = "csv",
                    RestoreDirectory = true
                })
                {
                    if (ofd2.ShowDialog(this) == DialogResult.OK)
                    {
                        TextboxSelectedRolesFile.Text = ofd2.FileName;
                        TextboxSelectedRolesFile.Refresh();
                        DisplayCSVProbe();
                        CreateTemporaryFile();
                        //fs.Close();
                    }
                }
            }
        }

        public void FileConvert_XMLToCSV(string UserFile)
        {
            //This method converts an xml file into a .csv file

            XDocument xDocument = XDocument.Load(UserFile);
            dataToBeWritten = new StringBuilder();


            var results = xDocument.Descendants("User").Select(x => new
            {
                identity = (string)x.Element("Identity"),
                identity2 = (string)x.Element("Identity2"),
                windowsAccountName = (string)x.Element("WindowsAccountName"),
                userPrincipalName = (string)x.Element("UserPrincipalName"),
                clientPrincipalName = (string)x.Element("ClientPrincipalName"),
                commonName = (string)x.Element("CommonName"),
                emailAddress = (string)x.Element("EmailAddress"),
                status = (string)x.Element("Status")
            }).ToList();

            for (int i = 0; i < results.Count; i++)
            {
                string tempidentity = results[i].identity;
                string tempidentity2 = results[i].identity2;
                string tempwindowsAccountName = results[i].windowsAccountName;
                string tempuserPrincipalName = results[i].userPrincipalName;
                string tempclientPrincipalName = results[i].clientPrincipalName;
                string tempcommonName = results[i].commonName;
                string tempemailAddress = results[i].emailAddress;
                string tempstatus = results[i].status;

                dataToBeWritten.Append(tempidentity);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempidentity2);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempwindowsAccountName);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempuserPrincipalName);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempclientPrincipalName);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempcommonName);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempemailAddress);
                dataToBeWritten.Append(";");
                dataToBeWritten.Append(tempstatus);
                dataToBeWritten.Append(Environment.NewLine);
                UserFileData.Add(new A
                {
                    Identity = tempidentity,
                    Identity2 = tempidentity2,
                    WindowsAccountName = tempwindowsAccountName,
                    UserPrincipalName = tempuserPrincipalName,
                    ClientPrincipalName = tempclientPrincipalName,
                    CommonName = tempcommonName,
                    EmailAddress = tempemailAddress,
                    Status = tempstatus
                });
            }

            var testpath = AppDomain.CurrentDomain.BaseDirectory + @"TestUsers.csv";

            File.WriteAllText(testpath, dataToBeWritten.ToString());
        }

        private string CreateTemporaryFile()
        {
            try
            {
                //Path = @"C:\Users\shari1\OneDrive - Infor\Desktop\MyTest.csv";

                using (StreamWriter sw = new StreamWriter(TempFile))
                {
                    sw.WriteLine(Netfile.ToString());
                }
            }
            catch (IOException)
            {
                ;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create TEMP file or set its attributes: " + ex.Message);
            }
            return TempFile;
        }

        /// <summary>
        /// Displays the first portion of the CSV file.
        /// </summary>

        private void DisplayCSVProbe()
        {
            int maxRows = 20;
            //string[] fields=null;
            try
            {
                using (Microsoft.VisualBasic.FileIO.TextFieldParser parser = new Microsoft.VisualBasic.FileIO.TextFieldParser(TextboxSelectedRolesFile.Text))
                {

                    SuspendLayout();
                    ListviewCSV.BeginUpdate();
                    ListviewCSV.SuspendLayout();
                    ListviewCSV.Items.Clear();

                    parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
                    parser.SetDelimiters(",");
                    bool firstRow = true;

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (firstRow)
                        {
                            for (int k = 0; k < fields.Length; k++)
                            {
                                ListviewCSV.Columns.Add((k + 1).ToString());
                            }
                            firstRow = false;
                        }
                    }

                    using (StreamReader file = new StreamReader(TextboxSelectedRolesFile.Text))
                    {
                        string templine;
                        while ((templine = file.ReadLine()) != null)
                        {
                            string[] templinearray = templine.Split(',');
                            RolesFileData.Add(new B
                            {
                                AttributeServiceCaller1 = templinearray[0],
                                CalleroftheIFSAttributeService1 = templinearray[1],
                                INFORBCionservice1 = templinearray[2]
                            }
                                          );
                            UserRolesFile.Add(new C
                            {
                                AttributeServiceCaller2 = templinearray[0],
                                CalleroftheIFSAttributeService2 = templinearray[1],
                                INFORBCionservice2 = templinearray[2]
                            }
                            );
                        }
                    }


                    for (int i = 0; i < UserRolesFile.Count; i++)
                    {
                        for (int j = 0; j < UserFileData.Count; j++)
                        {
                            if (UserRolesFile[i].INFORBCionservice2.ToLower().Trim() == UserFileData[j].WindowsAccountName.ToLower().Trim())
                            {
                                UserRolesFile[i].EmailAddress2 = UserFileData[j].EmailAddress;
                                UserRolesFile[i].Identity12 = UserFileData[j].Identity;
                                UserRolesFile[i].Identity22 = UserFileData[j].Identity2;
                                UserRolesFile[i].UserPrincipalName2 = UserFileData[j].UserPrincipalName;
                                UserRolesFile[i].ClientPrincipalName2 = UserFileData[j].ClientPrincipalName;
                                UserRolesFile[i].CommonName2 = UserFileData[j].CommonName;
                                UserRolesFile[i].Status2 = UserFileData[j].Status;

                            }
                        }
                    }
                    Netfile = new StringBuilder();
                    Netfile.Append("Name");
                    Netfile.Append(",");
                    Netfile.Append("SecurityRole");
                    Netfile.Append(",");
                    Netfile.Append("RoleDescription");
                    Netfile.Append(",");
                    Netfile.Append("EmailID");
                    Netfile.Append(",");
                    Netfile.Append("Identity1");
                    Netfile.Append(",");
                    Netfile.Append("UserGUID");
                    Netfile.Append(",");
                    Netfile.Append("User Name");
                    Netfile.Append(",");
                    Netfile.Append("Client Principal Name");
                    Netfile.Append(",");
                    Netfile.Append("Common Name");
                    Netfile.Append(",");
                    Netfile.Append("Status");
                    Netfile.Append(Environment.NewLine);

                    for (int p = 0; p < UserRolesFile.Count; p++)
                    {
                        if (UserRolesFile[p].INFORBCionservice2.Trim() != null)
                        {
                            Netfile.Append(UserRolesFile[p].INFORBCionservice2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].AttributeServiceCaller2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].CalleroftheIFSAttributeService2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].EmailAddress2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].Identity12);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].Identity22);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].UserPrincipalName2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].ClientPrincipalName2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].CommonName2);
                            Netfile.Append(",");
                            Netfile.Append(UserRolesFile[p].Status2);
                            Netfile.Append(Environment.NewLine);
                        }
                    }



                }




                for (int l = 0; l <= UserRolesFile.Count; l++)
                {
                    ListViewItem item = new ListViewItem(UserRolesFile[l].INFORBCionservice2);
                    item.SubItems.Add(UserRolesFile[l].AttributeServiceCaller2);
                    item.SubItems.Add(UserRolesFile[l].CalleroftheIFSAttributeService2);
                    item.SubItems.Add(UserRolesFile[l].EmailAddress2);
                    item.SubItems.Add(UserRolesFile[l].Identity12);
                    item.SubItems.Add(UserRolesFile[l].Identity22);
                    item.SubItems.Add(UserRolesFile[l].UserPrincipalName2);
                    item.SubItems.Add(UserRolesFile[l].ClientPrincipalName2);
                    item.SubItems.Add(UserRolesFile[l].CommonName2);
                    item.SubItems.Add(UserRolesFile[l].Status2);
                    ListviewCSV.Items.Add(item);
                    if (maxRows-- < 0)
                    {
                        break;
                    }
                }



                for (int i = 0; i < ListviewCSV.Columns.Count; i++)
                {
                    ListviewCSV.Columns[i].Width = -2;
                }

                ListviewCSV.ResumeLayout();
                ListviewCSV.EndUpdate();
                ResumeLayout();

            }
            catch
            {
                ListviewCSV.ResumeLayout();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            if (CheckBoxGuidUsersId.Checked && string.IsNullOrWhiteSpace(TextboxTenant.Text))
            {
                MessageBox.Show(this, "You must provide the tenant ID, if you have selected the tenant schema options.");
                return;
            }
            DialogResult = DialogResult.OK;
        }
    }
}