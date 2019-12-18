using System;
using System.Threading;
using System.Threading.Tasks;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Extensions
{
    public static class RepositoryExtensions
    {
        public static Task SoftDeleteAsync<TEntity>(
            this Repository<TEntity> repository,
            TEntity entity, 
            CancellationToken cancellationToken)
            where TEntity : Entity, ISoftDeletable
        {
            if (repository is RepositoryWithSoftDelete<TEntity> sdr)
                return sdr.SoftDeleteAsync(entity, cancellationToken);

            throw new InvalidOperationException(
                $"Repository for {typeof(ISoftDeletable).Name} entity {typeof(TEntity).Name} is not of type {typeof(Repository<TEntity>).Name}.");
        }

        public static Task RestoreSoftDeletedAsync<TEntity>(
            this Repository<TEntity> repository,
            TEntity entity,
            CancellationToken cancellationToken)
            where TEntity : Entity, ISoftDeletable
        {
            if (!entity.IsDeleted)
                return Task.CompletedTask;

            if (repository is RepositoryWithSoftDelete<TEntity> sdr)
                return sdr.RestoreSoftDeletedAsync(entity, cancellationToken);

            throw new InvalidOperationException(
                $"Repository for {typeof(ISoftDeletable).Name} entity {typeof(TEntity).Name} is not of type {typeof(Repository<TEntity>).Name}.");
        }
    }
}
