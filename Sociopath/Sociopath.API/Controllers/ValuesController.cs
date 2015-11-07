using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sociopath.DataContracts;
using Sociopath.DataEntities.Entities;

namespace Sociopath.API.Controllers
{
    public class ValuesController : ApiController
    {
        private IRepository repository;

        public ValuesController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET api/values
        public List<User> Get()
        {
            var user = new User();
            user.FacebookToken = "labas as krabas";

            repository.Save(user);
            repository.Commit();

            var user2 = repository.AsQueryable<User>().ToList();

            return user2;
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