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
            ThisApp.Project = cbbProjectList.SelectedItem as Project;
            //if (rdClient.Checked)
            //{
            //    ThisApp.Project.Id = 1;
            //    ThisApp.Project.Name = "client";
            //}
            //if (rdApi.Checked)
            //{
            //    ThisApp.Project.Id = 2;
            //    ThisApp.Project.Name = "api";
            //}
            Utility.UpdateLastSelectProject(ThisApp.Project);
            ThisApp.AppSetting = Utility.GetCurrentSetting(ThisApp.Project.Name);
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
            if (ThisApp.Project.Name == "my8")
            {
                Application.Exit();
            }
        }

        private void frmSelectProject_Load(object sender, EventArgs e)
        {
            List<Project> lstProject = Utility.GetAllProject();
            cbbProjectList.DataSource = lstProject;
            cbbProjectList.ValueMember = "Id";
            cbbProjectList.DisplayMember = "Name";
            Project last = Utility.GetLastSelectedProject();
            if (last != null)
            {
                cbbProjectList.SelectedValue = last.Id;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreateProject frmCreate = new frmCreateProject();
            frmCreate.ShowDialog();
            List<Project> lstProject = Utility.GetAllProject();
            cbbProjectList.DataSource = lstProject;
            cbbProjectList.ValueMember = "Id";
            cbbProjectList.DisplayMember = "Name";
        }
    }
}
