using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant
{
    public enum ObjectType
    {
        controller = 1,
        model = 2,
        view = 3,
        biz_interface = 4,
        biz_class = 5,
        biz_di = 6,
        rp_interface = 7,
        rp_class = 8,
        rp_di = 9,
        react_model = 10,
        react_component = 11,
        mapper = 12
    }
    public enum CommandType
    {
        create = 1,
        preview = 2,
        copy = 3
    }
    public class ConsoleObject
    {
        public ConsoleObject()
        {
            canExcuteCommand = true;
        }
        public ObjectType ObjectType { get; set; }
        public string ObjectName { get; set; }
        public CommandType Command { get; set; }
        public bool canExcuteCommand { get; set; }
        public string error { get; set; }
    }
}
