using System;
using System.Threading;
using System.Threading.Tasks;

namespace Labradoratory.Fetch.AddOn.SoftDelete
{
    public interface ISoftDeletableRepositoryExtentions<TEntity>
    {
        Task RestoreSoftDeletedAsync(TEntity entity, CancellationToken cancellationToken);
        Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }

    public static class SoftDeletableRepositoryExtensions
    {
        public static Task RestoreSoftDeletedAsync<TEntity>(this Repository<TEntity> repository, TEntity entity, CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (repository is ISoftDeletableRepositoryExtentions<TEntity> sd)
                return sd.RestoreSoftDeletedAsync(entity, cancellationToken);

            throw RepositoryNotExtended<TEntity>();
        }

        public static Task SoftDeleteAsync<TEntity>(this Repository<TEntity> repository, TEntity entity, CancellationToken cancellationToken)
            where TEntity : Entity
        {
            if (repository is ISoftDeletableRepositoryExtentions<TEntity> sd)
                return sd.SoftDeleteAsync(entity, cancellationToken);

            throw RepositoryNotExtended<TEntity>();
        }

        private static InvalidOperationException RepositoryNotExtended<TEntity>()
        {
            return new InvalidOperationException($"Repository does not implement {typeof(ISoftDeletableRepositoryExtentions<TEntity>).Name}");
        }
    }
}
