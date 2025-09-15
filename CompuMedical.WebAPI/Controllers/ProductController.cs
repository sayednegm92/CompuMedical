using CompuMedical.Application.Dto.Product;
using CompuMedical.Application.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompuMedical.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin,superadmin")]
public class ProductController : ControllerBase
{
    #region Fields
    private readonly IProductServices _service;
    #endregion
    #region Constractor
    public ProductController(IProductServices service)
    {
        _service = service;
    }
    #endregion
    #region Actions
    [HttpPost("AddNewProduct")]
    public async Task<IActionResult> AddNewProduct([FromBody] ProductDto dto)
    {
        var result = await _service.AddNewProduct(dto);
        return Ok(result);
    }

    #endregion
}
