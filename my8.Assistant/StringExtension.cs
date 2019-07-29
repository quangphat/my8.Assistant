using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant
{
    public static class StringExtension
    {


        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return Char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }

        public static string[] GetConsole(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return null;
            input = input.Trim();
            string[] arr = input.Split(new Char[] { ' ', '\n', '\t' });
            List<string> outputs = new List<string>();
            foreach (string s in arr)
            {
                if (!string.IsNullOrWhiteSpace(s))
                    outputs.Add(s);
            }
            return outputs.ToArray();
        }

        public static List<ConsoleObject> GetConsoleObjectMultiple(string input)
        {
            var commands = GetConsole(input);
            if (commands == null || commands.Length < 3)
                return null;
            string[] objectTypes = null;
            string[] objectNames = null;
            if (commands[1] != null)
            {
                objectTypes = commands[1].Split(new Char[] { ',' });
            }
            if (commands[2] != null)
            {
                var objNamesArr = commands[2];
                if (commands.Length > 3)
                {
                    objNamesArr = string.Join(",", commands, 2, commands.Length - 2);
                }
                objectNames = objNamesArr.Split(new Char[] { ',' });
                objectNames = objectNames.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            }
            List<ConsoleObject> objects = new List<ConsoleObject>();
            if (objectTypes != null && objectNames != null)
            {
                for (int i = 0; i < objectTypes.Length; i++)
                {
                    for (int j = 0; j < objectNames.Length; j++)
                    {
                        string[] command = new string[] { commands[0], objectTypes[i], objectNames[j] };
                        objects.Add(
                            GetConsoleObjectOne(command)
                        );
                    }
                }
            }
            return objects;
        }

        public static ConsoleObject GetConsoleObjectOne(string[] commands)
        {
            if (commands == null || commands.Length < 3)
                return null;
            var consoleObj = new ConsoleObject();
            if (!string.IsNullOrWhiteSpace(commands[0]))
            {
                var type = ConsoleDefinition.console_defs.FirstOrDefault(p => p.Key == commands[0].Trim().ToLower());
                if (type.Value == ConsoleDefinition.create)
                {
                    consoleObj.Command = CommandType.create;
                }
                else if (type.Value == ConsoleDefinition.preview)
                {
                    consoleObj.Command = CommandType.preview;
                }
                else if (type.Value == ConsoleDefinition.copy)
                {
                    consoleObj.Command = CommandType.copy;
                }
                else
                {
                    consoleObj.canExcuteCommand = false;
                    consoleObj.error = $"The command {commands[0]} is invalid";
                    return consoleObj;
                }
            }
            else
            {
                consoleObj.canExcuteCommand = false;
                consoleObj.error = $"The command {commands[0]} is invalid";
                return consoleObj;
            }
            if (!string.IsNullOrWhiteSpace(commands[1]))
            {
                var type = ConsoleDefinition.console_defs.FirstOrDefault(p => p.Key == commands[1].Trim().ToLower());
                if (type.Value == ConsoleDefinition.controller)
                {
                    consoleObj.ObjectType = ObjectType.controller;
                }
                else if (type.Value == ConsoleDefinition.model)
                {
                    consoleObj.ObjectType = ObjectType.model;
                }
                else if (type.Value == ConsoleDefinition.view)
                {
                    consoleObj.ObjectType = ObjectType.view;
                }
                else if (type.Value == ConsoleDefinition.react_component)
                {
                    consoleObj.ObjectType = ObjectType.react_component;
                }
                else if (type.Value == ConsoleDefinition.react_interface)
                {
                    consoleObj.ObjectType = ObjectType.react_interface;
                }
                else if (type.Value == ConsoleDefinition.react_repository)
                {
                    consoleObj.ObjectType = ObjectType.react_repository;
                }
                else if (type.Value == ConsoleDefinition.biz_interface)
                {
                    consoleObj.ObjectType = ObjectType.biz_interface;
                }
                else if (type.Value == ConsoleDefinition.biz_class)
                {
                    consoleObj.ObjectType = ObjectType.biz_class;
                }
                else if (type.Value == ConsoleDefinition.biz_dependency_injecttion)
                {
                    consoleObj.ObjectType = ObjectType.biz_di;
                }
                else if (type.Value == ConsoleDefinition.rp_class)
                {
                    consoleObj.ObjectType = ObjectType.rp_class;
                }
                else if (type.Value == ConsoleDefinition.rp_dependency_injection)
                {
                    consoleObj.ObjectType = ObjectType.rp_di;
                }
                else if (type.Value == ConsoleDefinition.rp_interface)
                {
                    consoleObj.ObjectType = ObjectType.rp_interface;
                }
                else if (type.Value == ConsoleDefinition.mapper)
                {
                    consoleObj.ObjectType = ObjectType.mapper;
                }
                else
                {
                    consoleObj.canExcuteCommand = false;
                    consoleObj.error = $"The object {commands[1]} is invalid";
                    return consoleObj;
                }
            }
            else
            {
                consoleObj.canExcuteCommand = false;
                consoleObj.error = $"The object {commands[1]} is invalid";
                return consoleObj;
            }
            if (!string.IsNullOrWhiteSpace(commands[2]))
            {
                consoleObj.ObjectName = commands[2];
            }
            else
            {
                consoleObj.canExcuteCommand = false;
                consoleObj.error = $"The object name must be not empty";
                return consoleObj;
            }
            return consoleObj;
        }

    }
}
