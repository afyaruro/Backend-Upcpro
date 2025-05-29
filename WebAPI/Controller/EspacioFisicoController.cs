
using Application.Service.EspacioFisico;
using Application.Service.EspacioFisico.Commands.EspacioFisicoGetAllPage;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/espacio-fisico")]
    public class EspacioFisicoController : ApiControllerBase
    {
        private readonly EspacioFisicoService _EspacioFisicoService;
        private readonly UserService _userService;

        public EspacioFisicoController(EspacioFisicoService service, UserService userService)
        {
            _EspacioFisicoService = service;
            _userService = userService;
        }


        [HttpPost("get-all-sync")]
        public async Task<IActionResult> GetAllSync([FromBody] EspacioFisicoGetAllPageSyncInputCommand dto)
        {
            try
            {

                var response = await _EspacioFisicoService.GetAllSync(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                if (response.listEntity == null || response.listEntity.Count == 0)
                    return NotFound(new { success = false, message = "No se encontraron resultados" });

                return Ok(new { success = true, message = response.message, totalRegistros = response.totalRecords, totalPages = response.totalPages, data = response.listEntity });
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

                return BadRequest(new
                {
                    success = false,
                    errors = errors
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] EspacioFisicoGetAllPageInputCommand dto)
        {
            try
            {

                var response = await _EspacioFisicoService.GetAllPage(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                if (response.listEntity == null || response.listEntity.Count == 0)
                    return NotFound(new { success = false, message = "No se encontraron resultados" });

                return Ok(new { success = true, message = response.message, totalRegistros = response.totalRecords, totalPages = response.totalPages, data = response.listEntity });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



    }
}