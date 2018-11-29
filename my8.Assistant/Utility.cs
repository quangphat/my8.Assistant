using my8.Assistant.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace my8.Assistant
{
    public enum DbInfoType { MainApp, Jquery };
    public static class Utility
    {
        public static string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\my8";
        private static string connectionfile = "connectioninfo.txt";
        private static string AppSettingFile = "AppSetting.txt";
        private static string AppSessionFile = "AppSession.txt";
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

        #region DbInfo
        public static bool RemoveDbInfo(DatabaseInfo dbInfo)
        {
            List<DatabaseInfo> lstDbInfo = GetListDbInfo();
            if (lstDbInfo == null) return false;
            lstDbInfo.Remove(dbInfo);
            WriteListDbInfo(lstDbInfo);
            return true;
        }
        private static bool WriteListDbInfo(List<DatabaseInfo> dbInfos)
        {
            if (dbInfos == null) return false;
            string info = ConvertListDbInfoToJson(dbInfos);
            WriteToFileInAppData(connectionfile, info);
            return true;
        }
        public static bool WriteDbInfo(DatabaseInfo dbInfo)
        {
            if (dbInfo == null) return false;
            dbInfo.ProjectId = ThisApp.Project.Id;
            string info = ConvertDbInfoToJson(dbInfo);
            List<DatabaseInfo> lstDbInfo = GetListDbInfo();
            if (lstDbInfo == null)
                lstDbInfo = new List<DatabaseInfo>();
            DatabaseInfo exists = lstDbInfo.Where(p => p.Id == dbInfo.Id).FirstOrDefault();
            if(exists==null)
            {
                dbInfo.Id = lstDbInfo.Count() + 1;
                lstDbInfo.Add(dbInfo);
            }
            else
            {
                exists = dbInfo.DeepClone();
            }
            WriteListDbInfo(lstDbInfo);
            return true;
        }
        public static string ConvertListDbInfoToJson(List<DatabaseInfo> dbInfo)
        {
            return JsonConvert.SerializeObject(dbInfo, Formatting.None);
        }
        public static string ConvertDbInfoToJson(DatabaseInfo dbInfo)
        {
            return JsonConvert.SerializeObject(dbInfo, Formatting.None);
        }
        private static List<DatabaseInfo> GetListDbInfo()
        {
            string info = "";

            info = ReadAppDataFile(connectionfile);
            if (string.IsNullOrEmpty(info))
            {
                return null;
            }
            try
            {
                List<DatabaseInfo> lstDbinfo = JsonConvert.DeserializeObject<List<DatabaseInfo>>(info);
                return lstDbinfo;
            }
            catch
            {
                return null;
            }
        }
        public static List<DatabaseInfo> GetCurrentListDbInfo()
        {
            List<DatabaseInfo> lstCurrent = GetListDbInfo().Where(p => p.ProjectId == ThisApp.Project.Id).ToList();
            return lstCurrent;
        }

        public static bool BindDbInfoToProject()
        {
            List<DatabaseInfo> lstDbinfo = GetCurrentListDbInfo();
            DatabaseInfo sql = lstDbinfo.FirstOrDefault(p => p.DbType == DatabaseType.SQL);
            ThisApp.SqlDbInfo = sql;
            DatabaseInfo mongo = lstDbinfo.FirstOrDefault(p => p.DbType == DatabaseType.Mongo);
            ThisApp.MongoDbInfo = mongo;
            DatabaseInfo neo = lstDbinfo.FirstOrDefault(p => p.DbType == DatabaseType.Neo);
            ThisApp.NeoDbInfo = neo;
            if (neo == null || sql == null || mongo == null)
                return false;
            return true;
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
        private static string ConvertAppSettingToJson(List<ApplicationSetting> appSettings)
        {
            return JsonConvert.SerializeObject(appSettings, Formatting.None);
        }
        public static void WriteAppSetting(ApplicationSetting appSetting)
        {
            appSetting.ProjectId = ThisApp.Project.Id;
            List<ApplicationSetting> lstSetting = GetListSetting();
            if (lstSetting == null) lstSetting = new List<ApplicationSetting>();
            ApplicationSetting exist = lstSetting.FirstOrDefault(p => p.ProjectId == ThisApp.Project.Id);
            if (exist != null)
            {
                lstSetting.Remove(exist);
                exist = appSetting.DeepClone();
                lstSetting.Add(exist);
            }
            else
            {
                lstSetting.Add(appSetting);
            }
            WriteToFileInAppData(AppSettingFile, ConvertAppSettingToJson(lstSetting));
        }
        private static List<ApplicationSetting> GetListSetting()
        {
            string info = ReadAppDataFile(AppSettingFile);
            if (string.IsNullOrEmpty(info))
            {
                return null;
            }
            try
            {
                List<ApplicationSetting> lstSetting = JsonConvert.DeserializeObject<List<ApplicationSetting>>(info);
                return lstSetting;
            }
            catch
            {
                return null;
            }
        }
        public static ApplicationSetting GetCurrentSetting(string projectName)
        {
            List<ApplicationSetting> lstExists = GetListSetting();
            if (lstExists == null) return null;
            ApplicationSetting current = lstExists.FirstOrDefault(p => p.ProjectId == ThisApp.Project.Id);
            return current;
        }
        #endregion

        #region Session
        private static string ConvertSessionToJson(List<ApplicationSession> sessions)
        {
            return JsonConvert.SerializeObject(sessions, Formatting.None);
        }
        public static void WriteSession(ApplicationSession session)
        {
            session.ProjectId = ThisApp.Project.Id;
            List<ApplicationSession> lstSession = GetListSession();
            if (lstSession == null) lstSession = new List<ApplicationSession>();
            ApplicationSession exist = lstSession.FirstOrDefault(p => p.ProjectId == session.ProjectId && p.DbType == session.DbType);
            if (exist != null)
            {
                lstSession.Remove(exist);
                exist = session.DeepClone();
                lstSession.Add(exist);
            }
            else
            {
                lstSession.Add(session);
            }
            WriteToFileInAppData(AppSessionFile, ConvertSessionToJson(lstSession));
        }
        public static List<ApplicationSession> GetProjectSessions(string projectName)
        {
            string info = ReadAppDataFile(AppSessionFile);
            if (string.IsNullOrEmpty(info))
            {
                return null;
            }
            try
            {
                List<ApplicationSession> lstAppSession = JsonConvert.DeserializeObject<List<ApplicationSession>>(info);
                List<ApplicationSession> sessions = lstAppSession.Where(p => p.ProjectId == ThisApp.Project.Id).ToList();
                return sessions;
            }
            catch
            {
                return null;
            }
        }
        private static List<ApplicationSession> GetListSession()
        {
            string info = ReadAppDataFile(AppSessionFile);
            if (string.IsNullOrEmpty(info))
            {
                return null;
            }
            try
            {
                List<ApplicationSession> lstSession = JsonConvert.DeserializeObject<List<ApplicationSession>>(info);
                return lstSession;
            }
            catch
            {
                return null;
            }
        }
        public static Project GetLastSelectedProject()
        {
            List<Project> lstProject = GetAllProject();
            if (lstProject == null) return null;
            return lstProject.Where(p => p.IsLastSelect == true).FirstOrDefault();
        }
        private static string ConvertProjectToJson(List<Project> projects)
        {
            return JsonConvert.SerializeObject(projects, Formatting.None);
        }
        public static void UpdateLastSelectProject(Project project)
        {
            List<Project> lstProject = GetAllProject();
            if (lstProject == null) lstProject = new List<Project>();
            Project exist = lstProject.FirstOrDefault(p => p.Id == project.Id);
            lstProject.ForEach(p =>
            {
                p.IsLastSelect = false;
                if (p.Id == project.Id)
                    p.IsLastSelect = true;
            });
            WriteToFileInAppData(ProjectsFile, ConvertProjectToJson(lstProject));
        }
        public static void WriteProjects(Project project)
        {
            List<Project> lstProject = GetAllProject();
            if (lstProject == null) lstProject = new List<Project>();
            Project exist = lstProject.FirstOrDefault(p => p.Id== project.Id || p.Name.Trim() == project.Name.Trim());
            if (exist != null)
            {
                lstProject.Remove(exist);
                exist = project.DeepClone();
                lstProject.Add(exist);
            }
            else
            {
                int maxId = lstProject.Count !=0 ? lstProject.Select(p => p.Id).Max(): 1;
                project.Id = maxId + 1;
                lstProject.Add(project);
            }
            WriteToFileInAppData(ProjectsFile, ConvertProjectToJson(lstProject));
        }
        private static string ReadProjectsFile()
        {
            return ReadAppDataFile(ProjectsFile);
        }
        #endregion
        public static List<Project> GetAllProject()
        {
            string projects = ReadProjectsFile();
            if (string.IsNullOrEmpty(projects))
            {
                return null;
            }
            try
            {
                List<Project> lstProjects = JsonConvert.DeserializeObject<List<Project>>(projects);
                return lstProjects;
            }
            catch
            {
                return null;
            }
        }
        public static Project GetProjectById(int id)
        {
            Project project = GetAllProject().Where(p => p.Id == id).FirstOrDefault();
            return project;
        }
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
