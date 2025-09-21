using AutoMapper;
using CompuMedical.Application.Dto.Auth;
using CompuMedical.Application.Dto.Invoice;
using CompuMedical.Application.Dto.Item;
using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Dto.Store;
using CompuMedical.Core.Entities;

namespace CompuMedical.MVC.AutoMappers;

public class MVCAutoMapperProfiles : Profile
{
    public MVCAutoMapperProfiles()
    {
        #region Invoice
        CreateMap<Invoice, GetInvoicesDto>();
        CreateMap<InvoiceDto, Invoice>()
       .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        #endregion

        #region Items
        CreateMap<Item, GetItemDto>();
        CreateMap<ItemDto, Item>();
        #endregion

        #region Store
        CreateMap<Store, GetStoreDto>()
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StoreName));
        #endregion

        #region Product
        CreateMap<Product, GetProductDto>()
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName));
        #endregion

        #region AppAuthorization
        CreateMap<ApplicationUser, UserDto>();
        CreateMap<UserRegisterDto, ApplicationUser>();
        #endregion


    }
}
