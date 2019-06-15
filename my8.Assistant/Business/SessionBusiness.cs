using my8.Assistant.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Business
{
    public class SessionBusiness
    {
        private static async Task<List<ApplicationSession>> GetAll()
        {
            string info = await Utility.ReadAppDataFile(Utility.AppSessionFile);
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
        public async Task<List<ApplicationSession>> GetProjectSessions(int projectId)
        {
            string info = await Utility.ReadAppDataFile(Utility.AppSessionFile);
            if (string.IsNullOrEmpty(info))
            {
                return null;
            }
            try
            {
                List<ApplicationSession> lstAppSession = JsonConvert.DeserializeObject<List<ApplicationSession>>(info);
                List<ApplicationSession> sessions = lstAppSession.Where(p => p.ProjectId == projectId).ToList();
                return sessions;
            }
            catch
            {
                return null;
            }
        }
        public async Task CreateSession(ApplicationSession session)
        {
            session.ProjectId = ThisApp.Project.Id;
            List<ApplicationSession> lstSession = await GetAll();
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
            await Utility.WriteToFileInAppData(Utility.AppSessionFile, Utility.ConvertListObjectToJson(lstSession));
        }
        public async Task<ApplicationSession> GetSessionByDbType(DatabaseType dbType, int projectId)
        {
            ApplicationSession session = (await GetProjectSessions(projectId)).FirstOrDefault(p => p.ProjectId ==projectId);
            return session;
        }
    }
}
