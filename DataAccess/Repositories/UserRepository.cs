using DataAccess.Interfaces.IRepositories;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            return DataManager.Users;
        }

        public User GetUser(int id)
        {
            return DataManager.Users.FirstOrDefault(c => c.Id == id);
        }

        public User GetUser(string email, string password)
        {
            return DataManager.Users.FirstOrDefault(c => c.Email == email && c.Password == password);
        }
    }
}
