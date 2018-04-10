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
using my8.Assistant.Model;

namespace my8.Assistant
{
    public partial class SetupConnection : Form
    {
        List<DatabaseInfo> m_lstDbInfo = null;
        public SetupConnection()
        {
            InitializeComponent();
            m_lstDbInfo = new List<DatabaseInfo>();
            txtServer.Select();
        }

        private void SetupConnection_Load(object sender, EventArgs e)
        {
            m_lstDbInfo = Utility.GetCurrentListDbInfo();
            DatabaseInfo sql = m_lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.SQL);
            this.ToForm(sql);
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbInfo = new DatabaseInfo();
            this.ToEntity(dbInfo);
            Utility.WriteDbInfo(dbInfo);
            m_lstDbInfo = Utility.GetCurrentListDbInfo();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            Utility.BindDbInfoToProject();
            this.Close();
        }

        private void SetupConnection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ThisApp.SqlCon == null && ThisApp.MongoCon==null)
            {
                Application.Exit();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbinfo = new DatabaseInfo();
            this.ToEntity(dbinfo);
            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa mục này?","", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.No) return;
            Utility.RemoveDbInfo(dbinfo);
            Utility.BindDbInfoToProject();
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
