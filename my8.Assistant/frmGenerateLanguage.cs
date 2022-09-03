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

            //GenerateLangFile();
        }

        private void txtEng_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
                return;

            //GenerateLangFile();
        }

        private void GenerateLangFile()
        {
            AppendILanguageFile();
            AppendApiMessageCodeFile();
            AppendLangFile($"{txtVN.Text.Trim()}", Utility.GetFullPathForConfigPath(ThisApp.AppSetting.MessageVNFilePath));
            AppendLangFile($"{txtEng.Text.Trim()}", Utility.GetFullPathForConfigPath(ThisApp.AppSetting.MessageENFilePath));
            txtEng.Text = string.Empty;
            txtVN.Text = string.Empty;
        }



        private void AppendILanguageFile()
        {
            if (string.IsNullOrWhiteSpace(ThisApp.AppSetting.ILanguageFilePath))
                return;

            if (string.IsNullOrWhiteSpace(txtKey.Text))
                return;
            var content = Utility.ReadFile(Utility.GetFullPathForConfigPath(ThisApp.AppSetting.ILanguageFilePath));
            //if (content.Contains($"{txtKey.Text}_") || content.Contains($"_{txtKey.Text}_") || content.Contains($"_{txtKey.Text}"))
            //    return;

            var strBuilder = new StringBuilder();
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append("\t");
            strBuilder.Append("//append_line_here");
            content = content.Replace("//append_line_here", $"{txtKey.Text}:string,{strBuilder.ToString()}");
            //content = content + strBuilder.ToString();
            Utility.WriteToFile(Utility.GetFullPathForConfigPath(ThisApp.AppSetting.ILanguageFilePath), content);
        }

        private void AppendApiMessageCodeFile()
        {
            if (string.IsNullOrWhiteSpace(ThisApp.AppSetting.MessageCodeApiFilePath))
                return;
            if (string.IsNullOrWhiteSpace(txtKey.Text))
                return;

            if (!cbWriteToApi.Checked)
                return;

            var content = Utility.ReadFile(Utility.GetFullPathForConfigPath(ThisApp.AppSetting.MessageCodeApiFilePath));
            //if (content.Contains($"{txtKey.Text}_") || content.Contains($"_{txtKey.Text}_") || content.Contains($"_{txtKey.Text}"))
            //    return;

            var strBuilder = new StringBuilder();
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append("\t");
            strBuilder.Append("\t//append_line_here");
            content = content.Replace("//append_line_here", $"public const string {txtKey.Text} = \"{txtVN.Text}\";{strBuilder.ToString()}");
            //content = content + strBuilder.ToString();
            Utility.WriteToFile(Utility.GetFullPathForConfigPath(ThisApp.AppSetting.MessageCodeApiFilePath), content);
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
            
            //if (content.Contains($"{txtKey.Text}_") || content.Contains($"_{txtKey.Text}_") || content.Contains($"_{txtKey.Text}"))
            //    return;

            var strBuilder = new StringBuilder();
            strBuilder.Append(Environment.NewLine);
            strBuilder.Append("\t");
            strBuilder.Append(keyToReplace);
            content = content.Replace(keyToReplace, $"{message},{strBuilder.ToString()}");
            //content = content + strBuilder.ToString();
            Utility.WriteToFile(filepath, content);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            GenerateLangFile();
        }
    }
}
