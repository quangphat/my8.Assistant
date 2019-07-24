using my8.Assistant.Business;
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
        private ProjectBusiness _bizProject;
        private AppSettingBusiness _bizAppSetting;
        private SessionBusiness _bizSession;
        public frmSelectProject()
        {
            InitializeComponent();
            _bizProject = new ProjectBusiness();
            _bizAppSetting = new AppSettingBusiness();
            _bizSession = new SessionBusiness();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ThisApp.Project = cbbProjectList.SelectedItem as Project;
            _bizProject.UpdateLastSelectProject(ThisApp.Project);
            ThisApp.AppSetting = _bizAppSetting.GetCurrentSetting(ThisApp.Project.Id);
            
            ThisApp.currentSession = _bizSession.GetSessionByDbType( ThisApp.Project.Id);
            
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
            List<Project> lstProject = _bizProject.GetAll();
            if (lstProject == null) return;
            cbbProjectList.DataSource = lstProject;
            cbbProjectList.ValueMember = "Id";
            cbbProjectList.DisplayMember = "Name";
            Project last = lstProject.FirstOrDefault(p=>p.IsLastSelect==true);
            if (last != null)
            {
                cbbProjectList.SelectedValue = last.Id;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreateProject frmCreate = new frmCreateProject();
            frmCreate.ShowDialog();
            List<Project> lstProject = _bizProject.GetAll();
            if (lstProject == null) return;
            cbbProjectList.DataSource = lstProject;
            cbbProjectList.ValueMember = "Id";
            cbbProjectList.DisplayMember = "Name";
        }
    }
}
