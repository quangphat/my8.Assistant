using AutoControl;
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
        Project project;
        public frmCreateProject()
        {
            InitializeComponent();
            project = new Project();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            CreateProject();
            this.Close();
        }
        private void CreateProject()
        {
            project.Id = 0;
            this.ToEntity(project);
            Utility.WriteProjects(project);
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
            if (project.Type == "client")
                autoMetroRadio1.Checked = true;
            else autoMetroRadio2.Checked = true;
        }
    }
}
