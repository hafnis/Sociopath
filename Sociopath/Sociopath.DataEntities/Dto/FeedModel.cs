using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Sociopath.DataEntities.Dto
{
    public class FeedModel
    {
        [Required]
        [StringLength(140)]
        public string Message { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
