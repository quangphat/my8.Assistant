using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Model
{
    public enum EFType{List=1,IEnumerable=2,IQueryable=3};
    public enum FileType
    {
        Model,
        Repository,
        Interface,
        DapperMapping,
        EFMapping
    }
    [Serializable]
    public class ApplicationSetting
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ModelFolder {get;set;}
        public string RepositoryFolder { get; set; }
        public string InterfaceFolder { get; set; }
        public bool UseInterface { get; set; }
        public bool UseEF { get; set; }
        
        public bool AutoCreateFile { get; set; }
        public EFType EfReturnType { get; set; }
        public string PrimaryColumnRule { get; set; }
        public bool GenSqlScript = true;
        public string ControllerTemplate { get; set; }
        public string ControllerFolder { get; set; }
        public string MongoFolder { get; set; }
        public string NeoFolder { get; set; }
        public string SqlFolder { get; set; }
        public string UnitTestFolder { get; set; }
        public string UnitTestFileTemplate { get; set; }
        public string ReactJsModelFile { get; set; }
        public string ReactModelFilename { get; set; }
        public string StartUpFile { get; set; }
        public string SqlRepositoryTemplate { get; set; }
        public string MongoRepositoryTemplate { get; set; }
        public string NeoRepositoryTemplate { get; set; }
        public string SqlInterfaceTemplate { get; set; }
        public string MongoInterfaceTemplate { get; set; }
        public string NeoInterfaceTemplate { get; set; }
        public string SqlModelTemplateFile { get; set; }
        public string MongoModelTemplateFile { get; set; }
        public string NeoModelTemplateFile { get; set; }
        public string BusinessFolder { get; set; }
        public string BusinessTemplate { get; set; }
        public string IBusinessFolder { get; set; }
        public string IBusinessTemplate { get; set; }
        public string SubFolderName { get; set; } //Tên subfolder của sql, mongo,neo
        public string MapperFile { get; set; }
        public string[] getSubFolferName()
        {
                return SubFolderName.Split(';');
        }
    }
}
