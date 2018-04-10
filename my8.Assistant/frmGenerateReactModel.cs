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
    public partial class frmGenerateReactModel : Form
    {

        public Generator m_Generator;
        private string className;
        public frmGenerateReactModel()
        {
            InitializeComponent();
            className = string.Empty;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (m_Generator != null)
            {
                List<Column> lstColumn = GetColumns();
                string generated = m_Generator.CreateReactJsModel(lstColumn, className);
                txtReactModel.Text = generated;
            }
        }
        private List<Column> GetColumns()
        {
            ColumnFromCsharpClass columnFromCsharpClass = new ColumnFromCsharpClass();
            columnFromCsharpClass = Generator.GetColumnFromCSharpClass(txtCSharpClass);
            className = columnFromCsharpClass.ClassName;
            return columnFromCsharpClass.Columns;
        }
        public readonly string[] ArrayDataType = { "List", "IEnumerable", "IList", "IQueryable" };

        private void txtCSharpClass_Click(object sender, EventArgs e)
        {
            string data = Clipboard.GetText();
            txtCSharpClass.Text = data;
        }
    }
}
