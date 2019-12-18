using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Labradoratory.Fetch.ChangeTracking;
using Labradoratory.Fetch.Processors;
using Labradoratory.Fetch.Processors.DataPackages;
using Microsoft.AspNetCore.Mvc;

namespace Labradoratory.Fetch.AddOn.SoftDelete
{
    /// <summary>
    /// The base implementation of an repository used to access data that also support soft deletion.  
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the accessor supports.</typeparam>
    /// <seealso cref="Repository{TEntity}" />
    /// <seealso cref="ProcessorPipeline"/>
    public abstract class RepositoryWithSoftDelete<TEntity> : Repository<TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        protected RepositoryWithSoftDelete(ProcessorPipeline processorPipeline)
            : base(processorPipeline)
        {
        }

        public virtual async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var softDeletingPackage = new EntitySoftDeletingPackage<TEntity>(entity);
            await ProcessorPipeline.ProcessAsync(softDeletingPackage, cancellationToken);

            entity.IsDeleted = true;
            var changes = entity.CommitChanges();
            await ExecuteUpdateAsync(entity, changes, cancellationToken);

            var softDeletedPackage = new EntitySoftDeletedPackage<TEntity>(entity);
            await ProcessorPipeline.ProcessAsync(softDeletedPackage, cancellationToken);
        }

        public virtual async Task RestoreSoftDeletedAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var restoringPackage = new EntityRestoringPackage<TEntity>(entity);
            await ProcessorPipeline.ProcessAsync(restoringPackage, cancellationToken);

            entity.IsDeleted = false;
            var changes = entity.CommitChanges();
            await ExecuteUpdateAsync(entity, changes, cancellationToken);

            var restoredPackage = new EntityRestoredPackage<TEntity>(entity);
            await ProcessorPipeline.ProcessAsync(restoredPackage, cancellationToken);
        }
    }
}
