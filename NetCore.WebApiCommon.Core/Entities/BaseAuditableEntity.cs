namespace NetCore.WebApiCommon.Core.Entities;

public abstract class BaseAuditableEntity<TKey, TUserKey> : BaseCreatableEntity<TKey, TUserKey>
{
    public TUserKey AuditedBy { get; set; }
    public DateTime AuditedAt { get; set; }
    public bool IsDeleted { get; set; }

    public virtual void SetAuditedInfo(TUserKey auditedBy, DateTime auditedAt)
    {
        AuditedBy = auditedBy;
        AuditedAt = auditedAt;
    }
    
    public virtual void SetAuditor(TUserKey auditedBy)
    {
        AuditedBy = auditedBy;
        AuditedAt = DateTime.Now;
    }

    public void SetDeletedInfo(TUserKey deletedBy)
    {
        SetAuditor(deletedBy);
        IsDeleted = true;
    }
}