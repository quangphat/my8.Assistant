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
    public partial class frmGenerateLanguage : Form
    {
        public frmGenerateLanguage()
        {
            InitializeComponent();
        }

        private void txtVN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;

            generateLangFile();
        }

        private void txtEng_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;

            generateLangFile();
        }

        private void generateLangFile()
        {
            AppendILanguageFile();
            AppendLangFile($"{txtVN.Text.Trim()}", ThisApp.AppSetting.MessageVNFilePath);
            AppendLangFile($"{txtEng.Text.Trim()}", ThisApp.AppSetting.MessageENFilePath);
        }

        private void AppendILanguageFile()
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
                return;
            var content = Utility.ReadFile(ThisApp.AppSetting.ILanguageFilePath);
            if (content.Contains(txtKey.Text.Trim()))
                return;
            
            var strBuilder = new StringBuilder();
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append("\t");
            strBuilder.Append("//append_line_here");
            content = content.Replace("//append_line_here", $"{txtKey.Text}:string,{strBuilder.ToString()}");
            //content = content + strBuilder.ToString();
            Utility.WriteToFile(ThisApp.AppSetting.ILanguageFilePath, content);
        }

        private void AppendLangFile(string message, string filepath)
        {
            if (string.IsNullOrWhiteSpace(txtKey.Text))
                return;
            if (string.IsNullOrWhiteSpace(message))
                return;
            if (string.IsNullOrWhiteSpace(filepath))
                return;

            message = $"\"{txtKey.Text.Trim()}\":\"{message}\"";
            var keyToReplace = $"\"append_line_here\":\"\"";
            var content = Utility.ReadFile(filepath);
            
            if (content.Contains(txtKey.Text))
                return;

            var strBuilder = new StringBuilder();
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append("\t");
            strBuilder.Append(keyToReplace);
            content = content.Replace(keyToReplace, $"{message},{strBuilder.ToString()}");
            //content = content + strBuilder.ToString();
            Utility.WriteToFile(filepath, content);
        }
    }
}
