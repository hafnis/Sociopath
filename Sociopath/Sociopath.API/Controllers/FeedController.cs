using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sociopath.DataContracts;
using Sociopath.DataEntities.Dto;
using Sociopath.ServiceContracts;

namespace Sociopath.API.Controllers
{
    public class FeedController : ApiController
    {
        private IFeedService feedService;
        public FeedController(IFeedService feedService)
        {
            this.feedService = feedService;
        }

        public HttpResponseMessage Get()
        {
            var messages = feedService.GetFeed(new FeedModel() { UserId = 5});
            var response = Request.CreateResponse(HttpStatusCode.OK, messages);
            return response;
        }

        public HttpResponseMessage Post(FeedModel request)
        {
            var feedItem = feedService.PostFeed(request);
            var response = Request.CreateResponse(HttpStatusCode.Created, feedItem);
            return response;
        }
    }
}
