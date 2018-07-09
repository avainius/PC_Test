using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using DataAccess.Services;
using System.Threading;
using System.Security.Principal;
using DataAccess.Models;

namespace API
{
    public class AdminAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private DataAccess.Interfaces.IServices.IUserService userService = new UserService();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                var loginDataArray = decodedAuthToken.Split(':');
                var username = loginDataArray[0];
                var password = loginDataArray[1];
                var tempUser = new User() { Email = username, Password = password };

                if (userService.Login(tempUser) && userService.IsAdmin(tempUser))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}