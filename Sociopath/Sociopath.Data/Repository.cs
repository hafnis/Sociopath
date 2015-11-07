using Sociopath.DataContracts;

using NHibernate;

namespace Sociopath.Data
{
    public class Repository: GenericRepositoryBase<int>, IRepository
    {
        public Repository(ISessionFactoryProvider sessionFactoryProvider)
            : base(sessionFactoryProvider)
        {
        }

        public Repository(ISession session)
            : base(session)
        {
        }
    }  
}
