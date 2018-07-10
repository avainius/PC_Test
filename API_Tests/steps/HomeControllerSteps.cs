using API.Controllers;
using DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace DataAccess.tests.steps
{
    [Binding]
    public class HomeControllerSteps
    {
        public static string HomeControllerKey = "HomeController";
        public static string NewUserKey = "NewUser";
        public static string GeneratedTokenKey = "ActualToken";
        public static string ExceptionKey = "Exception";

        public static string ExistingEmail = "test@gmail.com";
        public static string ExistingPassword = "0000";
        public static string ExpectedToken = "dGVzdEBnbWFpbC5jb206MDAwMA==";

        public HomeControllerSteps() { Init(); }

        [Given(@"I have a new user with an email:(.*) and password:(.*) ready for registration")]
        public void GivenIHaveANewUserWithAnEmailAndPasswordReadyForRegistration(string email, string password)
        {
            var newUser = new User()
            {
                Email = email,
                Password = password
            };
            ScenarioContext.Current.Add(NewUserKey, newUser);

        }
        
        [Given(@"I have an existing user")]
        public void GivenIHaveAnExistingUser()
        {
            var existingUser = new User() {
                Email = ExistingEmail,
                Password = ExistingPassword
            };

            DataManager.Users.Add(existingUser);
        }
        
        [When(@"I post a registration request for the new user")]
        public void WhenIPostTheUser()
        {
            try
            {
                var controller = ScenarioContext.Current.Get<HomeController>(HomeControllerKey);
                var newUser = ScenarioContext.Current.Get<User>(NewUserKey);
                controller.Register(newUser);
            }
            catch (Exception e)
            {
                ScenarioContext.Current.Add(ExceptionKey, e);
            }
        }
        
        [When(@"I login")]
        public void WhenILogin()
        {
            var controller = ScenarioContext.Current.Get<HomeController>(HomeControllerKey);

            var generatedToken = controller.Login(new User()
            {
                Email = ExistingEmail,
                Password = ExistingPassword
            });

            ScenarioContext.Current.Add(GeneratedTokenKey, generatedToken);
        }
        
        [Then(@"A new user is created")]
        public void ThenANewUserIsCreated()
        {
            var newUser = ScenarioContext.Current.Get<User>(NewUserKey);
            Assert.IsTrue(DataManager.Users.Any(c => c.Email == newUser.Email && c.Password == newUser.Password));
        }

        [Then(@"An exception was thrown")]
        public void ThenAnExceptionWasThrown()
        {
            ScenarioContext.Current.TryGetValue<Exception>(ExceptionKey, out var exception);
            Assert.IsNotNull(exception);
        }

        [Then(@"No Exception was thrown")]
        public void ThenNoExceptionWasThrown()
        {
            ScenarioContext.Current.TryGetValue<Exception>(ExceptionKey, out var exception);
            Assert.IsNull(exception);
        }

        [Then(@"User is not registerd")]
        public void ThenUserIsNotRegistered()
        {
            var newUser = ScenarioContext.Current.Get<User>(NewUserKey);
            Assert.IsFalse(DataManager.Users.Any(c => c.Email == newUser.Email && c.Password == newUser.Password));
        }

        [Then(@"I get an authentication token")]
        public void ThenIGetAnAuthenticationToken()
        {
            var actualToken = ScenarioContext.Current.Get<string>(GeneratedTokenKey);
            Assert.IsNotNull(actualToken);
        }
        
        [Then(@"The token is valid")]
        public void ThenTheTokenIsValid()
        {
            var actualToken = ScenarioContext.Current.Get<string>(GeneratedTokenKey);
            Assert.AreEqual(ExpectedToken, actualToken);
        }

        public static void Init()
        {
            var controller = new HomeController();
            ScenarioContext.Current.Add(HomeControllerKey, controller);
            DataManager.Users = new System.Collections.Generic.List<User>();
        }
    }
}
