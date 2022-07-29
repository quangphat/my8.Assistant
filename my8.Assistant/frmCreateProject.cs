using AutoControl;
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
    public partial class frmCreateProject : Form
    {
        Project _project;
        private ProjectBusiness _bizProject;
        private AppSettingBusiness _bizSettings;
        private void Init()
        {
            InitializeComponent();
            _bizProject = new ProjectBusiness();
            _bizSettings = new AppSettingBusiness();
        }
        public frmCreateProject()
        {
            Init();
            _project = new Project();
            
        }
        public frmCreateProject(Project project)
        {
            Init();
            this.rdProjectTypeApi.Visible = false;
            this.rdProjectTypeClient.Visible = false;
            _project = new Project
            {
                Id = 0,
                Name = string.Empty,
                IsLastSelect = true,
                Type = project.Type,
                CloneId = project.Id
            };
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateProject();
            this.Close();
        }
        

        private void txtProjectName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CreateProject();
                this.Close();
            }
        }

        private void frmCreateProject_Load(object sender, EventArgs e)
        {
            if (_project.Type == "client")
                rdProjectTypeClient.Checked = true;
            else rdProjectTypeApi.Checked = true;
        }
        private void CreateProject()
        {
            _project.Id = 0;
            this.ToEntity(_project);
            _bizProject.CreateProject(_project);
            if (_project.CloneId.HasValue && _project.CloneId.Value > 0)
            {
                var config = _bizSettings.GetCurrentSetting(_project.CloneId.Value);
                if (config != null)
                {
                    config.ProjectId = _project.Id;
                    config.ProjectName = _project.Name;
                    _bizSettings.WriteAppSetting(config);
                }
            }
            
        }
    }
}
