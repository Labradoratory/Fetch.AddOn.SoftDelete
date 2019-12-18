using Labradoratory.Fetch.AddOn.SoftDelete;

namespace Labradoratory.Fetch.Processors.DataPackages
{
    /// <summary>
    /// The data related a <typeparamref name="TEntity"/> that was restored from soft delete.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="DataPackage" />
    public class EntityRestoredPackage<TEntity> : BaseEntityDataPackage<TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRestoredPackage{TEntity}"/> class.
        /// </summary>
        /// <param name="entity">The entity that was restored.</param>
        public EntityRestoredPackage(TEntity entity)
            : base(entity)
        { }
    }
}
