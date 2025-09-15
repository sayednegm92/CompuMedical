using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Helper;

namespace CompuMedical.Application.Services.Products;

public interface IProductServices
{
    Task<GeneralResponse> AddNewProduct(ProductDto dto);
}
