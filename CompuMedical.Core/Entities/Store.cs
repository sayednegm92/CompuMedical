using CompuMedical.Core.BaseEntities;

namespace CompuMedical.Core.Entities;

public class Store : BaseEntity
{
    public string StoreName { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}
