using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Extensions.Helpers;
using WebApp.Models;

namespace WebApp.Data
{
    public class ApplicationDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Seed()
        {
            // create database + apply migrations
            _context.Database.Migrate();

            // add example roles
            if (!_context.Roles.Any())
            {
                var roles = new[]
                {
                    RoleHelper.Admin,
                    RoleHelper.Employee,
                    RoleHelper.User
                };

                foreach (var role in roles)
                {
                    var roleName = new IdentityRole(role) { NormalizedName = RoleHelper.Normalize(role) };
                    _context.Roles.Add(roleName);
                }
            }

            _context.SaveChanges();
        }
    }
}
