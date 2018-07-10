using API.Controllers;
using DataAccess;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using TechTalk.SpecFlow;

namespace API_Tests.steps
{
    [Binding]
    public class ProjectControllerSteps
    {
        public static string RegisteredUserKey = "ExistingUser";
        public static string NewProjectKey = "NewProject";
        public static string ControllerKey = "Controller";
        public static string ExceptionKey = "Exception";
        public static string ProjectListKey = "MyProjects";
        public static string AllProjectListKey = "AllProjects";
        public static string AdminUserKey = "AdminUser";

        public string NewProjectName = "Super Project";
        public static int RegisteredProjectCount = 5;

        public ProjectControllerSteps() { Init(); }

        [Given(@"I have a new project ready")]
        public void GivenIHaveANewProjectReady()
        {
            var project = new Project()
            {
                Name = NewProjectName,
                Description = "Very important project"
            };

            ScenarioContext.Current.Add(NewProjectKey, project);
        }
        
        [Given(@"There already is a project like the new one registered")]
        public void GivenThereAlreadyIsAProjectLikeTheNewOneRegistered()
        {
            DataManager.Projects.Add(new Project()
            {
                Name = NewProjectName,
                Description = "Very important project"
            });
        }
        
        [Given(@"I have an existing user with ID: (.*)")]
        public void GivenIHaveAnExistingUserWithID(int p0)
        {
            DataManager.Users.Add(new User()
            {
                Id = p0
            });
        }
        
        [Given(@"I have an existing project with ID: (.*)")]
        public void GivenIHaveAnExistingProjectWithID(int p0)
        {
            DataManager.Projects.Add(new Project()
            {
                Id = p0
            });
        }
        
        [When(@"I post the new project to create it")]
        public void WhenIPostTheNewProjectToCreateIt()
        {
            try
            {
                var controller = ScenarioContext.Current.Get<ProjectController>(ControllerKey);
                var newProject = ScenarioContext.Current.Get<Project>(NewProjectKey);

                controller.CreateProject(newProject);

            }
            catch(Exception e)
            {
                ScenarioContext.Current.Add(ExceptionKey, e);
            }
        }
        
        [When(@"I post to assign user (.*) to project (.*)")]
        public void WhenIPostToAssignUserToExistingProject(int p0, int p1)
        {
            try
            {
                var controller = ScenarioContext.Current.Get<ProjectController>(ControllerKey);
                controller.AssignToUser(p0, p1);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(ExceptionKey, e);
            }
        }
        
        [Then(@"A new project is added to the project list")]
        public void ThenANewProjectIsAddedToTheProjectList()
        {
            var newProject = ScenarioContext.Current.Get<Project>(NewProjectKey);
            Assert.IsTrue(DataManager.Projects.Contains(newProject));
        }
        
        [Then(@"An exception is thrown")]
        public void ThenAnExceptionIsThrown()
        {
            var exception = ScenarioContext.Current.Get<Exception>(ExceptionKey);
            Assert.IsNotNull(exception);
        }
        
        [Then(@"The user (.*) gets assigned to the project (.*)")]
        public void ThenTheUserGetsAssignedToTheProject(int p0, int p1)
        {
            var user = DataManager.Users.FirstOrDefault(c => c.Id == p0);
            Assert.IsNotNull(user);
            Assert.IsTrue(user.Projects.Any(c => c.Id == p1));
        }

        [Given(@"I have a registered user")]
        public void GivenIHaveARegisteredUser()
        {
            var regUser = new User()
            {
                Id = 1,
                Email = "test@gmail.com",
                Password = "0000"
            };

            ScenarioContext.Current.Add(RegisteredUserKey, regUser);
        }

        [Given(@"The registered user has projects assigned to him")]
        public void GivenTheRegisteredUserHasProjectsAssignedToHim()
        {
            var regUser = ScenarioContext.Current.Get<User>(RegisteredUserKey);
            regUser.Projects = new System.Collections.Generic.List<Project>();
            regUser.Projects.Add(new Project()
            {
                Id = 0,
                Name = "T1"
            });
            regUser.Projects.Add(new Project()
            {
                Id = 1,
                Name = "T2"
            });
        }

        [Given(@"The registered user is in the data storage")]
        public void GivenTheRegisteredUserIsInTheDataStorage()
        {
            var regUser = ScenarioContext.Current.Get<User>(RegisteredUserKey);
            DataManager.Users.Add(regUser);
        }

        [Given(@"The registered user has supplied a token")]
        public void GivenTheRegisteredUserHasSuppliedAToken()
        {
            var regUser = ScenarioContext.Current.Get<User>(RegisteredUserKey);
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(regUser.Email), null);
        }

        [Given(@"The registered user has not supplied a token")]
        public void GivenTheRegisteredUserHasNotSuppliedAToken()
        {
            Thread.CurrentPrincipal = null;
        }

        [When(@"I try to get that users data")]
        public void WhenITryToGetThatUsersData()
        {
            var controller = ScenarioContext.Current.Get<ProjectController>(ControllerKey);
            var projects = controller.GetMyProjects();
            ScenarioContext.Current.Add(ProjectListKey, projects);
        }

        [Then(@"I receive a list of projects assigned to that user")]
        public void ThenIReceiveAListOfProjectsAssignedToThatUser()
        {
            var projects = ScenarioContext.Current.Get<List<Project>>(ProjectListKey);
            Assert.IsNotNull(projects);
            Assert.IsTrue(projects.Any());
            Assert.IsTrue(projects.Count() > 0);
        }

        [Then(@"I receive a null list of projects assigned to that user")]
        public void ThenIReceiveAnEmptyListOfProjectsAssignedToThatUser()
        {
            var projects = ScenarioContext.Current.Get<List<Project>>(ProjectListKey);
            Assert.IsNull(projects);
        }
        
        [Given(@"I have projects registered in the database")]
        public void GivenIHaveProjectsRegisteredInTheDatabase()
        {
            for (var i = 0; i < RegisteredProjectCount; i++)
            {
                DataManager.Projects.Add(
                    new Project() {
                        Id = i,
                        Name = $"P{i}"
                    });
            }
        }
        
        [When(@"I try to get all projects")]
        public void WhenITryToGetAllProjects()
        {
            var controller = ScenarioContext.Current.Get<ProjectController>(ControllerKey);
            var projects = controller.GetAll();
            ScenarioContext.Current.Add(AllProjectListKey, projects);
        }

        [Then(@"I get all existing projects")]
        public void ThenIGetAllExistingProjects()
        {
            var projects = ScenarioContext.Current.Get<List<Project>>(AllProjectListKey);
            Assert.IsNotNull(projects);
            Assert.IsTrue(projects.Any());
            Assert.AreEqual(RegisteredProjectCount, projects.Count);
        }

        [Then(@"I get no projects")]
        public void ThenIGetNoProjects()
        {
            var projects = ScenarioContext.Current.Get<List<Project>>(AllProjectListKey);
            Assert.IsNull(projects);
        }


        public static void Init()
        {
            DataManager.Initialize();
            var controller = new ProjectController();
            ScenarioContext.Current.Add(ControllerKey, controller);
        }
    }
}
