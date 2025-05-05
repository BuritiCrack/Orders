using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;

namespace Orders_Backend.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        private readonly ICategoriesUnitOfWork _categoriesUnitOfWork;

        public CategoriesController(IGenericUnitOfWork<Category> unitOfWork, ICategoriesUnitOfWork categoriesUnitOfWork) 
            : base(unitOfWork)
        {
            _categoriesUnitOfWork = categoriesUnitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("combo")]
        public async Task<IActionResult> GetComboAsync()
        {
            return Ok(await _categoriesUnitOfWork.GetComboAsync());
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync(PaginationDTO pagination)
        {
            var action = await _categoriesUnitOfWork.GetAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetTotalPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _categoriesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}
