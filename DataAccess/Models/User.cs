using DataAccess.Enums;
using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    [Serializable]
    public class User
    {
        public User()
        {
            Projects = new List<Project>();
        }

        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles UserRole { get; set; }
        public List<Project> Projects { get; set; }
    }
}
