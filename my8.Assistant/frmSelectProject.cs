using my8.Assistant.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my8.Assistant
{
    public partial class frmSelectProject : Form
    {
        public frmSelectProject()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ThisApp.Project = new Project();
            if (rdClient.Checked)
            {
                ThisApp.Project.Id = 1;
                ThisApp.Project.ProjectName = "client";
            }
            if (rdApi.Checked)
            {
                ThisApp.Project.Id = 2;
                ThisApp.Project.ProjectName = "api";
            }
            Utility.WriteToFileInAppData(Utility.LastProjectFile, ThisApp.Project.ProjectName.ToLower());
            ThisApp.AppSetting = Utility.GetCurrentSetting(ThisApp.Project.ProjectName);
            ThisApp.currentSession = ThisApp.getSessionByDbType(DatabaseType.SQL);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmSelectProject_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
        }

        private void frmSelectProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ThisApp.Project.ProjectName == "my8")
            {
                Application.Exit();
            }
        }

        private void frmSelectProject_Load(object sender, EventArgs e)
        {
            string lastProject = Utility.ReadAppDataFile(Utility.LastProjectFile);
            if (lastProject == "api")
            {
                rdApi.Checked = true;
            }
            else
                rdClient.Checked = true;
        }
    }
}
