using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sociopath.DataEntities.Entities;

namespace Sociopath.DataEntities.Mappings
{
    public class FeedMap : EntityMapBase<Feed>
    {
        public FeedMap()
        {
            Table("Feed");

            Id(f => f.Id).GeneratedBy.Identity();

            Map(f => f.Message).Not.Nullable();
        }
    }
}
