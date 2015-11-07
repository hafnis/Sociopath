using Sociopath.Data.DataContext.Conventions;
using Sociopath.DataContracts;
using Sociopath.DataEntities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Event;

namespace Sociopath.Data.DataContext
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly static object LockObject = new object();

        private volatile ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    lock (LockObject)
                    {
                        if (sessionFactory == null)
                        {
                            sessionFactory = CreateSessionFactory();
                        }
                    }
                }

                return sessionFactory;
            }
        }

        public ISession Open()
        {
            return SessionFactory.OpenSession();
        }

        private ISessionFactory CreateSessionFactory()
        {                  
            return
                Fluently.Configure()
                        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<IEntity>()
                                        .Conventions.Add(ForeignKey.EndsWith("Id"))
                                        .Conventions.Add<EnumConvention>())

                        .BuildConfiguration()
                        .BuildSessionFactory();
        }
    }
}