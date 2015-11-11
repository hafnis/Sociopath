using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sociopath.DataEntities.Dto
{
    public class UserModel
    {
        public int UserId { get; set; }
        public bool IsFacebookEnabled { get; set; }
        public bool IsTwitterEnabled { get; set; }
    }
}
