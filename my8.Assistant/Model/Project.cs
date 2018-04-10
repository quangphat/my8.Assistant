using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Model
{
    public class Project
    {
        public Project()
        {
            Id = 1;
            ProjectName = "my8";
        }
        public int Id { get; set; }
        public string ProjectName;
    }
}
