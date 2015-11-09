using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sociopath.DataEntities.Entities
{
    public class Feed : EntityBase<Feed>
    {
        public virtual string Message { get; set; }
        public virtual DateTime Time { get; set; }
        public virtual string FacebookExternalId { get; set; }
        public virtual string TwitterExternalId { get; set; }
    }
}
