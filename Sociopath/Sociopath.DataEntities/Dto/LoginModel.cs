using DevBridge.Templates.WebProject.DataEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sociopath.DataEntities.Dto
{
    public class LoginModel
    {
        public Provider provider { get; set; }
        public string Secret { get; set; }
        public string Token { get; set; }
        public string ExternalId { get; set; }        
    }
}
