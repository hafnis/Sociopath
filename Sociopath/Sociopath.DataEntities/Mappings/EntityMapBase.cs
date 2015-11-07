using FluentNHibernate.Mapping;

namespace Sociopath.DataEntities.Mappings
{
    public abstract class EntityMapBase<TEntity> : ClassMap<TEntity>
        where TEntity : IEntity
    {
    }
}
