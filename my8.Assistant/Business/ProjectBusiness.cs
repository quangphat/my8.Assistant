using my8.Assistant.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Business
{
    public class ProjectBusiness
    {
        public List<Project> GetAll()
        {
            string projects = string.Empty;

            projects = Utility.ReadProjectsFile();
            
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
        public Project GetProjectById(int projectId)
        {
            Project project = GetAll().Where(p => p.Id == projectId).FirstOrDefault();
            return project;
        }
        public void CreateProject(Project project)
        {
            List<Project> lstProject = GetAll();
            if (lstProject == null)
                lstProject = new List<Project>();
            Project exist = lstProject.FirstOrDefault(p => p.Id == project.Id || p.Name.Trim() == project.Name.Trim());
            if (exist != null)
            {
                lstProject.Remove(exist);
                exist = project.DeepClone();
                lstProject.Add(exist);
            }
            else
            {
                int maxId = lstProject.Count != 0 ? lstProject.Select(p => p.Id).Max() : 1;
                project.Id = maxId + 1;
                lstProject.Add(project);
            }
            Utility.WriteToFileInAppData(Utility.ProjectsFile, Utility.ConvertListObjectToJson(lstProject));
        }
        public void UpdateLastSelectProject(Project project)
        {
            List<Project> lstProject = GetAll();
            if (lstProject == null)
                lstProject = new List<Project>();
            Project exist = lstProject.FirstOrDefault(p => p.Id == project.Id);
            lstProject.ForEach(p =>
            {
                p.IsLastSelect = false;
                if (p.Id == project.Id)
                    p.IsLastSelect = true;
            });
            Utility.WriteToFileInAppData(Utility.ProjectsFile, Utility.ConvertListObjectToJson(lstProject));
        }
    }
}
