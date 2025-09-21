using AutoMapper;
using CompuMedical.Application.Dto.Store;
using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IStores;
using CompuMedical.Core.Entities;
using CompuMedical.Core.IBasRepository;

namespace CompuMedical.Application.Services.Stores;

public class StoreServices : IStoreServices
{
    #region Fields
    private readonly IRepositoryApp<Store> _storeRepo;
    private readonly IMapper _mapper;
    private readonly IResponseHandler _responseHandler;
    #endregion
    #region Constractor
    public StoreServices(IRepositoryApp<Store> storeRepo, IMapper mapper, IResponseHandler responseHandler)
    {
        _mapper = mapper;
        _storeRepo = storeRepo;
        _responseHandler = responseHandler;
    }

    #endregion
    #region Handle Functions
    public async Task<GeneralResponse> AddNewStore(StoreDto dto)
    {
        if (dto == null)
            return _responseHandler.ErrorMessage("Store cannot be null or empty.");
        var storeMapper = _mapper.Map<Store>(dto);
        var result = await _storeRepo.AddAsync(storeMapper);
        return _responseHandler.SuccessMessage("Item Added Successfully ");
    }

    public async Task<GeneralResponse> GetStores()
    {
        var query = await _storeRepo.GetAllAsync(s => s.IsDeleted == false);
        var result = _mapper.Map<IEnumerable<GetStoreDto>>(query);
        return _responseHandler.Success(result);
    }
    #endregion
}
