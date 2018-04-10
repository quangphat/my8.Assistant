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
    public partial class frmGenerateMongoUpdate : Form
    {
        public frmGenerateMongoUpdate()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCSharpClass.Text))
            {
                return;
            }
            ColumnFromCsharpClass columnFromCsharpClass = Generator.GetColumnFromCSharpClass(txtCSharpClass);
            StringBuilder stringBuilder = new StringBuilder();
            foreach(Column col in columnFromCsharpClass.Columns)
            {
                stringBuilder.Append($".Set(s => s.{col.Name}, {columnFromCsharpClass.ClassName.ToLower()}.{col.Name})");
                stringBuilder.Append(Environment.NewLine);
            }
            txtBuilder.Text = stringBuilder.ToString();
            if(ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(txtBuilder.Text);
            }
        }
    }
}
