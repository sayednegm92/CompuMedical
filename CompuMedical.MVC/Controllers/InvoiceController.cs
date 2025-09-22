using CompuMedical.Application.Dto.Invoice;
using CompuMedical.Application.Dto.Item;
using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Dto.Store;
using CompuMedical.Application.Helper;
using CompuMedical.Application.IServices.IInvoices;
using CompuMedical.Application.IServices.IProducts;
using CompuMedical.Application.IServices.IStores;
using Microsoft.AspNetCore.Mvc;

namespace CompuMedical.MVC.Controllers;

public class InvoiceController : Controller
{
    private readonly ILogger<InvoiceController> _logger;
    private readonly IInvoiceServices _invoiceService;
    private readonly IProductServices _productService;
    private readonly IStoreServices _storeService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public InvoiceController(IInvoiceServices invoiceService, IProductServices productService, IStoreServices storeService, IHttpContextAccessor httpContextAccessor, ILogger<InvoiceController> logger)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _invoiceService = invoiceService;
        _productService = productService;
        _storeService = storeService;

    }
    public IActionResult Index()
    {
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> SearchInvoices(Creteria dto)
    {
        if (dto == null) return BadRequest("Please Enter Data !");
        IEnumerable<GetInvoicesDto> data = new List<GetInvoicesDto>();

        var result = await _invoiceService.GetAllInvoices(dto);
        if (result != null && result.Data is IEnumerable<GetInvoicesDto> invoices)
        {
            data = invoices;
        }

        return View(data);
    }
    [HttpGet]
    public async Task<IActionResult> InvoiceDetails(Guid InvoiceId)
    {
        IEnumerable<GetItemDto> data = new List<GetItemDto>();
        if (InvoiceId == Guid.Empty) return BadRequest("InvoiceId is Required !");
        var result = await _invoiceService.GetInvoiceDetails(InvoiceId);
        if (result != null && result.Data is IEnumerable<GetItemDto> invoiceItems)
        {
            data = invoiceItems;
        }
        return View(data);
    }
    [HttpPost]
    public async Task<IActionResult> SaveInvoice([FromBody] InvoiceDto dto)
    {
        if (dto == null) return BadRequest("Please Enter Data !");
        var result = await _invoiceService.CreateNewInvoice(dto);
        return Ok(result);
    }
    #region DropDwons
    [HttpGet]
    public async Task<IActionResult> GetStores()
    {
        IEnumerable<GetStoreDto> data = new List<GetStoreDto>();
        var result = await _storeService.GetStores();
        if (result != null && result.Data is IEnumerable<GetStoreDto> productData)
        {
            data = productData;
        }
        return Json(data);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(Guid StoreId)
    {
        IEnumerable<GetProductDto> data = new List<GetProductDto>();
        if (StoreId == Guid.Empty) return BadRequest("StoreId is Required !");
        var result = await _productService.GetProducts(StoreId);
        if (result != null && result.Data is IEnumerable<GetProductDto> productData)
        {
            data = productData;
        }
        return Json(data);
    }

    [HttpGet]
    public IActionResult GetProductUnits()
    {
        var result = _productService.GetProductUnits();
        return Json(result?.Data);
    }

    #endregion
}
