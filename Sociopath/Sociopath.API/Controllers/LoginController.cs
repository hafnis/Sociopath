using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sociopath.DataEntities.Dto;
using Sociopath.ServiceContracts;

namespace Sociopath.API.Controllers
{
    public class LoginController : ApiController
    {
        private IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }

        public HttpResponseMessage Post(LoginModel request)
        {
            var user = userService.GetUser(request);
            var response = Request.CreateResponse(HttpStatusCode.OK, user);
            return response;
        }
    }
}
