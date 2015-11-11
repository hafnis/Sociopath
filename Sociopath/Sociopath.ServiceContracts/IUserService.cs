using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sociopath.DataEntities.Dto;
using Sociopath.DataEntities.Entities;

namespace Sociopath.ServiceContracts
{
    public interface IUserService
    {
        UserModel GetUser(LoginModel model);
        UserModel GetUser(int id);
        UserModel UpdateUser(UserModel model);
    }
}
