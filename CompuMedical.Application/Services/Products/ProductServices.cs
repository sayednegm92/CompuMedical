using AutoMapper;
using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IProducts;
using CompuMedical.Core.Entities;
using CompuMedical.Core.Enums;
using CompuMedical.Core.IBasRepository;
using CompuMedical.Infrastructure.Helper;

namespace CompuMedical.Application.Services.Products;

public class ProductServices : IProductServices
{
    #region Fields
    private readonly IRepositoryApp<Product> _productRepo;
    private readonly IMapper _mapper;
    private readonly IResponseHandler _responseHandler;
    #endregion
    #region Constractor
    public ProductServices(IRepositoryApp<Product> productRepo, IMapper mapper, IResponseHandler responseHandler)
    {
        _mapper = mapper;
        _productRepo = productRepo;
        _responseHandler = responseHandler;
    }

    #endregion
    #region Handle Functions
    public async Task<GeneralResponse> AddNewProduct(ProductDto dto)
    {
        if (dto == null)
            return _responseHandler.ErrorMessage("Product cannot be null or empty.");
        var productMapper = _mapper.Map<Product>(dto);
        var result = await _productRepo.AddAsync(productMapper);
        return _responseHandler.SuccessMessage("Product Added Successfully ");
    }

    public async Task<GeneralResponse> GetProducts(Guid StoreId)
    {
        var query = await _productRepo.GetAllAsync(s => s.StoreId == StoreId && s.IsDeleted == false);
        var result = _mapper.Map<IEnumerable<GetProductDto>>(query);
        return _responseHandler.Success(result);
    }

    public GeneralResponse GetProductUnits()
    {
        var result = CommonExtenion.GetEnumList<Units>();
        return _responseHandler.Success(result);
    }
    #endregion
}
