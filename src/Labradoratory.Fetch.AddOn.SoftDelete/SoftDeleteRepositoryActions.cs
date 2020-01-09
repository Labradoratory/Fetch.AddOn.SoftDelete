using System;
using System.Threading;
using System.Threading.Tasks;
using Labradoratory.Fetch.ChangeTracking;
using Labradoratory.Fetch.Processors;
using Labradoratory.Fetch.Processors.DataPackages;

namespace Labradoratory.Fetch.AddOn.SoftDelete
{
    /// <summary>
    /// Defines the soft delete related <see cref="Repository{TEntity}"/> members.
    /// This class can be used to quickly implement the soft delete actions.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class SoftDeleteRepositoryActions<TEntity> : ISoftDeletableRepositoryExtentions<TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteRepositoryActions{TEntity}"/> class.
        /// </summary>
        /// <param name="processorPipeline">The processor pipeline.</param>
        /// <param name="executeUpdateAsync">The execute update function.  This is called to commit changes.</param>
        public SoftDeleteRepositoryActions(
            ProcessorPipeline processorPipeline,
            Func<TEntity, ChangeSet, CancellationToken, Task> executeUpdateAsync)
        {
            ProcessorPipeline = processorPipeline;
            ExecuteUpdateAsync = executeUpdateAsync;
        }

        /// <summary>
        /// Gets the processor pipeline.
        /// </summary>
        protected ProcessorPipeline ProcessorPipeline { get; }

        /// <summary>
        /// Gets the execute update action.
        /// </summary>
        protected Func<TEntity, ChangeSet, CancellationToken, Task> ExecuteUpdateAsync { get; }

        public virtual async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var softDeletingPackage = new EntitySoftDeletingPackage<TEntity>(entity);
            await ProcessorPipeline.ProcessAsync(softDeletingPackage, cancellationToken);

            entity.IsDeleted = true;
            // NOTE: We don't rely on the IsDeleted property participating in change tracking.
            // In fact, it is recommended that it doesn't so that it can't be change via an direct Update.
            var changes = new ChangeSet
            {
                { "IsDeleted", new ChangeValue { Action = ChangeAction.Update, Target = ChangeTarget.Object, OldValue = false, NewValue = true } }
            };
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
