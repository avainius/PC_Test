using DataAccess.Models;

namespace DataAccess.Interfaces.IServices
{
    public interface IUserService
    {
        bool Login(User user);
        bool IsAdmin(User user);
        string GetToken(User user);
        User Register(User user);
        void CreateOrUpdate(User user);
        void Update(User user);
        void Delete(User user);
        void Delete(int id);
    }
}
