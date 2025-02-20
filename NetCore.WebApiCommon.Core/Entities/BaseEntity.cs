namespace NetCore.WebApiCommon.Core.Entities;

public abstract class BaseEntity<TKey>
{
    public required TKey Id { get; set; }
}