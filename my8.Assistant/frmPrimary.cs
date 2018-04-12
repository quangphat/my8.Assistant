using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using AutoControl;
using ModernUI.Controls;
using Microsoft.SqlServer.Management.Smo;
using Model = my8.Assistant.Model;
using my8.Assistant.Model;

namespace my8.Assistant
{
    public partial class frmPrimary : Form
    {
        public List<Model.Table> m_lstSqlTable;
        public List<Model.Table> m_lstMongoCollection;
        public  List<Model.Table> m_lstNeoNode;
        Model.DatabaseHelper m_dbhelper;
        Generator m_Generator;
        DatabaseType m_dbType;
        Model.Table m_Table;
        public frmPrimary()
        {
            InitializeComponent();
            lblNotify.Text = "";
            rdRepository.Click += rdRepository_Click;
            rdEntityclass.Click += rdEntityclass_Click;
            rdInterface.Click += rdInterface_Click;
            rduow.Click += rduow_Click;
            rdIuow.Click += rdIuow_Click;
            rdSSelect.Click += rdSSelect_Click;
            rdInsert.Click += rdInsert_Click;
            rdDelete.Click += rdDelete_Click;
            rdUpdate.Click += rdUpdate_Click;
            foreach (Control c in groupBox2.Controls)
            {
                if (c is AutoMetroCheckBox)
                {
                    AutoMetroCheckBox cb = c as AutoMetroCheckBox;
                    cb.Cursor = Cursors.Hand;
                    cb.CheckedChanged += cb_CheckedChanged;
                }
            }
        }

