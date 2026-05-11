using System.Collections.Generic;

namespace Transparent_Form
{
    public class LoginResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string userType { get; set; }
        public LoginUserData data { get; set; }
    }

    public class LoginUserData
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string username { get; set; }
        public int classId { get; set; }
        public int classArmId { get; set; }
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
    public class TeacherResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<TeacherItem> data { get; set; }
    }
    public class TeacherItem
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string phoneNo { get; set; }

        public int classId { get; set; }
        public int classArmId { get; set; }

        public string className { get; set; }
        public string classArmName { get; set; }
    }

    public class AdminResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<AdminItem> data { get; set; }
    }

    public class AdminItem
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
    }
}