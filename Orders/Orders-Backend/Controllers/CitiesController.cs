﻿using Microsoft.AspNetCore.Mvc;
using Orders_Backend.UnitOfWork.Interfaces;
using Orders_Shared.DTOs;
using Orders_Shared.Entities;

namespace Orders_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : GenericController<City>
    {
        private readonly ICitiesUnitOfWork _citiesUnitOfWork;

        public CitiesController(IGenericUnitOfWork<City> unitOfWork, ICitiesUnitOfWork citiesUnitOfWork) : base(unitOfWork)
        {
            _citiesUnitOfWork = citiesUnitOfWork;
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _citiesUnitOfWork.GetAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }

        [HttpGet("totalPages")]
        public override async Task<IActionResult> GetTotalPagesAsync([FromQuery] PaginationDTO pagination)
        {
            var action = await _citiesUnitOfWork.GetTotalPagesAsync(pagination);
            if (action.WasSuccess)
            {
                return Ok(action.Result);
            }
            return BadRequest();
        }
    }
}