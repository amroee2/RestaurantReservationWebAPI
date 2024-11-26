using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RestaurantReservationServices.DTOs.TableDTOs;
using RestaurantReservationServices.Exceptions;
using RestaurantReservationServices.Services.RestaurantManagementService;
using RestaurantReservationServices.Services.TableManagementService;

namespace RestaurantReservationWebAPI.Controllers
{
    [Route("api/Tables")]
    [Authorize]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _restaurantService;
        public TableController(ITableService tableService, IMapper mapper, IRestaurantService restaurantService)
        {
            _tableService = tableService;
            _mapper = mapper;
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TableReadDTO>>> GetAllTablesAsync()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}", Name ="GetTableById")]
        public async Task<ActionResult<TableReadDTO>> GetTableByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Table Id must be larger than 0");
            }
            try
            {
                var table = await _tableService.GetTableByIdAsync(id);
                return Ok(table);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddTableAsync(TableCreateDTO table)
        {
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(table.RestaurantId);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            int tableId = await _tableService.AddTableAsync(table);

            var response = new
            {
                tableId = tableId,
                table = table
            };

            return CreatedAtAction("GetTableById", new { Id = tableId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTableAsync(int id, TableUpdateDTO tableDto)
        {
            if (id <= 0)
            {
                return BadRequest("Table Id must be larger than 0");
            }
            try
            {
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(tableDto.RestaurantId);
                var table = await _tableService.GetTableByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _tableService.UpdateTableAsync(id, tableDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTableAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Table Id must be larger than 0");
            }
            try
            {
                var table = await _tableService.GetTableByIdAsync(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            await _tableService.DeleteTableAsync(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateTable(int id, JsonPatchDocument<TableUpdateDTO> patchDoc)
        {
            if (id <= 0)
            {
                return BadRequest("Table Id must be larger than 0");
            }
            try
            {
                var table = await _tableService.GetTableByIdAsync(id);
                var tableToPatch = _mapper.Map<TableUpdateDTO>(table);
                patchDoc.ApplyTo(tableToPatch, ModelState);
                if(!TryValidateModel(tableToPatch))
                {
                    return ValidationProblem(ModelState);
                }
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(table.RestaurantId);
                await _tableService.UpdateTableAsync(id, tableToPatch);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
