namespace CompuMedical.Core.BaseEntities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public string? CreatedByUserId { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedByUserId { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? DeletedByUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
