using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Extensions.Helpers
{
    public class NameHelper
    {
        private static string FixedName(string name)
        {
            return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        }

        public static void ArtistFixedNames(Artist artist)
        {
            artist.Name = FixedName(artist.Name);
            artist.City = FixedName(artist.City);
        }

        public static void EmployeeFixedNames(Employee employee)
        {
            employee.Name = FixedName(employee.Name);
            employee.Surname = FixedName(employee.Surname);
            employee.City = FixedName(employee.City);
        }
    }
}
