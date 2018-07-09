using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces.IRepositories
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetByUser(string email);
        IEnumerable<Project> GetByUser(User user);
    }
}
