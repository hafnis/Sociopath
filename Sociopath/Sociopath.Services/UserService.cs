using DevBridge.Templates.WebProject.DataEntities.Enums;
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
    public class UserService : IUserService
    {
        private IRepository repository;
        private Sociopath.ServiceContracts.ITwitterService twitterService;

        public UserService(IRepository repository, Sociopath.ServiceContracts.ITwitterService twitterService)
        {
            this.repository = repository;
            this.twitterService = twitterService;
        }

        public User GetUser(LoginModel model)
        {
            User user = null;
            if (model.provider == Provider.Twitter)
            {
                user = repository.AsQueryable<User>().FirstOrDefault(x => x.TwitterSecret == model.Secret && x.TwitterToken == model.Token) ?? new User();
                user.TwitterToken = model.Token;
                user.TwitterSecret = model.Secret;
                repository.Save(user);
            }
            else if (model.provider == Provider.Facebook)
            {
                user = repository.AsQueryable<User>().FirstOrDefault(x => x.FacebookId == model.ExternalId) ?? new User();
                user.FacebookId = model.ExternalId;
                user.FacebookToken = model.Token;
                repository.Save(user);
            }

            repository.Commit();
            
            return user;
        }
    }
}
