using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sociopath.DataEntities.Entities;

namespace Sociopath.ServiceContracts
{
    public interface IFacebookService
    {
        IList<Feed> GetFeed();
    }
}
