using AutoMapper;
using CompuMedical.Application.Dto.Auth;
using CompuMedical.Application.Dto.Invoice;
using CompuMedical.Application.Dto.Item;
using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Dto.Store;
using CompuMedical.Core.Entities;

namespace CompuMedical.Application.AutoMappers;

public class ApplicationMapperProfiles : Profile
{
    public ApplicationMapperProfiles()
    {
        #region Item
        CreateMap<ItemDto, Item>();
        CreateMap<StoreDto, Store>();
        CreateMap<Item, GetItemDto>();
        CreateMap<Store, GetStoreDto>()
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StoreName));
        CreateMap<Product, GetProductDto>()
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName));
        #endregion
        #region Product
        CreateMap<ProductDto, Product>().ReverseMap();
        #endregion
        #region Invoice
        CreateMap<InvoiceDto, Invoice>();
        CreateMap<Invoice, GetInvoicesDto>();

        #endregion

        #region AppAuthorization
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<UserRegisterDto, ApplicationUser>();
        #endregion


    }
}
