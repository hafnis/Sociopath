using LinqToTwitter;
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
        private const string TwitterToken = "2284226888-axrNhsFXIXHvuGelfZlnF1sO2XwzufwcRyNI3gJ";
        private const string TwitterSecret = "myHqir0I33H4BjmxeeDmvfqlxtOrPoU64urFvonrbVKTs";
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
            var user = repository.AsQueryable<Sociopath.DataEntities.Entities.User>().FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                return null;
            }

            var auth = new SingleUserAuthorizer
            {                 
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    OAuthToken = user.TwitterToken,
                    OAuthTokenSecret = user.TwitterSecret
                }
            };
            var twitterCtx = new TwitterContext(auth);

            var tweets = twitterCtx.Status.Where(tweet => tweet.Type == StatusType.User).ToList();
            var result = new List<Feed>();

            foreach (Status tweet in tweets)
            {
                Feed feedItem = repository.AsQueryable<Feed>().FirstOrDefault(x => x.TwitterExternalId == tweet.StatusID.ToString()) ?? new Feed();
                feedItem.TwitterExternalId = tweet.StatusID.ToString();
                feedItem.Message = tweet.Text;
                feedItem.Time = tweet.CreatedAt;
                repository.Save(feedItem);
                result.Add(feedItem);
            }

            repository.Commit();
            return result;
        }

        public Feed PostFeed(FeedModel model)
        {
            var user = repository.AsQueryable<Sociopath.DataEntities.Entities.User>().FirstOrDefault(x => x.Id == model.UserId);
            if (user == null)
            {
                return null;
            }

            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    OAuthToken = user.TwitterToken,
                    OAuthTokenSecret = user.TwitterSecret
                }
            };
            var twitterCtx = new TwitterContext(auth);

            //var twitterService = new TweetSharp.TwitterService(ConsumerKey, ConsumerSecret);
            //twitterService.AuthenticateWith(user.TwitterToken, user.TwitterSecret);
            //var tweet = twitterService.SendTweet(new SendTweetOptions { Status = model.Message });
            var tweet = twitterCtx.TweetAsync(model.Message).Result;
            var feed = new Feed { TwitterExternalId = tweet.StatusID.ToString(), Time = tweet.CreatedAt, Message = tweet.Text };
            return feed;
        }
    }
}
