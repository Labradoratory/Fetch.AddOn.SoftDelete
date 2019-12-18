﻿using Labradoratory.Fetch.Processors;
using Labradoratory.Fetch.Processors.DataPackages;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Processors
{
    /// <summary>
    /// Processes an <typeparamref name="TEntity"/> that was soft deleted.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="IProcessor{TEntity}" />
    public abstract class EntitySoftDeletedProcessor<TEntity> : BaseEntityProcessor<TEntity, EntitySoftDeletedPackage<TEntity>>
        where TEntity : Entity, ISoftDeletable
    {
    }
}