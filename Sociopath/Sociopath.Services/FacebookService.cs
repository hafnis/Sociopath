using Facebook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sociopath.DataContracts;
using Sociopath.DataEntities.Dto;
using Sociopath.DataEntities.Entities;
using Sociopath.ServiceContracts;

namespace Sociopath.Services
{
    public class FacebookService : IFacebookService
    {
        private const string AppId = "887468504694694";
        private const string AppSecret = "86aad88a61e307654655537ab6c8ed41";
        private IRepository repository;

        public FacebookService(IRepository repository)
        {
            this.repository = repository;
        }


        public IList<Feed> GetFeed(FeedModel model)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == model.UserId);
            if (user == null)
            {
                return null;
            }

            var client = new FacebookClient();
            client.AccessToken = user.FacebookToken;

            var request = string.Format("me/feed");

            dynamic response = client.Get(request);
            var data = response["data"];
            List<FacebookResponseDto> messages = JsonConvert.DeserializeObject<List<FacebookResponseDto>>(data.ToString());

            var result = new List<Feed>();

            foreach (var message in messages)
            {
                var feedItem = repository.AsQueryable<Feed>().FirstOrDefault(x => x.FacebookExternalId == message.id) ?? new Feed();
                feedItem.FacebookExternalId = message.id;
                feedItem.Message = message.story ?? message.message;
                feedItem.Time = message.created_time;
                repository.Save(feedItem);
                result.Add(feedItem);
            }

            repository.Commit();
            return result;
        }


        public Feed PostFeed(FeedModel request)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                return null;
            }

            var client = new FacebookClient();
            client.AccessToken = user.FacebookToken;
            
            var response = client.Post("me/feed", new { message = request.Message});

            if (!string.IsNullOrEmpty(response.ToString())) {
                var feed = new Feed { FacebookExternalId = response.ToString(), Time = DateTime.Now, Message = request.Message };
                repository.Save(feed);
                repository.Commit();
                return feed;
            }

            return null;
        }
    }

    public class FacebookResponseDto
    {
        public string message { get; set; }
        public string story { get; set; }
        public DateTime created_time { get; set; }
        public string id { get; set; }
    }
}
