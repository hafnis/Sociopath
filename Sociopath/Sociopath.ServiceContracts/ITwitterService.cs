using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sociopath.DataEntities.Dto;
using Sociopath.DataEntities.Entities;
using TweetSharp;

namespace Sociopath.ServiceContracts
{
    public interface ITwitterService
    {
        IList<Feed> GetFeed(FeedModel request);
        TwitterUser GetUser(LoginModel user);
        Feed PostFeed(FeedModel model);
    }
}
