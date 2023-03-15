using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApplication1.Middleware
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext filterContext)
        {
            // Implement your custom authorization logic here
            // For example, you could check whether the user is authenticated and has the necessary role or permission to access the requested resource

            string authHeader = filterContext.Request.Headers.Authorization.ToString();
            var userName = WebApplication1.Models.User.ValidateToken(authHeader.Replace("Bearer ", ""));

            if (userName!=null)
            {
                return;
            }

            HandleUnauthorizedRequest(filterContext);
        }
    }
}