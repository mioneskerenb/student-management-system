using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transparent_Form
{
    public static class ApiSession
    {
        public static string Token { get; set; }
        public static string UserType { get; set; }
        public static int UserId { get; set; }
        public static string FullName { get; set; }
        public static int ClassId { get; set; }
        public static int ClassArmId { get; set; }
    }
}
