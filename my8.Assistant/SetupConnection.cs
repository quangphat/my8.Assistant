using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoControl;
using my8.Assistant.Business;
using my8.Assistant.Model;

namespace my8.Assistant
{
    public partial class SetupConnection : Form
    {
        List<DatabaseInfo> m_lstDbInfo = null;
        private DatabaseBusiness _bizDatabase;
        public SetupConnection()
        {
            InitializeComponent();
            m_lstDbInfo = new List<DatabaseInfo>();
            _bizDatabase = new DatabaseBusiness();
            txtServer.Select();

        }

        private async void SetupConnection_Load(object sender, EventArgs e)
        {
            m_lstDbInfo = await _bizDatabase.GetByProjectId(ThisApp.AppSetting.ProjectId);
            DatabaseInfo sql = m_lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.SQL);
            this.ToForm(sql);
        }

        private async void btnWrite_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbInfo = new DatabaseInfo();
            this.ToEntity(dbInfo);
            await _bizDatabase.CreateDbInfo(dbInfo);
            m_lstDbInfo = await _bizDatabase.GetByProjectId(ThisApp.AppSetting.ProjectId);
        }


        private async void btnConnect_Click(object sender, EventArgs e)
        {
            await _bizDatabase.BindDbInfoToProject();
            this.Close();
        }

        private void SetupConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ThisApp.SqlCon == null && ThisApp.MongoCon==null)
            {
                Application.Exit();
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbinfo = new DatabaseInfo();
            this.ToEntity(dbinfo);
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa mục này?","", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No) return;
            await _bizDatabase.RemoveDbInfo(dbinfo);
            await _bizDatabase.BindDbInfoToProject();
        }

        private void autoMetroRadio1_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbinfo = m_lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.SQL);
            if (dbinfo == null)
            {
                dbinfo = new DatabaseInfo();
                dbinfo.DbType = DatabaseType.SQL;
            }
               
            this.ToForm(dbinfo);
            
        }

        private void autoMetroRadio5_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbinfo = m_lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.Mongo);
            if (dbinfo == null)
            {
                dbinfo = new DatabaseInfo();
                dbinfo.DbType = DatabaseType.Mongo;
            }
            this.ToForm(dbinfo);
        }

        private void autoMetroRadio4_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbinfo = m_lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.Neo);
            if (dbinfo == null)
            {
                dbinfo = new DatabaseInfo();
                dbinfo.DbType = DatabaseType.Neo;
            }
            this.ToForm(dbinfo);
        }
    }
}
