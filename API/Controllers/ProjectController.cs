using DataAccess.Interfaces.IRepositories;
using DataAccess.Interfaces.IServices;
using DataAccess.Models;
using DataAccess.Repositories;
using DataAccess.Services;
using System.Collections.Generic;
using System.Threading;
using System.Web.Http;

namespace API.Controllers
{
    public class ProjectController : ApiController
    {
        private IUserService userService;
        private IProjectService projectService;
        private IProjectRepository projectRepository;
        public ProjectController()
        {
            projectService = new ProjectService();
            projectRepository = new ProjectRepository();
            userService = new UserService();
        }

        [HttpPost]
        [BasicAuthenticationAttribute(true)]
        public void CreateProject([FromBody]Project project)
        {
            projectService.Create(project);
        }

        [HttpPost]
        [BasicAuthenticationAttribute(true)]
        [Route("api/Project/AssignToUser/{userId}/{projectId}")]
        public User AssignToUser(int userId, int projectId)
        {
            return projectService.AssignToUser(userId, projectId);
        }

        [Route("api/Project/GetMy")]
        [BasicAuthenticationAttribute]
        public IEnumerable<Project> GetMyProjects()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            return projectRepository.GetByUser(userName);
        }

        [Route("api/Project/GetAll")]
        [BasicAuthenticationAttribute(true)]
        public IEnumerable<Project> GetAll() => projectRepository.GetAll();
    }
}