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
    public class FeedService : IFeedService
    {
        private IFacebookService facebookService;
        private IRepository repository;
        private Sociopath.ServiceContracts.ITwitterService twitterService;


        public FeedService(IFacebookService facebookService, IRepository repository, Sociopath.ServiceContracts.ITwitterService twitterService)
        {
            this.facebookService = facebookService;
            this.repository = repository;
            this.twitterService = twitterService;
        }

        public IList<Feed> GetFeed(FeedModel request)
        {
            var result = facebookService.GetFeed(request);
            var tweets = twitterService.GetFeed(request);

            foreach (Feed tweet in tweets)
            {
                if (!result.Where(x => x.Message == tweet.Message && x.TwitterExternalId == tweet.TwitterExternalId).Any())
                {
                    result.Add(tweet);
                }
            }

            return result;
        }


        public Feed PostFeed(FeedModel request)
        {
            Feed result = null;

            var user = repository.AsQueryable<User>(x => x.Id == request.UserId).FirstOrDefault();

            if (user.FacebookEnabled)
            {
                result = facebookService.PostFeed(request);
            }

            if (user.TwitterEnabled)
            {
                var twitterResult = twitterService.PostFeed(request);
                if (result != null)
                {
                    result.TwitterExternalId = twitterResult.TwitterExternalId;
                }
                else
                {
                    result = twitterResult;
                }
            }

            if (result != null)
            {
                repository.Save(result);
                repository.Commit();
            }

            return result;
        }
    }
}
