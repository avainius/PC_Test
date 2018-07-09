using DataAccess.Interfaces.IRepositories;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public IEnumerable<Project> GetAll() => DataManager.Projects;

        public IEnumerable<Project> GetByUser(string email)
        {
            return DataManager.Users.FirstOrDefault(c => c.Email == email)?.Projects;
        }

        public IEnumerable<Project> GetByUser(User user)
        {
            return GetByUser(user.Email);
        }
    }
}
