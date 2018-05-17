using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoControl;
using my8.Assistant.Model;

namespace my8.Assistant
{
    public partial class frmSetupAppClient : Form
    {
        ApplicationSetting AppSetting = null;
        ApplicationSession Session = null;
        public frmSetupAppClient()
        {
            AppSetting = ThisApp.AppSetting;
            InitializeComponent();
            lblNotify.Text = "";
        }


        public delegate void delgUpdateSession();
        public delgUpdateSession updateSession;
        private void btnSave_Click(object sender, EventArgs e)
        {
            AppSetting = new ApplicationSetting();
            this.ToEntity(AppSetting);
            AppSetting.ProjectName = ThisApp.Project.ProjectName;
            Utility.WriteAppSetting(AppSetting);
            Session = new ApplicationSession();
            if(rdSql.Checked)
            {
                Session.DbType = DatabaseType.SQL;
            }
            if (rdMongo.Checked)
            {
                Session.DbType = DatabaseType.Mongo;
            }
            if (rdNeo.Checked)
            {
                Session.DbType = DatabaseType.Neo;
            }
            groupBox2.ToEntity(Session);
            Session.ProjectName = ThisApp.Project.ProjectName;
            Utility.WriteSession(Session);
            ThisApp.currentSession = Session;
            ThisApp.AppSetting = AppSetting;
            lblNotify.SetText("Thành công", AutoControl.LabelNotify.EnumStatus.Success);
            updateSession?.Invoke();
        }
        private void HiddenControl(Control parent)
        {

            foreach (Control c in parent.Controls)
            {
                if (c.Tag != null && c.Tag.ToString() == "Api")
                {
                        c.Enabled = ThisApp.Project.ProjectName.ToLower()=="client"?false:true;
                        if (c.Controls.Count > 0)
                        {
                            HiddenControl(c);
                        }
                }
            }
        }
        DatabaseType m_dbType;
        private void frmSetupApplication_Load(object sender, EventArgs e)
        {
            HiddenControl(this);
            if (ThisApp.AppSetting == null) return;
            this.ToForm(AppSetting);
            
            if (rdSql.Checked)
                m_dbType = DatabaseType.SQL;
            if (rdMongo.Checked)
                m_dbType = DatabaseType.Mongo;
            if (rdNeo.Checked)
                m_dbType = DatabaseType.Neo;
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            groupBox2.ToForm(ThisApp.currentSession);
        }

        private void btnModelPFolder_Click(object sender, EventArgs e)
        {
            txtModelFolder.Text = Utility.GetFolderPath();
        }

        private void btnUowTemplate_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDialog.FileName;
                }
            }
        }

        private void btnUOWFilePath_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDialog.FileName;
                }
            }
        }

        private void btnIUowFilePath_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDialog.FileName;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void label22_Click(object sender, EventArgs e)
        {
            string message = @"public CustomerRepository CustomerRepository
        {
            get { return _customerRepository ?? (_customerRepository = new CustomerRepository(_transaction,< it is Here>)); }
        }";
            MessageBox.Show(message);
        }

        private void label31_Click(object sender, EventArgs e)
        {
            Process.Start("Note.png");
        }

        private void btnControllerTemplate_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDialog.FileName;
                }
            }
        }

        private void btnControllerFolder_Click(object sender, EventArgs e)
        {
            txtControllerFolder.Text = Utility.GetFolderPath();
        }

        private void btnViewFolder_Click(object sender, EventArgs e)
        {
        }
        private void rdApi_Click(object sender, EventArgs e)
        {
            HiddenControl(this);
        }

        private void rdClient_Click(object sender, EventArgs e)
        {
            HiddenControl(this);
        }

        private void btnUnitTestTemplate_Click(object sender, EventArgs e)
        {
            autoTextBox5.Text = Utility.GetFilePath();
        }

        private void btnReactModelFolder_Click(object sender, EventArgs e)
        {
            txtReactFile.Text = Utility.GetFolderPath();
        }

        private void btnUnitTestFolder_Click(object sender, EventArgs e)
        {
            autoTextBox2.Text = Utility.GetFolderPath();
        }

        
        private void button8_Click(object sender, EventArgs e)
        {
            autoTextBox14.Text = Utility.GetFilePath();
        }

        private void rdSql_Click(object sender, EventArgs e)
        {
            m_dbType = DatabaseType.SQL;
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            groupBox2.ToForm(ThisApp.currentSession);
        }

        private void rdMongo_Click(object sender, EventArgs e)
        {
            m_dbType = DatabaseType.Mongo;
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            groupBox2.ToForm(ThisApp.currentSession);
        }

        private void rdNeo_Click(object sender, EventArgs e)
        {
            m_dbType = DatabaseType.Neo;
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            groupBox2.ToForm(ThisApp.currentSession);
        }

        
    }
}
