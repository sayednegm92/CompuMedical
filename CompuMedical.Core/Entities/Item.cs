using CompuMedical.Core.BaseEntities;

namespace CompuMedical.Core.Entities;

public class Item : BaseEntity
{
    public string ItemName { get; set; }
    public string? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal? Discount { get; set; } = 0;
    public decimal Total => UnitPrice * Quantity;
    public decimal? Net => Total - (Discount ?? 0);
    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }
    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }


}
