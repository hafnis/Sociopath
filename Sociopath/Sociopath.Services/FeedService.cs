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


        public FeedService(IFacebookService facebookService, IRepository repository)
        {
            this.facebookService = facebookService;
            this.repository = repository;
        }

        public IList<Feed> GetFeed()
        {
            var result = facebookService.GetFeed();
            return result;
        }


        public Feed PostFeed(FeedModel request)
        {
            var result = facebookService.PostFeed(request);

            Feed feedItem = null;

            if (!string.IsNullOrEmpty(result))
            {
                feedItem = new Feed() { FacebookExternalId = result, Message = request.Message, Time = DateTime.Now};
                repository.Save(feedItem);
            }

            repository.Commit();

            return feedItem;
        }
    }
}
