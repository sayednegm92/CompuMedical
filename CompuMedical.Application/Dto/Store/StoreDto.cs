namespace CompuMedical.Application.Dto.Store;

public class StoreDto
{
    public string StoreName { get; set; }
}
public class GetStoreDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
}