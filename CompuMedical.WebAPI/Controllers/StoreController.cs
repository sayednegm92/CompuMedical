using CompuMedical.Application.Dto.Store;
using CompuMedical.Application.IServices.IStores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompuMedical.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin,superadmin")]
    public class StoreController : ControllerBase
    {
        #region Fields
        private readonly IStoreServices _service;
        #endregion
        #region Constractor
        public StoreController(IStoreServices service)
        {
            _service = service;
        }
        #endregion
        #region Actions
        [HttpPost("AddNewStore")]
        public async Task<IActionResult> AddNewStore([FromBody] StoreDto dto)
        {
            var result = await _service.AddNewStore(dto);
            return Ok(result);
        }

        #endregion
    }
}
