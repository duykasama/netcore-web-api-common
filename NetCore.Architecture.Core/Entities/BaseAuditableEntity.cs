namespace NetCore.Architecture.Core.Entities;

public abstract class BaseAuditableEntity<TKey, TUserKey> : BaseCreatableEntity<TKey, TUserKey>
{
    public TUserKey AuditedBy { get; set; }
    public DateTime AuditedAt { get; set; }
    public bool IsDeleted { get; set; }

    protected virtual void SetAuditedInfo(TUserKey auditedBy, DateTime auditedAt)
    {
        AuditedBy = auditedBy;
        AuditedAt = auditedAt;
    }
    
    protected virtual void SetAuditor(TUserKey auditedBy)
    {
        AuditedBy = auditedBy;
        AuditedAt = DateTime.Now;
    }

    protected void SetDeletedInfo(TUserKey deletedBy)
    {
        SetAuditor(deletedBy);
        IsDeleted = true;
    }
}