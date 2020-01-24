using Labradoratory.Fetch.Processors.DataPackages;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Processors.DataPackages
{
    /// <summary>
    /// The data related a <typeparamref name="TEntity"/> that was soft deleted.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="DataPackage" />
    public class EntitySoftDeletedPackage<TEntity> : BaseEntityDataPackage<TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySoftDeletedPackage{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">The entity was soft deleted.</param>
        public EntitySoftDeletedPackage(TEntity entity)
            : base(entity)
        { }
    }
}
