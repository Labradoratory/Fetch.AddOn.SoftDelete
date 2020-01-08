using Labradoratory.Fetch.Processors;
using Labradoratory.Fetch.Processors.DataPackages;
using Microsoft.Extensions.DependencyInjection;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Extensions
{
    /// <summary>
    /// Methods to make working with <see cref="IServiceCollection"/> a little easier.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <typeparamref name="TEntity"/> soft delete restoring processor, as transient.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The <paramref name="serviceCollection"/> so calls can be chained.</returns>
        public static IServiceCollection AddFetchRestoringProcessor<TEntity, TProcessor>(this IServiceCollection serviceCollection)
            where TEntity : Entity, ISoftDeletable
            where TProcessor : class, IProcessor<EntityRestoringPackage<TEntity>>
        {
            serviceCollection.AddTransient<IProcessor<EntityRestoringPackage<TEntity>>, TProcessor>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the <typeparamref name="TEntity"/> soft delete restored processor, as transient.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The <paramref name="serviceCollection"/> so calls can be chained.</returns>
        public static IServiceCollection AddFetchRestoredProcessor<TEntity, TProcessor>(this IServiceCollection serviceCollection)
            where TEntity : Entity, ISoftDeletable
            where TProcessor : class, IProcessor<EntityRestoredPackage<TEntity>>
        {
            serviceCollection.AddTransient<IProcessor<EntityRestoredPackage<TEntity>>, TProcessor>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the <typeparamref name="TEntity"/> soft deleting processor, as transient.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The <paramref name="serviceCollection"/> so calls can be chained.</returns>
        public static IServiceCollection AddFetchSoftDeletingProcessor<TEntity, TProcessor>(this IServiceCollection serviceCollection)
            where TEntity : Entity, ISoftDeletable
            where TProcessor : class, IProcessor<EntitySoftDeletingPackage<TEntity>>
        {
            serviceCollection.AddTransient<IProcessor<EntitySoftDeletingPackage<TEntity>>, TProcessor>();
            return serviceCollection;
        }

        /// <summary>
        /// Adds the <typeparamref name="TEntity"/> soft deleted processor, as transient.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProcessor">The type of the processor.</typeparam>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The <paramref name="serviceCollection"/> so calls can be chained.</returns>
        public static IServiceCollection AddFetchSoftDeletedProcessor<TEntity, TProcessor>(this IServiceCollection serviceCollection)
            where TEntity : Entity, ISoftDeletable
            where TProcessor : class, IProcessor<EntitySoftDeletedPackage<TEntity>>
        {
            serviceCollection.AddTransient<IProcessor<EntitySoftDeletedPackage<TEntity>>, TProcessor>();
            return serviceCollection;
        }
    }
}
