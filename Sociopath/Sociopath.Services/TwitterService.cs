using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sociopath.DataContracts;
using Sociopath.DataEntities.Dto;
using Sociopath.DataEntities.Entities;
using Sociopath.ServiceContracts;
using TweetSharp;

namespace Sociopath.Services
{
    public class TwitterService : Sociopath.ServiceContracts.ITwitterService
    {
        private const string ConsumerKey = "t4yOeOVPThO1XFAx7pKRl9MIz";
        private const string ConsumerSecret = "9kOT49Vt8wzBnjr0a2Mj9NTbToGi2hfKktMYIHdrICoJcNVULx";
        private IRepository repository;

        public TwitterService(IRepository repository)
        {
            this.repository = repository;
        }
        

        public TwitterUser GetUser(LoginModel model)
        {
            var twitterService = new TweetSharp.TwitterService(ConsumerKey, ConsumerSecret);
            twitterService.AuthenticateWith(model.Token, model.Secret);
            TwitterUser user = twitterService.VerifyCredentials(new VerifyCredentialsOptions() { IncludeEntities = false, SkipStatus = false });
            return user;
        }

        public IList<Feed> GetFeed(FeedModel request)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                return null;
            }

            var twitterService = new TweetSharp.TwitterService(ConsumerKey, ConsumerSecret);
            twitterService.AuthenticateWith(user.TwitterToken, user.TwitterSecret);
            List<TwitterStatus> tweets = twitterService.ListTweetsOnHomeTimeline(new ListTweetsOnHomeTimelineOptions()).ToList();

            var result = new List<Feed>();

            foreach (TwitterStatus tweet in tweets)
            {
                Feed feedItem = repository.AsQueryable<Feed>().FirstOrDefault(x => x.TwitterExternalId == tweet.Id.ToString()) ?? new Feed();
                feedItem.TwitterExternalId = tweet.Id.ToString();
                feedItem.Message = tweet.Text;
                feedItem.Time = tweet.CreatedDate;
                repository.Save(feedItem);
                result.Add(feedItem);
            }

            repository.Commit();
            return result;
        }

        public Feed PostFeed(FeedModel model)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == model.UserId);
            if (user == null)
            {
                return null;
            }

            var twitterService = new TweetSharp.TwitterService(ConsumerKey, ConsumerSecret);
            twitterService.AuthenticateWith(user.TwitterToken, user.TwitterSecret);
            var tweet = twitterService.SendTweet(new SendTweetOptions { Status = model.Message });
            var feed = new Feed { TwitterExternalId = tweet.Id.ToString(), Time = tweet.CreatedDate, Message = tweet.Text };
            return feed;
        }
    }
}
