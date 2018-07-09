using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess
{
    public static class DataManager
    {
        public static List<Project> Projects { get; set; }
        public static List<User> Users { get; set; }

        private static string _projects => "Projects";
        private static string _users => "Users";

        public static void Initialize()
        {
            Projects = new List<Project>();
            Users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Email = "aurimas.vainius@gmail.com",
                    Password = "0000",
                    UserRole = Enums.Roles.Administrator
                }
            };
        }
    }
}
