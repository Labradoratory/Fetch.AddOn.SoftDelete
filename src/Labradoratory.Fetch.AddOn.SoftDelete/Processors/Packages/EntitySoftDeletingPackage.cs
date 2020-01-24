using Labradoratory.Fetch.Processors.DataPackages;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Processors.DataPackages
{
    /// <summary>
    /// The data related a <typeparamref name="TEntity"/> that is being soft deleted.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="DataPackage" />
    public class EntitySoftDeletingPackage<TEntity> : BaseEntityDataPackage<TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySoftDeletingPackage{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">The entity being soft deleted.</param>
        public EntitySoftDeletingPackage(TEntity entity)
            : base(entity)
        { }
    }
}
