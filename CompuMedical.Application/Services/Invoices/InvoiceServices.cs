using AutoMapper;
using CompuMedical.Application.Dto.Invoice;
using CompuMedical.Application.Dto.Item;
using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IInvoices;
using CompuMedical.Core.Entities;
using CompuMedical.Core.IBasRepository;

namespace CompuMedical.Application.Services.Invoices;

public class InvoiceServices : IInvoiceServices
{
    #region Fields
    private readonly IRepositoryApp<Invoice> _invoiceRepo;
    private readonly IRepositoryApp<Item> _itemRepo;
    private readonly IMapper _mapper;
    private readonly IResponseHandler _responseHandler;
    #endregion
    #region Constractor
    public InvoiceServices(IRepositoryApp<Invoice> invoiceRepo, IRepositoryApp<Item> itemRepo, IMapper mapper, IResponseHandler responseHandler)
    {
        _mapper = mapper;
        _invoiceRepo = invoiceRepo;
        _itemRepo = itemRepo;
        _responseHandler = responseHandler;
    }

    #endregion
    #region Handle Functions
    public async Task<GeneralResponse> CreateNewInvoice(InvoiceDto dto)
    {
        if (dto == null)
            return _responseHandler.ErrorMessage("Invoice cannot be null or empty.");

        var invoiceMapper = _mapper.Map<Invoice>(dto);
        if (invoiceMapper == null)
            return _responseHandler.ErrorMessage("Mapping resulted in null or empty Invoice details.");

        await _invoiceRepo.AddAsync(invoiceMapper);
        return _responseHandler.SuccessMessage("Invoice Created Successfully");
    }

    public async Task<GeneralResponse> GetAllInvoices(Creteria request)
    {
        IEnumerable<Invoice> query;
        var startDate = new DateTime();
        var endDate = new DateTime();
        if (request != null && request.InvoiceDate != null)
        {
            startDate = request.InvoiceDate.Value.Date;
            endDate = startDate.AddDays(1);
        }

        if (request == null || (request.StoreId == null && request.InvoiceId == null && request.InvoiceDate == null)) query = await _invoiceRepo.GetAllAsync(o => o.IsDeleted == false);

        else query = await _invoiceRepo.GetAllAsync(o => (o.Id == request.InvoiceId ||
                                         (o.CreatedDate >= startDate && o.CreatedDate < endDate) ||
                                         o.StoreId == request.StoreId) && o.IsDeleted == false);
        if (query == null || !query.Any())
            return _responseHandler.ShowMessage("No Invoices Found.");
        var result = _mapper.Map<IEnumerable<GetInvoicesDto>>(query);

        return _responseHandler.Success(result);
    }
    public async Task<GeneralResponse> GetInvoiceDetails(Guid InvoiceId)
    {
        IEnumerable<Item> query;
        query = await _itemRepo.GetAllAsync(a => a.InvoiceId == InvoiceId);
        if (query == null)
            return _responseHandler.ShowMessage("No Items in This Invoice.");
        var result = _mapper.Map<IEnumerable<GetItemDto>>(query);

        return _responseHandler.Success(result);
    }
    public async Task<GeneralResponse> DeleteInvoice(Guid id)
    {
        var entity = await _invoiceRepo.GetByIdAsync(id);
        if (entity == null) return _responseHandler.ErrorMessage("Invoice Not Found !");
        await _invoiceRepo.DeleteAsync(entity);
        return _responseHandler.Success("Invoice Delete Successfully");
    }

    #endregion
}
