namespace NetCore.Architecture.Core.Entities;

public abstract class BaseVersionableEntity<TKey, TUserKey> : BaseAuditableEntity<TKey, TUserKey>
{
    public int Version { get; set; }
    
    protected override void SetAuditedInfo(TUserKey auditedBy, DateTime auditedAt)
    {
        base.SetAuditedInfo(auditedBy, auditedAt);
        Version++;
    }

    protected override void SetAuditor(TUserKey auditedBy)
    {
        base.SetAuditor(auditedBy);
        Version++;
    }
}