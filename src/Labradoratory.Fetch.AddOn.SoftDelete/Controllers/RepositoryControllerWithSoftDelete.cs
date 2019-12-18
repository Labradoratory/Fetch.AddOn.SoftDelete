using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Labradoratory.Fetch.AddOn.SoftDelete.Authorization;
using Labradoratory.Fetch.AddOn.SoftDelete.Extensions;
using Labradoratory.Fetch.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Controllers
{
    /// <summary>
    /// An implementation of <see cref="RepositoryController{TEntity}"/> that support soft deletion.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class RepositoryControllerWithSoftDelete<TEntity> : RepositoryControllerWithSoftDelete<TEntity, TEntity>
        where TEntity : Entity, ISoftDeletable
    {
        public RepositoryControllerWithSoftDelete(
            Repository<TEntity> repository,
            IMapper mapper,
            IAuthorizationService authorizationService)
            : base(repository, mapper, authorizationService)
        {
        }
    }

    /// <summary>
    /// An implementation of <see cref="RepositoryController{TEntity, TView}"/> that support soft deletion.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="EntityRepositoryController{TEntity, TEntity}" />
    public abstract class RepositoryControllerWithSoftDelete<TEntity, TView> : RepositoryController<TEntity, TView>
        where TEntity : Entity, ISoftDeletable
        where TView: class
    {
        public RepositoryControllerWithSoftDelete(
            Repository<TEntity> repository,
            IMapper mapper, 
            IAuthorizationService authorizationService)
            : base(repository, mapper, authorizationService)
        {
        }

        protected override IQueryable<TEntity> FilterGetAll(IQueryable<TEntity> query)
        {
            // The GetAll method should only return entities that are NOT deleted.
            return query.GetNotDeleted();
        }

        [HttpGet, Route("deleted")]
        public virtual async Task<ActionResult<List<TEntity>>> GetDeleted(CancellationToken cancellationToken)
        {
            var authorizationResult = await AuthorizationService.AuthorizeAsync(User, typeof(TEntity), EntityAuthorizationPolicies.GetAllDeleted);
            if (!authorizationResult.Succeeded)
                return AuthorizationFailed(authorizationResult);

            var entities = await Repository.GetAsyncQueryResolver(FilterGetAll).ToListAsync(cancellationToken);
            authorizationResult = await AuthorizationService.AuthorizeAsync(User, entities, EntityAuthorizationPolicies.GetSomeDeleted);
            if (!authorizationResult.Succeeded)
                return AuthorizationFailed(authorizationResult);

            return Ok(Mapper.Map<IEnumerable<TView>>(entities));
        }

        /// <inheritdoc />
        public override async Task<IActionResult> Delete(string encodedKeys, CancellationToken cancellationToken)
        {
            var keys = Entity.DecodeKeys<TEntity>(encodedKeys);
            var entity = await Repository.FindAsync(keys, cancellationToken);
            if (entity == null)
                return NotFound();

            if (entity.IsDeleted)
                return Ok();

            var authorizationResult = await AuthorizationService.AuthorizeAsync(User, entity, EntityAuthorizationPolicies.SoftDelete);
            if (!authorizationResult.Succeeded)
                return AuthorizationFailed(authorizationResult);

            await Repository.SoftDeleteAsync(entity, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Restores an entity that has been deleted.
        /// </summary>
        /// <param name="encodedKeys">An encoded string representation of the keys to identify an instance of an entity.</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns></returns>
        [HttpPut, Route("{encodedKeys}/restore")]
        public virtual async Task<IActionResult> RestoreSoftDeleted(string encodedKeys, CancellationToken cancellationToken)
        {
            var keys = Entity.DecodeKeys<TEntity>(encodedKeys);
            var entity = await Repository.FindAsync(keys, cancellationToken);
            if (entity == null)
                return NotFound();

            if (!entity.IsDeleted)
                return Ok();

            var authorizationResult = await AuthorizationService.AuthorizeAsync(User, entity, EntityAuthorizationPolicies.SoftDelete);
            if (!authorizationResult.Succeeded)
                return AuthorizationFailed(authorizationResult);

            await Repository.RestoreSoftDeletedAsync(entity, cancellationToken);

            return Ok();
        }
    }
}
