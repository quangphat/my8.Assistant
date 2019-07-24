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
        private static List<ApplicationSession> GetAll()
        {
            string info = Utility.ReadAppDataFile(Utility.AppSessionFile);
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
        public List<ApplicationSession> GetProjectSessions(int projectId)
        {
            string info = Utility.ReadAppDataFile(Utility.AppSessionFile);
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
        public void CreateSession(ApplicationSession session)
        {
            session.ProjectId = ThisApp.Project.Id;
            List<ApplicationSession> lstSession = GetAll();
            if (lstSession == null) lstSession = new List<ApplicationSession>();
            ApplicationSession exist = lstSession.FirstOrDefault(p => p.ProjectId == session.ProjectId);
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
            Utility.WriteToFileInAppData(Utility.AppSessionFile, Utility.ConvertListObjectToJson(lstSession));
        }
        public ApplicationSession GetSessionByDbType(int projectId)
        {
            ApplicationSession session = GetProjectSessions(projectId).FirstOrDefault();
            return session;
        }
    }
}
