using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.Web.Http.Filters;
using NLog;
using System.Web.Http.Controllers;

namespace Sociopath.API.Extensions
{
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateResponse( HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}