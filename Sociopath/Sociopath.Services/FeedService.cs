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
            var facebookResult = facebookService.PostFeed(request);

            var twitterResult = twitterService.PostFeed(request);

            facebookResult.TwitterExternalId = twitterResult.TwitterExternalId;

            repository.Save(facebookResult);
            repository.Commit();

            return facebookResult;
        }
    }
}
