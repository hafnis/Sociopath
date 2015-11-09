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
        private const string UserId = "1266148806744180";
        private IRepository repository;

        public FacebookService(IRepository repository)
        {
            this.repository = repository;
        }


        public IList<Feed> GetFeed()
        {
            var client = new FacebookClient();
            client.AccessToken = "CAAMnJdz4v6YBAEye8Pam1C5SVuwetZCtdzov7JdvOtzgw3MhTCJAABkMH8SFNGPP7BB7ZBrJmJJNUAZB7yt4NjbvZBYd4xbyQzHzMQLAbLYKsmSA7TuVra63AeyRgdYH0GvFwBGat8yYgwmoPRHVDy4YJtuZCR6FIxZCOhfpI2N1iIJpiOCxGBy8X0G1DouJrVSOT5DC4wbSUzrnIe0ui9uTeqK3ir1mQOCJvMydCK6au1PNSjE2BouHm9ExHxYYsZD";
            client.AppId = "887468504694694";
            client.AppSecret = "86aad88a61e307654655537ab6c8ed41";

            var request = string.Format("/{0}/feed/", UserId);

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


        public string PostFeed(FeedModel request)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                return null;
            }

            var client = new FacebookClient();
            client.AccessToken = "CAAMnJdz4v6YBAFNbjD7ZCwhBrDlFmWfARYwKHLZAHM49ZBizJprLKrFmxIXKOXUnVZB87pqIbryfAZCGhrZCKocKCeKpGgU9oKLl6WXZBSZBL2h36lczEREUyJdgCJl2DVB3KXQoQzmmf58XKK4Vz74JR4ayMF3ZCVBBJRMnb7jNxBoGZApI8k0JleoF0QgZB8D8ZBnpZAoYVMcZBR8NZCinVQIfUTlN9RU9kGxZBQZCcJ5Kz5fS1yGSTrOZAZBZBYv5a2T5NJRPuZBwZD";
            
            var response = client.Post("me/feed", new { message = request.Message});
            return response.ToString();
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
