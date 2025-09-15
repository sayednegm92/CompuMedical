using CompuMedical.Application.Dto.Item;

namespace CompuMedical.Application.Dto.Invoice;


public class InvoiceDto
{
    public decimal TotalAmount { get; set; }
    public decimal? Taxes { get; set; }
    public Guid? StoreId { get; set; }
    public List<ItemDto> Items { get; set; } = new List<ItemDto>();

}
public class GetInvoicesDto
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? Taxes { get; set; }
    public Guid? StoreId { get; set; }
    public decimal? Net => TotalAmount + (Taxes ?? 0);

}
