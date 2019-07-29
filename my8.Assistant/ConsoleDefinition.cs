using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my8.Assistant
{
    public static class ConsoleDefinition
    {
        public const string controller = "controller";
        public const string model = "class";
        public const string view = "view";
        public const string react_component = "react-component";
        public const string react_repository = "react-repository";
        public const string react_interface = "react-interface";
        public const string biz_interface = "business-interface";
        public const string biz_class = "business";
        public const string rp_interface = "repository-interface";
        public const string rp_class = "repository-class";
        public const string biz_dependency_injecttion = "business-denpendency-injection";
        public const string rp_dependency_injection = "repository-denpendency-injection";
        public const string mapper = "mapper";
        public const string copy = "copy";
        public const string create = "create";
        public const string preview = "preview";


        public static IDictionary<string, string> console_defs = new Dictionary<string, string>
        {
            {"controller","controller" },
            { "view","view" },
            { "class","class" },
            {"model","class" },
            {"business-interface","business-interface" },
            {"biz-interface","business-interface" },
            {"bz-interface","business-interface" },
            {"business","business" },
             {"business-class","business" },
              {"biz-class","business" },
            { "biz","business" },
            { "bz","business" },
            {"repository-interface","repository-interface" },
            {"rp-interface","repository-interface" },
            {"repo-interface","repository-interface" },
            {"rp-class","repository-class" },
            {"rp","repository-class" },
            {"repo","repository-class" },
            {"repo-class","repository-class" },
            {"repository-class","repository-class" },
            {"repository","repository-class" },
            {"react-interface","react-interface" },
            {"react-model","react-interface" },
            {"react-class","react-interface" },
            {"react-repository","react-repository" },
            {"react-rp","react-repository" },
            {"rp-react","react-repository" },
            {"repository-react","react-repository" },
            {"react-component","react-component" },
            {"biz-di","business-denpendency-injection" },
            {"business-denpendency-injection","business-denpendency-injection" },
            { "biz-denpendency-injection","business-denpendency-injection" },
            {"rp-di","repository-denpendency-injection" },
            {"repository-denpendency-injection","repository-denpendency-injection" },
            {"rp-denpendency-injection","repository-denpendency-injection" },
            {"mapper","mapper" },
            {"create","create" },
            {"view-output","preview" },
            {"preview","preview" },
            {"copy","copy" }
        };
    }
}
