using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MongoDB.Bson;
using MongoDB.Driver;

namespace my8.Assistant.Model
{
    public class DatabaseHelper
    {
        public static List<Table> lstSqlTable;
        public static List<Table> lstMongoCollection;
        public static List<Table> lstNeoNode;
        public static List<Table> lstNeoRelationship;
        public DatabaseHelper()
        { }
        public DatabaseHelper(List<DatabaseInfo> lstDbInfo)
        {
            DatabaseInfo Sql = lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.SQL);
            if(Sql!=null)
            {
                ThisApp.SqlDbInfo = Sql;
                
            }
            DatabaseInfo Mongo = lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.Mongo);
            if(Mongo!=null)
            {
                ThisApp.MongoDbInfo = Mongo;
            }
            DatabaseInfo Neo = lstDbInfo.FirstOrDefault(p => p.DbType == DatabaseType.Neo);
            if (Neo != null)
            {
                ThisApp.NeoDbInfo = Neo;
            }
        }
        public void GetAllTableType()
        {
            lstSqlTable = GetTable(DatabaseType.SQL);
            lstMongoCollection = GetTable(DatabaseType.Mongo);
            lstNeoNode = GetTable(DatabaseType.Neo);
        }
        public List<Table> GetTable(DatabaseType dbType)
        {
            if(dbType== DatabaseType.SQL)
            {
                string getSqlTableQuery = string.Format(@"use {0}
                                select t.name as RealName,p.Column_Name as PrimaryKeyCol,dt.Data_Type as KeyType,TableType =1 from 
                                (SELECT p.*, row_number() over (partition by Table_Name order by Column_Name desc) as seqnum
                                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE p) p
                                right join sys.Tables t on p.Table_Name = t.Name and p.seqnum=1
                                left join (SELECT distinct(COLUMN_NAME) as colName, DATA_TYPE,Table_Name 
                                FROM INFORMATION_SCHEMA.COLUMNS) dt on p.COLUMN_NAME = dt.colName and dt.Table_Name = t.Name
                                where t.Name<>'sysdiagrams'", ThisApp.SqlDbInfo.DatabaseName);
                lstSqlTable = ThisApp.SqlCon.Query<Table>(getSqlTableQuery).ToList();
                return lstSqlTable;
            }
            if(dbType == DatabaseType.Mongo)
            {
                lstMongoCollection = GetCollections();
                return lstMongoCollection;
            }
            if(dbType == DatabaseType.Neo)
            {
                List<Table> lstNode = GetNeoNodes();
                List<Table> lstRelationShip = GetNeoRelationships();
                if (lstNode == null) return null;
                lstNeoNode = lstNode.Concat<Table>(lstRelationShip).ToList();
                return lstNeoNode;
            }
            return null;
        }
        private List<Table> GetCollections()
        {
            List<Table> collections = new List<Table>();
            IMongoDatabase _database = ThisApp.MongoCon;
            try
            {
                List<BsonDocument> lstBsonDoc = _database.ListCollectionsAsync().Result.ToList();
                foreach (BsonDocument collection in lstBsonDoc)
                {
                    string name = collection["name"].AsString;
                    Table table = new Table();
                    table.RealName = name;
                    table.TableType = TableType.table;
                    collections.Add(table);
                }
                return collections;
            }
            catch
            {
                return null;
            }
        }
        private List<Table> GetNeoNodes()
        {
            List<Table> lstNode = new List<Table>();
            if (ThisApp.NeoClient == null || ThisApp.NeoClient.IsConnected == false) return null;
            IEnumerable<string> lstName = ThisApp.NeoClient.Cypher
                .Match(@"(n) WITH DISTINCT labels(n) AS labels UNWIND labels AS label")
                .ReturnDistinct<string>("label")
                .OrderBy("label").Results;
            foreach(string name in lstName)
            {
                Table table = new Table();
                table.RealName = name;
                table.TableType = TableType.table;
                lstNode.Add(table);
            }
            return lstNode;
        }
        private List<Table> GetNeoRelationships()
        {
            List<Table> lstRelationShip = new List<Table>();
            if (ThisApp.NeoClient == null || ThisApp.NeoClient.IsConnected == false) return null;
            IEnumerable<string> lstName = ThisApp.NeoClient.Cypher
                .Match(@"(a)-[r]->(b)")
                .ReturnDistinct<string>("TYPE(r)").Results;
            foreach (string name in lstName)
            {
                Table table = new Table();
                table.RealName = name;
                table.TableType = TableType.RelationShip;
                lstRelationShip.Add(table);
            }
            return lstRelationShip;
        }
        public List<Column> GetSqlColumn(Table table)
        {
            string getColumnsQuery = string.Format("SELECT COLUMN_NAME as Name, DATA_TYPE as DataType FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'", table.RealName);
            List<Column> columns = ThisApp.SqlCon.Query<Column>(getColumnsQuery).ToList();
            if (columns != null)
            {
                foreach (Column col in columns)
                {
                    if (col.Name == table.PrimaryKeyCol || col.Name.ToLower()=="id")
                    {
                        col.IsPrimary = true;
                    }
                }
            }
            return columns;
        }
    }


}
