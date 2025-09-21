using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Helper;

namespace CompuMedical.Application.IServices.IProducts;

public interface IProductServices
{
    Task<GeneralResponse> AddNewProduct(ProductDto dto);
    Task<GeneralResponse> GetProducts(Guid StoreId);
    GeneralResponse GetProductUnits();
}
