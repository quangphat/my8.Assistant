using MongoDB.Driver;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Model
{
    public class ThisApp
    {
        private static IDbConnection sqlCon;
        private static IMongoDatabase mongoCon;
        private static MongoClient mongoClient;
        private static GraphClient neoClient;
        public static DatabaseInfo SqlDbInfo;
        public static DatabaseInfo MongoDbInfo;
        public static DatabaseInfo NeoDbInfo;
        private static ApplicationSetting appSetting;
        public static DatabaseType DbType;
        public static Project Project = new Project();
        public static IDbConnection SqlCon
        {
            get
            {
                if (sqlCon == null)
                    sqlCon = new SqlConnection($"server={SqlDbInfo.Server};database={SqlDbInfo.DatabaseName};User={SqlDbInfo.UserName};password={SqlDbInfo.Password};");
                return sqlCon;
            }
        }
        public static IMongoDatabase MongoCon
        {
            get
            {
                
                if (mongoCon==null)
                {
                    mongoClient = new MongoClient($"mongodb://{MongoDbInfo.UserName}:{MongoDbInfo.Password}@{MongoDbInfo.Server}");
                    mongoCon = mongoClient.GetDatabase(MongoDbInfo.DatabaseName);
                }
                return mongoCon;
            }
        }
        public static GraphClient NeoClient
        {
            get
            {
                if(neoClient==null)
                {
                    try
                    {
                        neoClient = new GraphClient(new Uri($"{NeoDbInfo.Server}"), NeoDbInfo.UserName, NeoDbInfo.Password);
                        neoClient.Connect();
                    }
                    catch
                    {
                        return null;
                    }
                }
                return neoClient;
            }
        }
        public static ApplicationSetting AppSetting
        {
            get
            {
                appSetting = Utility.GetCurrentSetting(ThisApp.Project.Name);
                if (appSetting == null)
                    return new ApplicationSetting();
                return appSetting;
            }
            set
            {
                appSetting = value;
            }
        }
        public static ApplicationSession currentSession;
        public static ApplicationSession getSessionByDbType(DatabaseType dbType)
        {
            ApplicationSession session = ProjectSessions.FirstOrDefault(p => p.ProjectId == Project.Id);
            return session;
        }
        public static List<ApplicationSession> projectSessions;
        public static List<ApplicationSession> ProjectSessions
        {
            get
            {
                projectSessions = Utility.GetProjectSessions(ThisApp.Project.Name);
                if (projectSessions == null)
                    return new List<ApplicationSession>();
                return projectSessions;
            }
        }
        public static string usingSytem = "using System;" + Environment.NewLine;
        public static string usingDapperExtension = "using DapperExtensions.Mapper;" + Environment.NewLine;
        public static string usingEFBuilder = "using Microsoft.EntityFrameworkCore.Metadata.Builders;" + Environment.NewLine;
        public static string usingCollection = "using System.Collections.Generic;" + Environment.NewLine;
        public static string usingLinq = "using System.Linq;" + Environment.NewLine;
        public static string usingDapper = "using Dapper;" + Environment.NewLine;
        public static string usingCommon = "using System.Data.Common;" + Environment.NewLine;
    }
}
