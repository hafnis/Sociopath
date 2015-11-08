using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sociopath.DataEntities.Entities;
using Sociopath.ServiceContracts;

namespace Sociopath.Services
{
    public class FacebookService : IFacebookService
    {
        private const string UserId = "1266148806744180";

        public IList<Feed> GetFeed()
        {
            var client = new FacebookClient();
            client.AccessToken = "CAAMnJdz4v6YBAEye8Pam1C5SVuwetZCtdzov7JdvOtzgw3MhTCJAABkMH8SFNGPP7BB7ZBrJmJJNUAZB7yt4NjbvZBYd4xbyQzHzMQLAbLYKsmSA7TuVra63AeyRgdYH0GvFwBGat8yYgwmoPRHVDy4YJtuZCR6FIxZCOhfpI2N1iIJpiOCxGBy8X0G1DouJrVSOT5DC4wbSUzrnIe0ui9uTeqK3ir1mQOCJvMydCK6au1PNSjE2BouHm9ExHxYYsZD";
            client.AppId = "887468504694694";
            client.AppSecret = "86aad88a61e307654655537ab6c8ed41";

            var request = string.Format("/{0}/feed/", UserId);

            var response = client.Get(request);

            return new List<Feed>();
        }
    }
}
