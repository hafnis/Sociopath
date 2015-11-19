using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Sociopath.DataEntities.Dto
{
    public class UserModel
    {
        [Required]
        public int UserId { get; set; }
        public bool IsFacebookEnabled { get; set; }
        public bool IsTwitterEnabled { get; set; }
        public bool IsFacebookConnected { get; set; }
        public bool IsTwitterConnected { get; set; }
    }
}
