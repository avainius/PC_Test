using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        User GetUser(int id);
        User GetUser(string email, string password);
        IEnumerable<User> GetAll();
    }
}
