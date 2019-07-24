using my8.Assistant.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Business
{
    public class DatabaseBusiness
    {
        public List<DatabaseInfo> GetAll()
        {
            string info = "";

            info = Utility.ReadAppDataFile(Utility.connectionfile);
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
        public List<DatabaseInfo> GetByProjectId(int projectId)
        {
            if (projectId <= 0)
                return null;
            return GetAll().Where(p => p.ProjectId == projectId).ToList();
        }
        public bool RemoveDbInfo(DatabaseInfo dbInfo)
        {
            List<DatabaseInfo> lstDbInfo = GetAll();
            if (lstDbInfo == null) return false;
            lstDbInfo.Remove(dbInfo);
            WriteListDbInfo(lstDbInfo);
            return true;
        }
        private bool WriteListDbInfo(List<DatabaseInfo> dbInfos)
        {
            if (dbInfos == null)
                return false;
            string info = Utility.ConvertListObjectToJson(dbInfos);
            Utility.WriteToFileInAppData(Utility.connectionfile, info);
            return true;
        }
        public bool CreateDbInfo(DatabaseInfo dbInfo)
        {
            if (dbInfo == null) return false;
            dbInfo.ProjectId = ThisApp.Project.Id;
            string info = Utility.ConvertObjectToJson(dbInfo);
            List<DatabaseInfo> lstDbInfo = GetAll();
            if (lstDbInfo == null)
                lstDbInfo = new List<DatabaseInfo>();
            DatabaseInfo exists = lstDbInfo.Where(p => p.Id == dbInfo.Id).FirstOrDefault();
            if (exists == null)
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
        //public bool BindDbInfoToProject()
        //{
        //    List<DatabaseInfo> lstDbinfo = GetByProjectId(ThisApp.Project.Id);
        //    DatabaseInfo sql = lstDbinfo.FirstOrDefault(p => p.DbType == DatabaseType.SQL);
        //    ThisApp.SqlDbInfo = sql;
        //    DatabaseInfo mongo = lstDbinfo.FirstOrDefault(p => p.DbType == DatabaseType.Mongo);
        //    ThisApp.MongoDbInfo = mongo;
        //    DatabaseInfo neo = lstDbinfo.FirstOrDefault(p => p.DbType == DatabaseType.Neo);
        //    ThisApp.NeoDbInfo = neo;
        //    if (neo == null || sql == null || mongo == null)
        //        return false;
        //    return true;
        //}
    }
}
