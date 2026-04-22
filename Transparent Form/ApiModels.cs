using System.Collections.Generic;

namespace Transparent_Form
{
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