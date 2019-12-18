using System;
using Labradoratory.Fetch.Processors;
using Labradoratory.Fetch.Processors.DataPackages;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Processors
{
    /// <summary>
    /// Processes an <typeparamref name="TEntity"/> that is being soft deleted.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IProcessor{TEntity}" />
    public abstract class EntitySoftDeletingProcessor<TEntity> : BaseEntityProcessor<TEntity, EntitySoftDeletingPackage<TEntity>>
        where TEntity : Entity, ISoftDeletable
    {
    }
}