        private void GetCurrentTable()
        {
            m_Table = cbbTable.SelectedItem as Model.Table;
            if (m_Table == null)
            {
                if (!string.IsNullOrEmpty(cbbTable.Text))
                {
                    m_Table = new Model.Table();
                    m_Table.RealName = cbbTable.Text;
                    m_Table.TableType = TableType.table;
                }
            }
            m_Generator = new Generator(m_Table, m_dbhelper, m_dbType);
        }
        void rdUpdate_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabUpdate;
            rtUpdate.Text = m_Generator.Update();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtUpdate.Text);
            }
        }

        void rdDelete_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabDelete;
            rtDelete.Text = m_Generator.Delete();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtDelete.Text);
            }
        }

        void rdInsert_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabInsert;
            rtInsert.Text = m_Generator.Insert();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtInsert.Text);
            }
        }

        void rdSSelect_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabSelect;
            rtSelect.Text = m_Generator.Select();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtSelect.Text);
            }
        }

        void rdIuow_Click(object sender, EventArgs e)
        {
            
        }

        void rduow_Click(object sender, EventArgs e)
        {
            
        }

        void rdInterface_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabInterface;
            rtInterface.Text = m_Generator.BuildInterface();
            if (ThisApp.currentSession.AutoCopy && !string.IsNullOrWhiteSpace(rtRepository.Text))
            {
                Clipboard.SetText(rtInterface.Text);
            }
        }

        void rdEntityclass_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabClass;
            rtClass.Text = m_Generator.CreateClass();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtClass.Text);
            }
        }
        void rdRepository_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            tab.SelectedTab = tabRepository;
            rtRepository.Text = m_Generator.BuildRepository();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtRepository.Text);
            }
        }
        void cbbTable_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        void cb_CheckedChanged(object sender, EventArgs e)
        {
            ApplicationSession session = new ApplicationSession();
            session.DbType = m_dbType;
            groupBox2.ToEntity(session);
            Utility.WriteSession(session);
            ThisApp.currentSession = session;
        }

        void btnSettingApplication_Click(object sender, EventArgs e)
        {
        }
        void btnCreate_Click(object sender, EventArgs e)
        {
            GetCurrentTable();
            if (ThisApp.currentSession.CreateClass)
            {
                rtClass.Text = m_Generator.CreateClass();
            }
            if (ThisApp.currentSession.CreateInterface)
            {
                rtInterface.Text = m_Generator.BuildInterface();
            }
            if (ThisApp.currentSession.CreateRepository)
            {
                rtRepository.Text = m_Generator.BuildRepository();
            }
            if (ThisApp.currentSession.CreateReactModel && m_dbType == DatabaseType.SQL)
            {
                rtIUOW.Text = m_Generator.CreateReactJsModel(m_dbhelper.GetSqlColumn(m_Table),string.Empty);
            }
            if (ThisApp.currentSession.CreateController)
            {
                m_Generator.CreateController();
            }
            if (ThisApp.currentSession.CreateUnitTest)
            {
                m_Generator.CreateUnitTestClass();
            }
            if (ThisApp.currentSession.CreateDependencyInjection)
            {
                m_Generator.CreateDependencyInjection();
            }
            if (ThisApp.currentSession.CreateBusiness)
            {
                m_Generator.CreateBusinessClass();
                m_Generator.CreateBusinessInterface();
            }
            if (ThisApp.currentSession.CreateMapper)
            {
                m_Generator.CreateMapper();
            }
            lblNotify.SetText("Thành công", LabelNotify.EnumStatus.Success);
            //rdRepository.PerformClick();
        }

        void cbbTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Return")
            {
                btnCreate.PerformClick();
            }
        }

        private void frmPrimary_Load(object sender, EventArgs e)
        {
            bool setup = Utility.BindDbInfoToProject();
            if(setup==false)
            {
                SetupConnection frmSetupDb = new SetupConnection();
                frmSetupDb.ShowDialog();
            }
            frmSelectProject frmSelectProject = new frmSelectProject();
            frmSelectProject.ShowDialog();
            m_dbhelper = new Model.DatabaseHelper(Utility.GetCurrentListDbInfo());
            m_dbhelper.GetAllTableType();
            InitCbbTable(Model.DatabaseType.SQL);

            m_dbType = DatabaseType.SQL;
            if (rdSql.Checked)
                m_dbType = DatabaseType.SQL;
            if (rdMongo.Checked)
                m_dbType = DatabaseType.Mongo;
            if (rdNeo.Checked)
                m_dbType = DatabaseType.Neo;
            GetCurrentTable();
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            this.ToForm(ThisApp.currentSession);
        }
        private void InitCbbTable(Model.DatabaseType databaseType)
        {
            m_dbType = databaseType;
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            this.ToForm(ThisApp.currentSession);
            ThisApp.DbType = m_dbType;
            cbbTable.DataSource = null;
            if(databaseType== Model.DatabaseType.SQL)
            {
                cbbTable.DataSource = Model.DatabaseHelper.lstSqlTable;
            }
            if (databaseType == Model.DatabaseType.Mongo)
            {
                cbbTable.DataSource = Model.DatabaseHelper.lstMongoCollection;
            }
            if (databaseType == Model.DatabaseType.Neo)
            {
                cbbTable.DataSource = Model.DatabaseHelper.lstNeoNode;
            }
            cbbTable.DisplayMember = "RealName";
        }
        public void LoadTable()
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            m_dbhelper.GetAllTableType();
            InitCbbTable(Model.DatabaseType.SQL);
        }

        private void btnTableName_Click(object sender, EventArgs e)
        {

        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            frmSelectProject frmSelectProject = new frmSelectProject();
            frmSelectProject.ShowDialog();
            m_dbhelper = new Model.DatabaseHelper(Utility.GetCurrentListDbInfo());
            m_dbhelper.GetAllTableType();
            InitCbbTable(Model.DatabaseType.SQL);
            this.ToForm(ThisApp.currentSession);

            m_dbType = DatabaseType.SQL;
            if (rdSql.Checked)
                m_dbType = DatabaseType.SQL;
            if (rdMongo.Checked)
                m_dbType = DatabaseType.Mongo;
            if (rdNeo.Checked)
                m_dbType = DatabaseType.Neo;
            GetCurrentTable();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {

        }

        private void metroRadioButton9_Click(object sender, EventArgs e)
        {
            InitCbbTable(Model.DatabaseType.Mongo);
        }

        private void metroRadioButton7_Click(object sender, EventArgs e)
        {
            InitCbbTable(Model.DatabaseType.Neo);
        }

        private void metroRadioButton8_Click(object sender, EventArgs e)
        {
            InitCbbTable(Model.DatabaseType.SQL);
        }

        private void btnSettingApplication_Click_1(object sender, EventArgs e)
        {
            frmSetupApp frmSetting = new frmSetupApp();
            frmSetting.ShowDialog();
            if (rdSql.Checked)
                m_dbType = DatabaseType.SQL;
            if (rdMongo.Checked)
                m_dbType = DatabaseType.Mongo;
            if (rdNeo.Checked)
                m_dbType = DatabaseType.Neo;
            ThisApp.currentSession = ThisApp.getSessionByDbType(m_dbType);
            groupBox2.ToForm(ThisApp.currentSession);
        }

        private void btnGenReactClass_Click(object sender, EventArgs e)
        {
            frmGenerateReactModel frmGenReact = new frmGenerateReactModel();
            frmGenReact.m_Generator = m_Generator;
            frmGenReact.ShowDialog();
        }

        private void btnCreateUpdateMongo_Click(object sender, EventArgs e)
        {
            frmGenerateMongoUpdate frm = new frmGenerateMongoUpdate();
            frm.ShowDialog();
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            frmGenerateNeoInsert frm = new frmGenerateNeoInsert();
            frm.ShowDialog();
        }
    }
}
