using CompuMedical.Application.Dto.Invoice;
using CompuMedical.Application.Helper;

namespace CompuMedical.Application.Services.Invoices;

public interface IInvoiceServices
{
    Task<GeneralResponse> CreateNewInvoice(InvoiceDto dto);
    Task<GeneralResponse> GetAllInvoices(Creteria request);
    Task<GeneralResponse> GetInvoiceDetails(Guid InvoiceId);
    Task<GeneralResponse> DeleteInvoice(Guid id);
}
