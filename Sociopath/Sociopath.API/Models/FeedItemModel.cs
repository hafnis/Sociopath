using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sociopath.API.Models
{
    public class FeedItemModel
    {
        public string Message { get; set; }
        public string Time { get; set; }
        public bool IsPostedToFacebook { get; set; }
        public bool IsPostedToTwitter { get; set; }
    }
}