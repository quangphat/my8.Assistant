using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant.Model
{

    public enum TableType{table = 1,view =2,RelationShip=3};
    public class Table
    {
        public string RealName { get; set; }
        public string CustomName
        {
            get
            {
                if (TableType == TableType.RelationShip)
                    return $"{RealName.ToPascalCase()}Edge"; 
                return RealName.ToPascalCase();
            }
        }
        public string PrimaryKeyCol { get; set; }
        public string KeyType { get; set; }
        public TableType TableType { get; set; }
    }

    public class BaseColumn
    {
        public string Name { get; set; }
        public string Datatype { get; set; }
    }
    public class Column
    {
        public string Name { get; set; }
        public string Datatype { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsArray { get; set; }
        public string KeyType
        {
            get
            {
                if (Datatype == "uniqueidentifier")
                    return "Guid";
                else
                    return "int";
            }
        }
        public Column()
        {
            Name = Datatype = string.Empty;
            IsPrimary = false;
        }
    }
}
