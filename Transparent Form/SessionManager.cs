using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transparent_Form
{
    public static class SessionManager
    {
        public static string Token { get; set; }
        public static string UserType { get; set; }
        public static string UserName { get; set; }
    }

    public class ClassItem
    {
        public int Id { get; set; }
        public string className { get; set; }
    }

    public class ClassResponse
    {
        public bool success { get; set; }
        public List<ClassItem> data { get; set; }
        public string message { get; set; }
    }

    public class ClassArmItem
    {
        public int Id { get; set; }
        public string className { get; set; }
        public string classArmName { get; set; }
        public string isAssigned { get; set; }
    }

    public class ApiMessageResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
