using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Model
{
    [Serializable]
    public class Project
    {
        public Project()
        {
            Id = 1;
            Name = "my8";
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsLastSelect {get;set;}
        public string Type { get; set; }
    }
}
