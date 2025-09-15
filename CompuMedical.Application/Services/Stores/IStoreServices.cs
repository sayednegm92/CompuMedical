using CompuMedical.Application.Dto.Store;
using CompuMedical.Application.Helper;

namespace CompuMedical.Application.Services.Stores;

public interface IStoreServices
{
    Task<GeneralResponse> AddNewStore(StoreDto dto);
}
