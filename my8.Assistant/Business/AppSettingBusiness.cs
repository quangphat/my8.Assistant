using my8.Assistant.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Business
{
    public class AppSettingBusiness
    {
        public List<ApplicationSetting> GetAll()
        {
            string info = Utility.ReadAppDataFile(Utility.AppSettingFile);
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
        public ApplicationSetting GetCurrentSetting(int projectId)
        {
            List<ApplicationSetting> lstExists = GetAll();
            if (lstExists == null) return null;
            ApplicationSetting current = lstExists.FirstOrDefault(p => p.ProjectId == projectId);
            return current;
        }
        public void WriteAppSetting(ApplicationSetting appSetting)
        {
            appSetting.ProjectId = ThisApp.Project.Id;
            List<ApplicationSetting> lstSetting = GetAll();
            if (lstSetting == null) lstSetting = new List<ApplicationSetting>();
            ApplicationSetting exist = lstSetting.FirstOrDefault(p => p.ProjectId == appSetting.ProjectId);
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
            Utility.WriteToFileInAppData(Utility.AppSettingFile, Utility.ConvertListObjectToJson(lstSetting));
        }
    }
}
