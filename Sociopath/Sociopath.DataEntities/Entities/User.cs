using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sociopath.DataEntities.Entities
{
    public class User : EntityBase<User>
    {
        public virtual string FacebookToken { get; set; }
        public virtual string TwitterToken { get; set; }
        public virtual string FacebookId { get; set; }
        public virtual string TwitterId { get; set; }
    }
}
