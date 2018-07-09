using System.Web.Http;
using DataAccess.Services;
using DataAccess.Repositories;
using DataAccess.Interfaces.IServices;
using DataAccess.Interfaces.IRepositories;
using DataAccess.Models;
using API.Services;

namespace API.Controllers
{
    public class HomeController : ApiController
    {
        private IUserService userService;
        private IUserRepository userRepository;
        public HomeController()
        {
            userService = new UserService();
            userRepository = new UserRepository();
        }

        [HttpPost]
        [Route("api/Home/Register")]
        public void Register([FromBody]User user)
        {
            userService.Register(user);
            EmailService.SendRegistrationInformation(user);
        }
        
        [HttpPost]
        [Route("api/Home/Login")]
        public string Login([FromBody]User user)
        {
            if (userService.Login(user)) return userService.GetToken(user);
            return null;
        }
    }
}
