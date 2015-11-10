using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sociopath.DataEntities.Entities;

namespace Sociopath.DataEntities.Mappings
{
    public class UserMap : EntityMapBase<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.FacebookToken).Nullable();
            Map(f => f.TwitterToken).Nullable();
            Map(f => f.TwitterSecret).Nullable();
            Map(f => f.TwitterId).Nullable();
            Map(f => f.FacebookId).Nullable();
        }
    }
}
