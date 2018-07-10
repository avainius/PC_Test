using System.Collections.Generic;
using System.Web.Http;
using DataAccess.Services;
using DataAccess.Repositories;
using DataAccess.Interfaces.IServices;
using DataAccess.Interfaces.IRepositories;
using DataAccess.Models;
using DataAccess;

namespace API.Controllers
{
    /// <summary>
    /// Class for testing purposes
    /// </summary>
    [AdminAuthenticationAttribute]
    public class UserController : ApiController
    {
        private IUserService userService;
        private IUserRepository userRepository;
        public UserController()
        {
            userService = new UserService();
            userRepository = new UserRepository();
        }

        [HttpGet]
        [Route("api/User/GetAll")]
        public List<User> GetAll() => DataManager.Users;

        [HttpPost]
        [Route("api/User/Update")]
        public void Update([FromBody]User user)
        {
            userService.Update(user);
        }

        [HttpGet]
        public void Delete(int? id)
        {
            if (id == null) return;
            userService.Delete(id.Value);
        }
    }
}