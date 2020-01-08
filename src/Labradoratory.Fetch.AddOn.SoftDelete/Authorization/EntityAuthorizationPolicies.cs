using Labradoratory.Fetch.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Labradoratory.Fetch.AddOn.SoftDelete.Authorization
{
    /// <summary>
    /// The names of <see cref="AuthorizationPolicy"/> instances related to <see cref="Entity"/> access.
    /// </summary>
    public static class EntityAuthorizationPolicies
    {
        /// <summary>Resource authorize policy for getting all of a type of deleted entity.</summary>
        /// <remarks>The resource will be a <see cref="Type"/> of entity.</remarks>
        public static readonly EntityAuthorizationPolicy GetAllDeleted = "EntityGetAllDeleted";

        /// <summary>Resource authorize policy for getting some of a type of deleted entity.</summary>
        /// <remarks>The resource will be list of entities that should be filtered to only those accessible.</remarks>
        public static readonly EntityAuthorizationPolicy GetSomeDeleted = "EntityGetSomeDeleted";

        /// <summary>Resource authorize policy for soft deleting an entity.</summary>
        /// <remarks>The resource will be an <see cref="Entity"/>.</remarks>
        public static readonly EntityAuthorizationPolicy SoftDelete = "EntitySoftDelete";

        /// <summary>Resource authorize policy for restoring a soft deleted entity.</summary>
        /// <remarks>The resource will be an <see cref="Entity"/>.</remarks>
        public static readonly EntityAuthorizationPolicy Restore = "EntityRestoreSoftDelete";
    }
}
