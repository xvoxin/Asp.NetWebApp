using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Extensions.Helpers
{
    public static class RoleHelper
    {
        public const string User = "User";
        public const string Employee = "Employee";
        public const string Admin = "Admin";

        public static string Normalize(string name)
        {
            return name.ToUpper();
        }
    }
}
