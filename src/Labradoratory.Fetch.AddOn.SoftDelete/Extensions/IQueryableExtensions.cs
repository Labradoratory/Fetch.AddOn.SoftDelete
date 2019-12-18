using System.Linq;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> GetNotDeleted<TEntity>(this IQueryable<TEntity> query)
            where TEntity : Entity, ISoftDeletable
        {
            return query.Where(e => !e.IsDeleted);
        }

        public static IQueryable<TEntity> GetDeleted<TEntity>(this IQueryable<TEntity> query)
            where TEntity : Entity, ISoftDeletable
        {
            return query.Where(e => e.IsDeleted);
        }
    }
}
