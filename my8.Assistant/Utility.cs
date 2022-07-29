using my8.Assistant.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my8.Assistant
{
    public enum DbInfoType { MainApp, Jquery };
    public static class Utility
    {
        public static string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\my8";
        public static string connectionfile = "connectioninfo.txt";
        public static string AppSettingFile = "AppSetting.txt";
        public static string AppSessionFile = "AppSession.txt";
        public static string LastProjectFile = "LastProject.txt";
        public static string ProjectsFile = "Projects.txt";
        #region WriteCsFile
        private static void WriteFile(string fileName, string content)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                file.WriteLine(content);
            }
        }
        public static void WriteToFile(string content, string filepath, FileType Type)
        {
            if (Type == FileType.Model)
            {
                WriteFile(filepath, content);
            }
            if (Type == FileType.Interface)
            {
                WriteFile(filepath, content);
            }
            if (Type == FileType.Repository)
            {
                WriteFile(filepath, content);
            }
        }
        #endregion

        public static string GetFullPathForConfigPath(string absolutePath, TypeOfPath type = TypeOfPath.Project)
        {
            if (string.IsNullOrWhiteSpace(absolutePath)) return string.Empty;

            if(!absolutePath.StartsWith("\\"))
                absolutePath = "\\" + absolutePath;
            return type == TypeOfPath.Project ? $"{ThisApp.AppSetting.ProjectFolder}{absolutePath}": $"{ThisApp.AppSetting.TemplateFolder}{absolutePath}";
        }
        public static string GetFolderPath()
        {
            string folderPath = string.Empty;
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderDialog.SelectedPath;
                }
            }
            return folderPath;
        }
        public static string GetFilePath()
        {
            string filePath = string.Empty;
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = fileDialog.FileName;
                }
            }
            return filePath;
        }

        public static string GetFolderPathFromUrl(string fileURL)
        {
            if (string.IsNullOrWhiteSpace(fileURL))
                return null;

            return Path.GetDirectoryName(fileURL);
        }

        #region DbInfo
       
       
        

        public static string ConvertObjectToJson<T>(T dbInfo)
        {
            return JsonConvert.SerializeObject(dbInfo, Formatting.None);
        }


        
        #endregion

        #region File
        
        public static string ReadAppDataFile(string fileName)
        {
            string path = appDataDir + "\\" + fileName;
            if (!File.Exists(path))
                return string.Empty;
            string s = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                s = sr.ReadToEnd();
            }
            return s;
        }
        public static bool WriteToFileInAppData(string fileName, string value)
        {
            string path = appDataDir + "\\" + fileName;
            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(value);
            }
            return true;
        }
        public static bool WriteToFile(string filePath, string value)
        {
            string path = filePath;
            if (!Directory.Exists(appDataDir))
            {
                Directory.CreateDirectory(appDataDir);
            }
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(value);
            }
            return true;
        }
        public static string ReadFile(string filePath)
        {
            string path = filePath;
            if (!File.Exists(path))
                return string.Empty;
            string s = string.Empty;
            using (StreamReader sr = new StreamReader(path))
            {
                s = sr.ReadToEnd();
            }
            return s;
        }
        #endregion

        #region Setting

        
        
        
        #endregion

        #region Session

        public static string ConvertListObjectToJson<T>(List<T> projects)
        {
            return JsonConvert.SerializeObject(projects, Formatting.None);
        }

        
        public static string ReadProjectsFile()
        {
            return ReadAppDataFile(ProjectsFile);
        }
        #endregion


        public static string ToPascalCase(this string the_string)
        {
            // If there are 0 or 1 characters, just return the string.
            if (the_string == null) return the_string;
            if (the_string.Length < 2) return the_string.ToUpper();

            // Split the string into words.
            string[] words = the_string.Split('_');

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }
    }
}
