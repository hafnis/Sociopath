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
    public class UserController : ApiController
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public HttpResponseMessage Get(int id)
        {
            var user = userService.GetUser(id);
            HttpResponseMessage response;
            if (user == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return response;
        }

        public HttpResponseMessage Post(LoginModel request)
        {
            var user = userService.GetUser(request);
            var response = Request.CreateResponse(HttpStatusCode.Created, user);
            return response;
        }

        public HttpResponseMessage Put(UserModel request)
        {
            var user = userService.UpdateUser(request);
            HttpResponseMessage response;
            if (user == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound, "User not found");
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.OK, request);
            }

            return response;
        }

    }
}
