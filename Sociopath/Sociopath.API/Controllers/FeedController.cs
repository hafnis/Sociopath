using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sociopath.API.Models;
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

        public HttpResponseMessage Get(int userId)
        {
            var messages = feedService.GetFeed(new FeedModel { UserId = userId }).Select(x => new FeedItemModel
            {
                Message = x.Message,
                Time = x.Time,
                IsPostedToFacebook = string.IsNullOrEmpty(x.FacebookExternalId) ? false : true,
                IsPostedToTwitter = string.IsNullOrEmpty(x.TwitterExternalId) ? false : true
            }).ToList();
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
