namespace CompuMedical.Application.Dto.Product;

public class ProductDto
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public int? Unit { get; set; } = 3;
    public decimal UnitPrice { get; set; }
    public Guid? StoreId { get; set; }
}
