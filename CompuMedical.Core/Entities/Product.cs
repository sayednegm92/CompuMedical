using CompuMedical.Core.BaseEntities;

namespace CompuMedical.Core.Entities;

public class Product : BaseEntity
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public int? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid? StoreId { get; set; }
    public Store? Store { get; set; }
}
