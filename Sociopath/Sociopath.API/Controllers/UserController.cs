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
        public void Get()
        {

        }

    }
}
