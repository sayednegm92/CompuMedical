using CompuMedical.Application.Dto.Store;
using CompuMedical.Application.Helper;

namespace CompuMedical.Application.IServices.IStores;

public interface IStoreServices
{
    Task<GeneralResponse> AddNewStore(StoreDto dto);
    Task<GeneralResponse> GetStores();
}
