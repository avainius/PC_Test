using DataAccess.Interfaces.IServices;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataAccess.Services
{
    public class UserService : IUserService
    {
        private List<User> Users => DataManager.Users;
        public void CreateOrUpdate(User user)
        {
            if(Users.Any(c => c.Id == user.Id))
            {
                Users[Users.FindIndex(c => c.Id == user.Id)] = user;
            }
            else
            {
                Users.Add(user);
            }
        }

        public void Delete(User user)
        {
            Delete(user.Id.Value);
        }

        public void Delete(int id)
        {
            if (Users.Any(c => c.Id == id))
            {
                Users.RemoveAt(Users.FindIndex(c => c.Id == id));
            }
            else
            {
                MessageBox.Show("User does not exist");
            }
        }

        public bool Login(User user)
        {
            return Users.Any(c => c.Email == user.Email && c.Password == user.Password);
            
        }

        public bool IsAdmin(User user)
        {
            return Users.FirstOrDefault(c => c.Email == user.Email)?.UserRole == Enums.Roles.Administrator;
        }

        public string GetToken(User user)
        {
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user.Email}:{user.Password}"));
            return token;
        }

        public User Register(User user)
        {
            if (Users.Any(c => c.Email == user.Email)) throw new Exception("A user with the specified email already exists");
            user.Id = Users.Any() ? Users.Max(c => c.Id) + 1 : 0;
            user.UserRole = Enums.Roles.Employee;
            CreateOrUpdate(user);
            return user;
        }

        public void Update(User user)
        {
            if (!Users.Any(c => c.Id == user.Id))
                throw new Exception("User not found");
            CreateOrUpdate(user);            
        }
    }
}
