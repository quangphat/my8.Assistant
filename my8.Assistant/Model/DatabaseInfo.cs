using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace my8.Assistant.Model
{
    public enum DatabaseType { SQL = 1, Mongo=2,Neo=3 };
    [Serializable]
    public class DatabaseInfo
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Server { get; set; }
        public string Port { get; set; }
        public DatabaseType DbType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }
        public bool IsSelecting { get; set; }
        public DateTime ConnectedTime { get; set; }
        public DatabaseInfo()
        {
            Server = Port = UserName = Password = DatabaseName = string.Empty;
            DbType = 0;
            IsSelecting = false;
            ConnectedTime = DateTime.Now;
        }
    }
}
