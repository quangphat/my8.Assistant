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
using my8.Assistant.Business;

namespace my8.Assistant
{
    public partial class frmPrimary : Form
    {
        public List<Model.Table> m_lstSqlTable;
        public List<Model.Table> m_lstMongoCollection;
        public  List<Model.Table> m_lstNeoNode;
        Model.DatabaseHelper _dbHelper;
        Generator m_Generator;
       
        Model.Table m_Table;

        DatabaseBusiness _bizDatabase;
        ProjectBusiness _bizProject;
        SessionBusiness _bizSession;
        public frmPrimary()
        {
            InitializeComponent();
            _bizDatabase = new DatabaseBusiness();
            _bizProject = new ProjectBusiness();
            _bizSession = new SessionBusiness();
            _dbHelper = new DatabaseHelper();
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

        private Model.Table GetCurrentTable()
        {
            m_Table = new Model.Table {

               RealName = txtConsole.Text
            };
            
            return m_Table;
        }
        void rdUpdate_Click(object sender, EventArgs e)
        {
            var table = GetCurrentTable();
            tab.SelectedTab = tabUpdate;
            rtUpdate.Text = m_Generator.GetSqlUpdateQuery(table);
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtUpdate.Text);
            }
        }

        void rdDelete_Click(object sender, EventArgs e)
        {
            var table  = GetCurrentTable();
            tab.SelectedTab = tabDelete;
            rtDelete.Text = m_Generator.GetSqlDeleteQuery(table);
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtDelete.Text);
            }
        }

        void rdInsert_Click(object sender, EventArgs e)
        {
            var table = GetCurrentTable();
            tab.SelectedTab = tabInsert;
            rtInsert.Text = m_Generator.GetSqlInsertQuery(table);
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtInsert.Text);
            }
        }

        void rdSSelect_Click(object sender, EventArgs e)
        {
            var table = GetCurrentTable();
            tab.SelectedTab = tabSelect;
            rtSelect.Text = m_Generator.GetSqlSelectQuery(table);
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
            var table = GetCurrentTable();
            tab.SelectedTab = tabInterface;
            rtInterface.Text = m_Generator.BuildInterface(table);
            if (ThisApp.currentSession.AutoCopy && !string.IsNullOrWhiteSpace(rtRepository.Text))
            {
                Clipboard.SetText(rtInterface.Text);
            }
        }

        void rdEntityclass_Click(object sender, EventArgs e)
        {
            var table = GetCurrentTable();
            tab.SelectedTab = tabClass;
            rtClass.Text = m_Generator.CreateClass(table);
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(rtClass.Text);
            }
        }
        void rdRepository_Click(object sender, EventArgs e)
        {
            var table = GetCurrentTable();
            tab.SelectedTab = tabRepository;
            rtRepository.Text = m_Generator.BuildRepository(table);
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
            //session.DbType = m_dbType;
            groupBox2.ToEntity(session);
            _bizSession.CreateSession(session);
            ThisApp.currentSession = session;
        }

        void btnSettingApplication_Click(object sender, EventArgs e)
        {
        }
        void btnCreate_Click(object sender, EventArgs e)
        {
            var table = GetCurrentTable();
            if (ThisApp.currentSession.CreateClass)
            {
                rtClass.Text = m_Generator.CreateClass(table);
            }
            if (ThisApp.currentSession.CreateInterface)
            {
                rtInterface.Text = m_Generator.BuildInterface(table);
            }
            if (ThisApp.currentSession.CreateRepository)
            {
                if(ThisApp.Project.Id==1)
                {
                    m_Generator.CreateReactJsRepositoryFile(table);
                }
                else
                    rtRepository.Text = m_Generator.BuildRepository(table);
            }
            //if (ThisApp.currentSession.CreateReactModel && m_dbType == DatabaseType.SQL)
            //{
            //    rtIUOW.Text = m_Generator.CreateReactJsModel(table,_dbHelper.GetSqlColumn(m_Table),string.Empty);
            //}
            if (ThisApp.currentSession.CreateController)
            {
                m_Generator.CreateController(table);
            }
            if (ThisApp.currentSession.CreateUnitTest)
            {
                m_Generator.CreateUnitTestClass(table);
            }
            if (ThisApp.currentSession.CreateDependencyInjection)
            {
                m_Generator.CreateDependencyInjection(table, ObjectType.biz_di);
                m_Generator.CreateDependencyInjection(table, ObjectType.rp_di);
            }
            if (ThisApp.currentSession.CreateBusiness)
            {
                m_Generator.CreateBusinessClass(table);
                m_Generator.CreateBusinessInterface(table);
            }
            if (ThisApp.currentSession.CreateMapper)
            {
                m_Generator.CreateMapper(table);
            }
            if(ThisApp.currentSession.CreateReactComponent)
            {
                m_Generator.CreateReactComponent(table);
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
            //bool setup = Utility.BindDbInfoToProject();
            //if(setup == false)
            //{
            //    SetupConnection frmSetupDb = new SetupConnection();
            //    frmSetupDb.ShowDialog();
            //}
            frmSelectProject frmSelectProject = new frmSelectProject();
            frmSelectProject.ShowDialog();
            this.ToForm(ThisApp.currentSession);
            this.Text = ThisApp.Project.Name;
            
            m_Generator = new Generator();
        }
        private void InitCbbTable()
        {
            ThisApp.currentSession  = _bizSession.GetSessionByDbType( ThisApp.Project.Id);
            this.ToForm(ThisApp.currentSession);
            
            //cbbTable.DataSource = null;
            //if(databaseType== Model.DatabaseType.SQL)
            //{
            //    cbbTable.DataSource = Model.DatabaseHelper.lstSqlTable;
            //}
            //if (databaseType == Model.DatabaseType.Mongo)
            //{
            //    cbbTable.DataSource = Model.DatabaseHelper.lstMongoCollection;
            //}
            //if (databaseType == Model.DatabaseType.Neo)
            //{
            //    cbbTable.DataSource = Model.DatabaseHelper.lstNeoNode;
            //}
            //cbbTable.DisplayMember = "RealName";
        }
        public void LoadTable()
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //_dbHelper.GetAllTableType();
            InitCbbTable();
        }

        private void btnTableName_Click(object sender, EventArgs e)
        {

        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            frmSelectProject frmSelectProject = new frmSelectProject();
            frmSelectProject.ShowDialog();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {

        }

        private void metroRadioButton9_Click(object sender, EventArgs e)
        {
            InitCbbTable();
        }

        private void metroRadioButton7_Click(object sender, EventArgs e)
        {
            InitCbbTable();
        }

        private void metroRadioButton8_Click(object sender, EventArgs e)
        {
            InitCbbTable();
        }

        private void btnSettingApplication_Click_1(object sender, EventArgs e)
        {
            if(ThisApp.Project.Type.ToLower() =="api")
            {
                frmSetupApp frmSetting = new frmSetupApp();
                frmSetting.ShowDialog();
            }
            else
            {
                frmSetupAppClient frmSetup = new frmSetupAppClient();
                frmSetup.ShowDialog();
            }
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
            //dwd
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            frmGenerateNeoInsert frm = new frmGenerateNeoInsert();
            frm.ShowDialog();
        }

        private void txtConsole_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;
            string content = txtConsole.Text;
            if (string.IsNullOrWhiteSpace(content))
                return;
            //string[] arr = StringExtension.GetConsole(content);
            var consoleObj = StringExtension.GetConsoleObjectMultiple(content);
            excuteConsoleMultiple(consoleObj);
        }
        private void excuteConsoleMultiple(List<ConsoleObject> objects)
        {
            if (objects == null)
                return;
            foreach(ConsoleObject obj in objects)
            {
                excuteConsoleOne(obj);
            }
        }
        private void excuteConsoleOne(ConsoleObject consoleObj)
        {
            if (!consoleObj.canExcuteCommand)
            {
                lblNotify.SetText(consoleObj.error, LabelNotify.EnumStatus.Failed);
                return;
            }

            var table = new Model.Table
            {
                RealName = consoleObj.ObjectName
            };
            if (consoleObj.Command == CommandType.create)
            {
                if (consoleObj.ObjectType == ObjectType.model)
                {
                    m_Generator.CreateClass(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.rp_interface)
                {
                    m_Generator.BuildInterface(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.rp_class)
                {
                    m_Generator.BuildRepository(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.react_model)
                {
                    m_Generator.CreateReactJsRepositoryFile(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.react_component)
                {
                    m_Generator.CreateReactComponent(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.controller)
                {
                    m_Generator.CreateController(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.rp_di)
                {
                    m_Generator.CreateDependencyInjection(table, ObjectType.rp_di);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.biz_di)
                {
                    m_Generator.CreateDependencyInjection(table, ObjectType.biz_di);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.biz_interface)
                {
                    m_Generator.CreateBusinessInterface(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.biz_class)
                {
                    m_Generator.CreateBusinessClass(table);
                    return;
                }
                if (consoleObj.ObjectType == ObjectType.mapper)
                {
                    m_Generator.CreateMapper(table);
                    return;
                }
            }
        }
    }
}
