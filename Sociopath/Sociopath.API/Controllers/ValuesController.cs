using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sociopath.DataContracts;
using Sociopath.DataEntities.Entities;
using Sociopath.ServiceContracts;

namespace Sociopath.API.Controllers
{
    public class ValuesController : ApiController
    {
        private IRepository repository;
        private IFacebookService facebookService;

        public ValuesController(IRepository repository, IFacebookService facebookService)
        {
            this.repository = repository;
            this.facebookService = facebookService;
        }

        // GET api/values
        public IList<Feed> Get()
        {
            var result = facebookService.GetFeed();

            var user2 = repository.AsQueryable<User>().ToList();

            return result;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}