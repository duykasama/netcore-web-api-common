namespace NetCore.WebApiCommon.Core.Entities;

public abstract class BaseVersionableEntity<TKey, TUserKey> : BaseAuditableEntity<TKey, TUserKey>
{
    public int Version { get; set; }

    public override void SetAuditedInfo(TUserKey auditedBy, DateTime auditedAt)
    {
        base.SetAuditedInfo(auditedBy, auditedAt);
        Version++;
    }

    public void SetAuditedInfoWithoutVersionUpdate(TUserKey auditedBy, DateTime auditedAt)
    {
        base.SetAuditedInfo(auditedBy, auditedAt);
    }
    
    public void SetAuditedInfoWithoutVersionUpdate(TUserKey auditedBy)
    {
        base.SetAuditor(auditedBy);
    }

    public override void SetAuditor(TUserKey auditedBy)
    {
        base.SetAuditor(auditedBy);
        Version++;
    }

    public override void SetCreatedInfo(TUserKey createdBy, DateTime createdAt)
    {
        base.SetCreatedInfo(createdBy, createdAt);
        Version = 0;
    }
}