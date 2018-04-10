using my8.Assistant.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace my8.Assistant
{
    public class Generator
    {
        protected StringBuilder m_strBuilder;
        public StringBuilder m_clause1, m_clause2;
        public StringBuilder m_strResult;
        protected List<Column> m_lstColumn;
        protected string m_templateFilePath;
        protected string m_templateContent;
        protected Table m_table;
        protected DatabaseHelper m_DbHelper;
        protected DatabaseType m_DbType;
        public Generator(Table table, DatabaseHelper dbHelper, DatabaseType dbType)
        {
            m_strBuilder = null;
            m_lstColumn = null;
            m_clause1 = null;
            m_clause2 = null;
            m_strResult = null;
            m_templateContent = string.Empty;
            m_templateFilePath = string.Empty;
            m_templateContent = string.Empty;
            m_table = table;
            m_DbHelper = dbHelper;
            m_DbType = dbType;
            if (m_table != null)
            {
                m_lstColumn = m_DbHelper.GetSqlColumn(table);
            }
        }
        public string Insert()
        {
            bool first = true;
            m_clause1 = new StringBuilder();
            m_clause2 = new StringBuilder();
            foreach (Column column in m_lstColumn)
            {
                if (first == true)
                {
                    if ((column.IsPrimary == true && column.Datatype != "int") || column.IsPrimary == false)
                    {
                        m_clause1.Append(column.Name);
                        m_clause2.Append("@");
                        m_clause2.Append(column.Name);
                        first = false;
                    }
                }
                else
                {
                    if ((column.IsPrimary == true && column.Datatype != "int") || column.IsPrimary == false)
                    {
                        m_clause1.Append(",");
                        m_clause1.Append(column.Name);
                        m_clause2.Append(",@");
                        m_clause2.Append(column.Name);
                    }
                }
            }
            m_strResult = new StringBuilder();
            m_strResult.Append("insert into ");
            m_strResult.Append(m_table.CustomName);
            m_strResult.Append(" (");
            m_strResult.Append(m_clause1);
            m_strResult.Append(") ");
            m_strResult.Append("values (");
            m_strResult.Append(m_clause2);
            m_strResult.Append(")");
            return m_strResult.ToString();
        }
        public string Update()
        {
            bool first = true;
            m_clause1 = new StringBuilder();
            foreach (Column column in m_lstColumn)
            {
                if (first == true)
                {
                    if ((column.IsPrimary == true && column.Datatype != "int") || column.IsPrimary == false)
                    {
                        m_clause1.Append(column.Name);
                        m_clause1.Append("= @");
                        m_clause1.Append(column.Name);
                        first = false;
                    }
                }
                else
                {
                    if ((column.IsPrimary == true && column.Datatype != "int") || column.IsPrimary == false)
                    {
                        m_clause1.Append(",");
                        m_clause1.Append(column.Name);
                        m_clause1.Append("= @");
                        m_clause1.Append(column.Name);
                    }
                }
            }
            m_strResult = new StringBuilder();
            m_strResult.Append("update ");
            m_strResult.Append(m_table.CustomName);
            m_strResult.Append(" set ");
            m_strResult.Append(m_clause1);
            m_strResult.Append(" where ");
            m_strResult.Append(m_table.PrimaryKeyCol);
            m_strResult.Append(" = ");
            m_strResult.Append("@");
            m_strResult.Append(m_table.PrimaryKeyCol);
            return m_strResult.ToString();
        }

        public string Select()
        {
            m_strResult = new StringBuilder();
            m_strResult.Append("select * from ");
            m_strResult.Append(m_table.CustomName);
            return m_strResult.ToString();
        }

        public string Delete()
        {
            m_strResult = new StringBuilder();
            m_strResult.Append("delete from ");
            m_strResult.Append(m_table.CustomName);
            m_strResult.Append(" where ");
            m_strResult.Append(m_table.PrimaryKeyCol);
            m_strResult.Append(" = @");
            m_strResult.Append(m_table.PrimaryKeyCol);
            return m_strResult.ToString();
        }
        #region BuildRepository
        public string BuildRepository()
        {
            if (!ThisApp.currentSession.CreateRepository) return null;
            string filepath = string.Empty;
            m_templateFilePath = string.Empty;
            if (m_DbType== DatabaseType.SQL)
            {
                filepath = ThisApp.AppSetting.RepositoryFolder +"\\" + ThisApp.AppSetting.getSubFolferName()[0] + "\\" + m_table.CustomName + "Repository.cs";
                m_templateFilePath = ThisApp.AppSetting.SqlRepositoryTemplate;
            }
            else if (m_DbType == DatabaseType.Mongo)
            {

                    filepath = ThisApp.AppSetting.RepositoryFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[1] + "\\" + m_table.CustomName + "Repository.cs";
                    m_templateFilePath = ThisApp.AppSetting.MongoRepositoryTemplate;
            }
            else if (m_DbType == DatabaseType.Neo)
            {

                filepath = ThisApp.AppSetting.RepositoryFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[2] + "\\" + m_table.CustomName + "Repository.cs";
                m_templateFilePath = ThisApp.AppSetting.NeoRepositoryTemplate;
            }
            
            
            m_templateContent = string.Empty;
            m_templateContent = Utility.ReadFile(m_templateFilePath);
            m_templateContent = m_templateContent.Replace(TheText.ModelName, m_table.CustomName);
            m_templateContent = m_templateContent.Replace(TheText.modelnameLowerCase, m_table.CustomName.ToLower());
            if (m_templateContent.Contains(TheText.KeyType) == true)
            {
                m_templateContent = m_templateContent.Replace(TheText.KeyType, m_table.KeyType);
            }
            if (ThisApp.AppSetting.UseEF)
            {
                if (m_templateContent.Contains(TheText.EFReturnType))
                {
                    if (ThisApp.AppSetting.EfReturnType == EFType.List)
                        m_templateContent = m_templateContent.Replace(TheText.EFReturnType, "List");
                    if (ThisApp.AppSetting.EfReturnType == EFType.IQueryable)
                        m_templateContent = m_templateContent.Replace(TheText.EFReturnType, "IQueryable");
                    if (ThisApp.AppSetting.EfReturnType == EFType.IEnumerable)
                        m_templateContent = m_templateContent.Replace(TheText.EFReturnType, "IEnumerable");
                }
            }
            //Nếu dùng dapper thuần( sql script)
            if (ThisApp.AppSetting.GenSqlScript)
            {
                //insert
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("string insert = string.Format(@\"");
                string sql = Insert();
                m_strBuilder.Append(sql);
                m_strBuilder.Append("\");");
                m_strBuilder.Append(Environment.NewLine);
                m_strBuilder.Append("\t\t\tConnection.Execute(insert, entity, Transaction);");
                m_templateContent = m_templateContent.Replace(TheText.SqlCreate, m_strBuilder.ToString());
                //select
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("string select = string.Format(@\"");
                sql = Select();
                m_strBuilder.Append(sql);
                m_strBuilder.Append("\");");
                m_strBuilder.Append(Environment.NewLine);
                m_strBuilder.Append("\t\t\treturn Connection.Query<");
                m_strBuilder.Append(m_table.CustomName);
                m_strBuilder.Append(">(select, transaction: Transaction).ToList();");
                m_strBuilder.Append(Environment.NewLine);
                m_templateContent = m_templateContent.Replace(TheText.SqlGetAll, m_strBuilder.ToString());
                //FindById
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("return Connection.Query<");
                m_strBuilder.Append(m_table.CustomName);
                m_strBuilder.Append(">(\"select * from");
                m_strBuilder.Append(m_table.CustomName);
                m_strBuilder.Append(" where ");
                m_strBuilder.Append(m_table.PrimaryKeyCol);
                m_strBuilder.Append(" = @Id)\"");
                m_strBuilder.Append(", param: new { Id = id }, transaction: Transaction).FirstOrDefault();");
                m_templateContent = m_templateContent.Replace(TheText.SqlFindById, m_strBuilder.ToString());
                //Find by entity
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("return Connection.Query<");
                m_strBuilder.Append(m_table.CustomName);
                m_strBuilder.Append(">(\"select * from ");
                m_strBuilder.Append(m_table.CustomName);
                m_strBuilder.Append(" where ");
                m_strBuilder.Append(m_table.PrimaryKeyCol);
                m_strBuilder.Append(" = @Id)\"");
                m_strBuilder.Append(", param: new { Id = ");
                m_strBuilder.Append("entity.");
                m_strBuilder.Append(m_table.PrimaryKeyCol);
                m_strBuilder.Append(" }, transaction: Transaction).FirstOrDefault();");
                m_templateContent = m_templateContent.Replace(TheText.SqlFind, m_strBuilder.ToString());
                //remove by Id
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("Connection.Execute(\"delete from ");
                m_strBuilder.Append(m_table.CustomName);
                m_strBuilder.Append(" where ");
                m_strBuilder.Append(m_table.PrimaryKeyCol);
                m_strBuilder.Append(" = @Id\", param: new { Id = id }, transaction: Transaction);");
                m_templateContent = m_templateContent.Replace(TheText.SqlRemoveById, m_strBuilder.ToString());
                //remove by entity
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("Remove(entity.");
                m_strBuilder.Append(m_table.PrimaryKeyCol);
                m_strBuilder.Append(");");
                m_templateContent = m_templateContent.Replace(TheText.SqlRemove, m_strBuilder.ToString());
                //update 
                m_strBuilder = new StringBuilder();
                m_strBuilder.Append("string update = string.Format(@\"");
                sql = Update();
                m_strBuilder.Append(sql);
                m_strBuilder.Append("\");");
                m_strBuilder.Append(Environment.NewLine);
                m_strBuilder.Append("\t\t\tConnection.Execute(update, entity, transaction: Transaction);");
                m_templateContent = m_templateContent.Replace(TheText.SqlUpdate, m_strBuilder.ToString());

            }
            if (ThisApp.currentSession.OverwriteRepository == false)
            {
                if (File.Exists(filepath))
                {
                    return m_templateContent;
                }
            }
            if (ThisApp.AppSetting.AutoCreateFile)
            {
                Utility.WriteToCsFile(m_templateContent, filepath, FileType.Repository);
            }
            return m_templateContent;
        }
        #endregion
        #region BuildInterface
        public string BuildInterface()
        {
            m_strBuilder = new StringBuilder();
            string temp = string.Empty;
            string filepath = string.Empty;
            if (m_DbType== DatabaseType.SQL)
            {
                temp = Utility.ReadFile(ThisApp.AppSetting.SqlInterfaceTemplate);
                filepath = ThisApp.AppSetting.InterfaceFolder +"\\" + ThisApp.AppSetting.getSubFolferName()[0] + "\\I" + m_table.CustomName + "Repository.cs";
            }
            else if (m_DbType == DatabaseType.Mongo)
            {
                temp = Utility.ReadFile(ThisApp.AppSetting.MongoInterfaceTemplate);
                filepath = ThisApp.AppSetting.InterfaceFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[1] + "\\I" + m_table.CustomName + "Repository.cs";
            }
            else if (m_DbType == DatabaseType.Neo)
            {
                temp = Utility.ReadFile(ThisApp.AppSetting.NeoInterfaceTemplate);
                filepath = ThisApp.AppSetting.InterfaceFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[2] + "\\I" + m_table.CustomName + "Repository.cs";
            }
            if (string.IsNullOrWhiteSpace(temp)) return string.Empty;
            m_templateContent = temp.Replace(TheText.ModelName, m_table.CustomName);
            m_templateContent = m_templateContent.Replace(TheText.modelnameLowerCase, m_table.CustomName.ToLower());
            m_templateContent = m_templateContent.Replace(TheText.KeyType, m_table.KeyType);
            if (ThisApp.currentSession.OverwriteInterfaceRepository == false)
            {
                if (File.Exists(filepath))
                {
                    return m_templateContent;
                }
            }
            if (ThisApp.AppSetting.AutoCreateFile)
            {
                Utility.WriteToCsFile(m_templateContent, filepath, FileType.Interface);
            }
            return m_templateContent;
        }
        #endregion
        #region Class
        protected string BuildEntitiesClass()
        {
            StringBuilder m_strBuilder = new StringBuilder();
            if (m_lstColumn == null) return string.Empty;
            m_lstColumn = m_lstColumn.OrderByDescending(p => p.IsPrimary).ToList();
            foreach (Column column in m_lstColumn)
            {
                if (column.Datatype == "int")
                {
                    if (column.IsPrimary)
                    {
                        m_strBuilder.Append("public int ");
                    }
                    else
                    {
                        m_strBuilder.Append("\t\tpublic Nullable<int> ");
                    }
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "float" || column.Datatype == "real")
                {

                    m_strBuilder.Append("\t\tpublic double? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "decimal")
                {

                    m_strBuilder.Append("\t\tpublic decimal? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "bit")
                {

                    m_strBuilder.Append("\t\tpublic bool? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "timestamp" || column.Datatype == "smalldatetime" || column.Datatype == "datetime" || column.Datatype == "datetime2" || column.Datatype == "date")
                {
                    m_strBuilder.Append("\t\tpublic DateTime? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "datetimeoffset")
                {
                    m_strBuilder.Append("\t\tpublic DateTimeOffset? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "smallint")
                {
                    m_strBuilder.Append("\t\tpublic short? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "varbinary" || column.Datatype == "tinyint" || column.Datatype == "image" || column.Datatype == "binary")
                {
                    m_strBuilder.Append("\t\tpublic byte[] ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "smallmoney" || column.Datatype == "money" || column.Datatype == "numeric")
                {
                    m_strBuilder.Append("\t\tpublic decimal? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "text" || column.Datatype == "ntext" || column.Datatype == "char" || column.Datatype == "nchar" || column.Datatype == "ntext" || column.Datatype == "nvarchar" || column.Datatype == "varchar")
                {
                    if(column.IsPrimary)
                    {
                        m_strBuilder.Append("\tpublic string ");
                    }
                    else
                    {
                        m_strBuilder.Append("\t\tpublic string ");
                    }
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "bigint")
                {
                    m_strBuilder.Append("\t\tpublic long? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "time")
                {
                    m_strBuilder.Append("\t\tpublic TimeSpan? ");
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }
                if (column.Datatype == "uniqueidentifier")
                {
                    if (column.IsPrimary)
                    {
                        m_strBuilder.Append("public Guid ");
                    }
                    else
                    {
                        m_strBuilder.Append("\t\tpublic Guid? ");
                    }
                    m_strBuilder.Append(column.Name);
                    m_strBuilder.Append(" { get; set; }");
                    m_strBuilder.Append(Environment.NewLine);
                    continue;
                }

            }

            return m_strBuilder.ToString();
        }
        #endregion
        #region BuildEntityClass
        public string CreateClass()
        {
            string filepath = string.Empty;
            if (m_DbType== DatabaseType.SQL)
            {
                m_templateFilePath = ThisApp.AppSetting.SqlModelTemplateFile;
                filepath = ThisApp.AppSetting.ModelFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[0] + "\\" + m_table.CustomName + ".cs";
            }
            else if(m_DbType == DatabaseType.Mongo)
            {
                m_templateFilePath = ThisApp.AppSetting.MongoModelTemplateFile;
                filepath = ThisApp.AppSetting.ModelFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[1] + "\\" + m_table.CustomName + ".cs";
            }
            else if (m_DbType == DatabaseType.Neo)
            {
                m_templateFilePath = ThisApp.AppSetting.NeoModelTemplateFile;
                filepath = ThisApp.AppSetting.ModelFolder + "\\" + ThisApp.AppSetting.getSubFolferName()[2] + "\\" + m_table.CustomName + ".cs";
            }
            m_templateContent = Utility.ReadFile(m_templateFilePath);
            string Entityclass = BuildEntitiesClass();
            m_templateContent = m_templateContent.Replace(TheText.ModelName, m_table.CustomName);
            m_templateContent = m_templateContent.Replace(TheText.modelnameLowerCase, m_table.CustomName.ToLower());
            m_templateContent = m_templateContent.Replace(TheText.TableName, m_table.RealName);
            m_templateContent = m_templateContent.Replace("[ModelClass]", Entityclass);
            
            if (ThisApp.currentSession.OverwriteClass == false)
            {
                if (File.Exists(filepath))
                {
                    return m_templateContent;
                }
            }
            if (ThisApp.AppSetting.AutoCreateFile)
            {
                    Utility.WriteToCsFile(m_templateContent, filepath, FileType.Model);
            }
            return m_templateContent;
        }
        #endregion
        #region Controller
        public string CreateController()
        {
            string filepath = ThisApp.AppSetting.ControllerFolder + "\\" + m_table.CustomName + "Controller.cs";
            m_strBuilder = new StringBuilder();
            m_templateContent = string.Empty;
            m_templateContent = Utility.ReadFile(ThisApp.AppSetting.ControllerTemplate);
            if (string.IsNullOrWhiteSpace(m_templateContent))
                return string.Empty;
            m_templateContent = m_templateContent.Replace(TheText.ModelName, m_table.CustomName);
            m_templateContent = m_templateContent.Replace(TheText.modelnameLowerCase, m_table.CustomName.ToLower());
            m_templateContent = m_templateContent.Replace(TheText.KeyType, m_table.KeyType);
            if (ThisApp.AppSetting.AutoCreateFile == true)
            {
                if (File.Exists(filepath))
                {
                    if (ThisApp.currentSession.OverWriteController)
                    {
                        Utility.WriteToFile(filepath, m_templateContent);
                    }
                }
                else
                {
                    Utility.WriteToFile(filepath, m_templateContent);
                }
            }
            return m_templateContent;
        }
        #endregion

        #region ReactJs
        public string CreateReactJsModel(List<Column> columns,string modelName)
        {
            string filepath = ThisApp.AppSetting.ReactJsModelFile;
            m_strBuilder = new StringBuilder();
            m_templateContent = Utility.ReadFile(filepath);
            if (string.IsNullOrWhiteSpace(m_templateContent))
                return string.Empty;
            string className = string.Empty;
            if(m_DbType== DatabaseType.SQL && string.IsNullOrEmpty(modelName))
            {
                className = m_table.CustomName;
            }
            else
            {
                className = modelName;
            }
            m_strBuilder.Append($"export interface {className}");
            m_strBuilder.Append(Environment.NewLine);
            m_strBuilder.Append("{");
            m_strBuilder.Append(Environment.NewLine);
            string name = string.Empty;
            string type = string.Empty;
            foreach (Column col in columns)
            {
                if (Array.IndexOf(NumberDataType, col.Datatype.ToString().ToLower()) > -1)
                {
                    if (col.IsArray)
                    {
                        name += $"\t {col.Name}: number[]," + Environment.NewLine;
                    }
                    else
                        name += $"\t {col.Name}: number," + Environment.NewLine;
                }
                else
                if (Array.IndexOf(StringDataType, col.Datatype.ToString().ToLower()) > -1)
                {
                    if (col.IsArray)
                    {
                        name += $"\t {col.Name}: string[]," + Environment.NewLine;
                    }
                    else
                        name += $"\t {col.Name}: string," + Environment.NewLine;
                }
                else
                if (Array.IndexOf(BoolDataType, col.Datatype.ToString().ToLower()) > -1)
                {
                    name += $"\t {col.Name}: boolean," + Environment.NewLine;
                }
                else
                {
                    if (col.IsArray)
                    {
                        name += $"\t {col.Name}: {col.Datatype}[]," + Environment.NewLine;
                    }
                    else
                        name += $"\t {col.Name}: {col.Datatype}," + Environment.NewLine;
                }
            }
            m_strBuilder.Append(name);
            m_strBuilder.Append(Environment.NewLine);
            m_strBuilder.Append("}");
            m_strBuilder.Append(Environment.NewLine);
            m_strBuilder.Append(TheText.AppendNewHere);
            m_templateContent = m_templateContent.Replace(TheText.AppendNewHere, m_strBuilder.ToString());

            if (ThisApp.AppSetting.AutoCreateFile == true)
            {
                if (File.Exists(filepath))
                {
                    if (ThisApp.currentSession.OverWriteReactModel)
                    {
                        Utility.WriteToFile(filepath, m_templateContent);
                    }
                }
                else
                {
                    Utility.WriteToFile(filepath, m_templateContent);
                }
            }
            return m_templateContent;
        }
        #endregion
        #region UnitTest
        public string CreateUnitTestClass()
        {
            string filepath = $"{ThisApp.AppSetting.UnitTestFolder}\\Test{m_table.CustomName}.cs";
            m_strBuilder = new StringBuilder();
            m_templateContent = Utility.ReadFile(ThisApp.AppSetting.UnitTestFileTemplate);
            if (string.IsNullOrWhiteSpace(m_templateContent))
                return string.Empty;
            m_templateContent = m_templateContent.Replace(TheText.ModelName, m_table.CustomName);
            if (ThisApp.AppSetting.AutoCreateFile == true)
            {
                if (File.Exists(filepath))
                {
                    if (ThisApp.currentSession.OverWriteUnitTest)
                    {
                        Utility.WriteToFile(filepath, m_templateContent);
                    }
                }
                else
                {
                    Utility.WriteToFile(filepath, m_templateContent);
                }
            }
            return m_templateContent;
        }
        #endregion
        #region Business
        public string CreateBusinessClass()
        {
            string filepath = $"{ThisApp.AppSetting.BusinessFolder}\\{m_table.CustomName}Business.cs";
            m_strBuilder = new StringBuilder();
            m_templateContent = Utility.ReadFile(ThisApp.AppSetting.BusinessTemplate);
            if (string.IsNullOrWhiteSpace(m_templateContent))
                return string.Empty;
            m_templateContent = m_templateContent.Replace(TheText.ModelName, m_table.CustomName);
            m_templateContent = m_templateContent.Replace(TheText.modelnameLowerCase, m_table.CustomName.ToLower());
            if (ThisApp.AppSetting.AutoCreateFile == true)
            {
                if (File.Exists(filepath))
                {
                    if (ThisApp.currentSession.OverWriteBusiness)
                    {
                        Utility.WriteToFile(filepath, m_templateContent);
                    }
                }
                else
                {
                    Utility.WriteToFile(filepath, m_templateContent);
                }
            }
            return m_templateContent;
        }
        public string CreateBusinessInterface()
        {
            string filepath = $"{ThisApp.AppSetting.IBusinessFolder}\\I{m_table.CustomName}Business.cs";
            m_strBuilder = new StringBuilder();
            m_templateContent = Utility.ReadFile(ThisApp.AppSetting.IBusinessTemplate);
            if (string.IsNullOrWhiteSpace(m_templateContent))
                return string.Empty;
            m_templateContent = m_templateContent.Replace(TheText.ModelName, m_table.CustomName);
            m_templateContent = m_templateContent.Replace(TheText.modelnameLowerCase, m_table.CustomName.ToLower());
            if (ThisApp.AppSetting.AutoCreateFile == true)
            {
                if (File.Exists(filepath))
                {
                    if (ThisApp.currentSession.OverWriteBusiness)
                    {
                        Utility.WriteToFile(filepath, m_templateContent);
                    }
                }
                else
                {
                    Utility.WriteToFile(filepath, m_templateContent);
                }
            }
            return m_templateContent;
        }
        #endregion
        #region Dependency Injection
        public string CreateDependencyInjection()
        {
            string filepath = $"{ThisApp.AppSetting.StartUpFile}";
            m_strBuilder = new StringBuilder();
            m_templateContent = Utility.ReadFile(ThisApp.AppSetting.StartUpFile);
            if (string.IsNullOrWhiteSpace(m_templateContent))
                return string.Empty;
            if (m_templateContent.Contains($"I{m_table.CustomName}Repository"))
                return null;
            string line = string.Empty;
            if(m_DbType== DatabaseType.SQL)
            {
                if (m_templateContent.Contains($"SqlI.I{m_table.CustomName}Repository"))
                    return null;
                line = $"services.AddSingleton<SqlI.I{m_table.CustomName}Repository, SqlR.{m_table.CustomName}Repository>();";
                m_strBuilder.Append(line);
                m_strBuilder.Append(Environment.NewLine);
                m_strBuilder.Append("\t\t\t");
                m_strBuilder.Append(TheText.AppendSqlDI);
                m_templateContent = m_templateContent.Replace(TheText.AppendSqlDI, m_strBuilder.ToString());
            }
            if (m_DbType == DatabaseType.Mongo)
            {
                if (m_templateContent.Contains($"MongoI.I{m_table.CustomName}Repository"))
                    return null;
                line = $"services.AddSingleton<MongoI.I{m_table.CustomName}Repository, MongoR.{m_table.CustomName}Repository>();";
                m_strBuilder.Append(line);
                m_strBuilder.Append(Environment.NewLine);
                m_strBuilder.Append("\t\t\t");
                m_strBuilder.Append(TheText.AppendMongoDI);
                m_templateContent = m_templateContent.Replace(TheText.AppendMongoDI, m_strBuilder.ToString());
            }
            if (m_DbType == DatabaseType.Neo)
            {
                if (m_templateContent.Contains($"NeoI.I{m_table.CustomName}Repository"))
                    return null;
                line = $"services.AddSingleton<NeoI.I{m_table.CustomName}Repository, NeoR.{m_table.CustomName}Repository>();";
                m_strBuilder.Append(line);
                m_strBuilder.Append(Environment.NewLine);
                m_strBuilder.Append("\t\t\t");
                m_strBuilder.Append(TheText.AppendNeoDI);
                m_templateContent = m_templateContent.Replace(TheText.AppendNeoDI, m_strBuilder.ToString());
            }
            
            
            if (ThisApp.AppSetting.AutoCreateFile == true)
            {
                if (File.Exists(filepath))
                {
                    if (ThisApp.currentSession.OverWriteDependencyInjection)
                    {
                        Utility.WriteToFile(filepath, m_templateContent);
                    }
                }
                else
                {
                    Utility.WriteToFile(filepath, m_templateContent);
                }
            }
            return m_templateContent;
        }
        public static ColumnFromCsharpClass GetColumnFromCSharpClass(RichTextBox richTextBox)
        {
            ColumnFromCsharpClass columnFromCsharpClass = new ColumnFromCsharpClass();
            int start = 0;
            for (int i = 0; i < richTextBox.Lines.Count(); i++)
            {
                string line = richTextBox.Lines[i].Replace(" ", "");
                if (line.Trim().Contains("publicclass"))
                {
                    start = i + 1;
                    string[] firstline = richTextBox.Lines[i].Trim().Split(' ');
                    if (firstline.Length > 2)
                    {
                        columnFromCsharpClass.ClassName = firstline[2];
                    }
                    break;
                }
            }
            List<Column> lstColumn = new List<Column>();
            for (int i = start; i < richTextBox.Lines.Count(); i++)
            {
                string line = richTextBox.Lines[i].Trim();
                if (line.Contains("(")) continue;
                string[] strArr = line.Split(' ');
                if (strArr.Length > 2)
                {
                    Column col = new Column();
                    col.Name = strArr[2];
                    col.Datatype = strArr[1];
                    if (col.Datatype.Contains("<"))
                    {
                        strArr[1] = strArr[1].Replace('>', ' ');
                        string[] temp = strArr[1].Split('<');
                        col.Datatype = temp[1].Trim();
                        col.IsArray = true;
                    }
                    lstColumn.Add(col);
                }
            }
            columnFromCsharpClass.Columns = lstColumn;
            return columnFromCsharpClass;
        }
        #endregion
        public readonly string[] NumberDataType = {"int","long","double","money","decimal","Double","bigint" };
        public readonly string[] StringDataType = { "string", "datetime", "DateTime","char","nchar","varchar","nvarchar", "uniqueidentifier","text","time", "Guid" };
        public readonly string[] BoolDataType = { "bit", "bool", "Boolean" };
    }
    public class ColumnFromCsharpClass
    {
        public string ClassName { get; set; }
        public List<Column> Columns { get; set; }
    }
}
