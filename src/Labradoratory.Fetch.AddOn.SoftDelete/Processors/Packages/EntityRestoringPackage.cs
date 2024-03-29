﻿using Labradoratory.Fetch.Processors.DataPackages;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Processors.DataPackages
{
    /// <summary>
    /// The data related a <typeparamref name="TEntity"/> that being restored from soft delete.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="DataPackage" />
    public class EntityRestoringPackage<TEntity> : EntityAddingPackage<TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRestoringPackage{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">The entity being restored.</param>
        public EntityRestoringPackage(TEntity entity)
            : base(entity)
        { }
    }
}
