using CompuMedical.Application.Dto.Invoice;
using CompuMedical.Application.Helper;
using CompuMedical.Application.Services.Invoices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompuMedical.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        #region Fields
        private readonly IInvoiceServices _service;
        #endregion
        #region Constractor
        public InvoiceController(IInvoiceServices service)
        {
            _service = service;
        }
        #endregion
        #region Actions

        [HttpGet("GetAllInvoices")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAllInvoices([FromQuery] Creteria request)
        {
            var result = await _service.GetAllInvoices(request);
            return Ok(result);
        }
        [HttpGet("GetInvoiceDetails")]
        [Authorize(Roles = "superadmin")]
        public async Task<IActionResult> GetInvoiceDetails(Guid InvoiceId)
        {
            var result = await _service.GetInvoiceDetails(InvoiceId);
            return Ok(result);
        }

        [HttpPost("CreateNewInvoice")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> CreateNewInvoice([FromBody] InvoiceDto dto)
        {
            var result = await _service.CreateNewInvoice(dto);
            return Ok(result);
        }

        [HttpDelete("DeleteInvoice")]
        [Authorize(Roles = "superadmin")]
        public async Task<IActionResult> DeleteInvoice(Guid id)
        {
            var result = await _service.DeleteInvoice(id);
            return Ok(result);
        }

        #endregion
    }
}
