namespace CompuMedical.Application.Dto.Item;

public class ItemDto
{
    public string ItemName { get; set; }
    public int? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal? Discount { get; set; } = 0;
    public Guid? ProductId { get; set; }
}
public class GetItemDto
{
    public string ItemName { get; set; }
    public int? Unit { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal? Discount { get; set; } = 0;
    public decimal Total { get; set; } = 0;
    public decimal? Net { get; set; } = 0;
}

