namespace NetCore.Architecture.Core.Entities;

public abstract class BaseCreatableEntity<TKey, TUserKey> : BaseEntity<TKey>
{
    public TUserKey CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public virtual void SetCreatedInfo(TUserKey createdBy, DateTime createdAt)
    {
        CreatedBy = createdBy;
        CreatedAt = createdAt;
    }
}