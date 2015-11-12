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

        public UserModel GetUser(LoginModel model)
        {
            User user = null;
            if (model.provider == Provider.Twitter)
            {
                user = repository.AsQueryable<User>().FirstOrDefault(x => x.TwitterSecret == model.Secret && x.TwitterToken == model.Token) ?? new User();
                user.TwitterToken = model.Token;
                user.TwitterSecret = model.Secret;
                user.TwitterEnabled = true;
                repository.Save(user);
            }
            else if (model.provider == Provider.Facebook)
            {
                user = repository.AsQueryable<User>().FirstOrDefault(x => x.FacebookId == model.ExternalId) ?? new User();
                user.FacebookId = model.ExternalId;
                user.FacebookToken = model.Token;
                user.FacebookEnabled = true;
                repository.Save(user);
            }

            repository.Commit();

            var response = new UserModel()
            {
                UserId = user.Id,
                IsFacebookEnabled = user.FacebookEnabled,
                IsTwitterEnabled = user.TwitterEnabled
            };

            return response;
        }

        public UserModel UpdateUser(UserModel model)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == model.UserId);
            if (user != null)
            {
                user.TwitterEnabled = model.IsTwitterEnabled;
                user.FacebookEnabled = model.IsFacebookEnabled;
                repository.Save(user);
                repository.Commit();

                model.IsTwitterEnabled = user.TwitterEnabled;
                model.IsFacebookEnabled = user.FacebookEnabled;
                model.UserId = user.Id;
                return model;
            }
            return null;
        }

        public UserModel GetUser(int id)
        {
            var user = repository.AsQueryable<User>().FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                return new UserModel { UserId = user.Id, IsFacebookEnabled = user.FacebookEnabled, IsTwitterEnabled = user.TwitterEnabled };
            }
            return null;
        }

        public IList<UserModel> GetUsers()
        {
            var users = repository.AsQueryable<User>().Select(x => new UserModel
            {
                UserId = x.Id,
                IsFacebookEnabled = x.FacebookEnabled,
                IsTwitterEnabled = x.TwitterEnabled
            }).ToList();

            return users;
        }
    }
}
