using CompuMedical.Application.Dto.Auth;
using CompuMedical.Application.IServices.IAuthServices;
using CompuMedical.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CompuMedical.MVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAuthService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HomeController(IAuthService service, IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _service = service;

    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.UserName))
        {
            return BadRequest("Username and Password are Required");
        }
        var result = await _service.Login(dto);
        if (result != null && result.Token != null)
        {
            _httpContextAccessor.HttpContext?.Response.SetCookie("JWToken", result.Token, 60); // store 60 mins

        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password) || string.IsNullOrWhiteSpace(dto.Email))
        {
            return BadRequest("Username and Password and Email are Required");
        }
        var result = await _service.Register(dto);
        return Ok(result);
    }

}
