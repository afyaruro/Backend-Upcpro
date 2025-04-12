
using System.Net;
using Application.Common.Exceptions;
using Application.Service.Program;
using Application.Service.Program.Commands.ProgramCreate;
using Application.Service.Program.Commands.ProgramDelete;
using Application.Service.Program.Commands.ProgramGetAllPage;
using Application.Service.Program.Commands.ProgramUpdate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] ProgramCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _ProgramService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "Programa creado", });
            }

            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return InternalServerError();

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

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] ProgramUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _ProgramService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo el programas" });
                }

                return Ok(new { success = true, message = "El programa se ha actualizado exitosamente" });
            }
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }

            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
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

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] ProgramDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var result = await _ProgramService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino el programa" });
                }
                return Ok(new { success = result, message = "El programa se ha eliminado correctamente." });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }
    }

}