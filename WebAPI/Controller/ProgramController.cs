
using Application.Service.Program;
using Application.Service.Program.Commands.ProgramGetAllPage;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/program")]
    public class ProgramController : ApiControllerBase
    {

        private readonly ProgramService _ProgramService;
        private readonly UserService _userService;


        public ProgramController(ProgramService service, UserService userService)
        {
            _ProgramService = service;
            _userService = userService;
        }



        [HttpPost("get-all-sync")]
        public async Task<IActionResult> GetAllSync([FromBody] ProgramGetAllPageSyncInputCommand dto)
        {
            try
            {

                var response = await _ProgramService.GetAllSync(dto);

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
        public async Task<IActionResult> GetAll([FromBody] ProgramGetAllPageInputCommand dto)
        {
            try
            {
                var response = await _ProgramService.GetAllPage(dto);

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
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }


    }

}