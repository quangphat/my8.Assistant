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
    public partial class frmGenerateNeoInsert : Form
    {
        public frmGenerateNeoInsert()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtCSharpClass.Text))
            {
                return;
            }
            char firstChar = txtClassName.Text.ToLower()[0];
            ColumnFromCsharpClass columnFromCsharpClass = Generator.GetColumnFromCSharpClass(txtCSharpClass);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"({firstChar}:{txtClassName.Text} ");
            stringBuilder.Append("{");
            foreach (Column col in columnFromCsharpClass.Columns)
            {
                stringBuilder.Append(col.Name +":{" + col.Name + "},");
            }
            stringBuilder.Append("})\")");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(".WithParams(new { ");
            foreach (Column col in columnFromCsharpClass.Columns)
            {
                stringBuilder.Append($"{col.Name} = {txtClassName.Text.ToLower()}.{col.Name},");
            }
            stringBuilder.Append("})");
            txtBuilder.Text = stringBuilder.ToString();
            if (ThisApp.currentSession.AutoCopy)
            {
                Clipboard.SetText(txtBuilder.Text);
            }
        }
    }
}
