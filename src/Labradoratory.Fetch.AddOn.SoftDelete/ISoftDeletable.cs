using System;

namespace Labradoratory.Fetch.AddOn.SoftDelete
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
