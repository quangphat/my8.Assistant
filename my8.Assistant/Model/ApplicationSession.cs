using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Model
{
    [Serializable]
    public class ApplicationSession
    {
        public ApplicationSession()
        {
            
        }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DatabaseType DbType { get; set; }
        public bool CreateInterface { get; set; }
        public bool CreateRepository { get; set; }
        public bool CreateReactModel { get; set; }
        public bool CreateClass { get; set; }
        public bool OverwriteClass { get; set; }
        public bool OverwriteRepository { get; set; }
        public bool OverwriteInterfaceRepository { get; set; }
        public bool AutoCopy { get; set; }
        public bool CreateController { get; set; }
        public bool OverWriteController { get; set; }
        public bool CreateView { get; set; }
        public bool OverWriteReactModel { get; set; }
        public bool CreateUnitTest { get; set; }
        public bool OverWriteUnitTest { get; set; }
        public bool CreateDependencyInjection { get; set; }
        public bool OverWriteDependencyInjection { get; set; }
        public bool CreateBusiness { get; set; }
        public bool OverWriteBusiness { get; set; }
        public bool CreateMapper { get; set; }
        public bool CreateReactComponent { get; set; }
    }
    public enum ModelSyntaxType
    {
        Original = 1,
        CamelCase=2
    }
}
