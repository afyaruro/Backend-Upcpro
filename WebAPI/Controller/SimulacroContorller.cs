
using System.Net;
using Application.Common.Exceptions;
using Application.Service.Simulacro;
using Application.Service.Simulacro.Commands.GenerarSimulacro;
using Application.Service.Simulacro.Commands.SimulacroCreate;
using Application.Service.Simulacro.Commands.SimulacroDelete;
using Application.Service.Simulacro.Commands.SimulacroGet;
using Application.Service.Simulacro.Commands.SimulacroUpdate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/simulacro")]
    public class SimulacroController : ApiControllerBase
    {

        private readonly SimulacroService _simulacroService;
        private readonly UserService _userService;


        public SimulacroController(SimulacroService service, UserService userService)
        {
            _simulacroService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] SimulacroCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }


                var response = await _simulacroService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "Simulacro creado", });
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

        [Authorize]
        [HttpPost("generar-simulacro")]
        public async Task<IActionResult> Generate([FromBody] GenerarSimulacroInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }


                var response = await _simulacroService.GenerarSimulacro(dto);

                if (response == null)
                    return BadRequest();

                return Ok(new { success = true, preguntasTotales = response.Count, data = response, message = "Simulacro generado correctamente", });
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

        [Authorize]
        [HttpPost("get-all-active")]
        public async Task<IActionResult> GetAllActive([FromBody] SimulacroGetInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAdminStudent(userId: userId!, userService: _userService);
                if (resp != null)
                {
                    return resp;
                }
                var response = await _simulacroService.GetAllActive(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                if (response.listEntity == null || response.listEntity.Count == 0)
                    return NotFound(new { success = false, message = "No se encontraron resultados" });

                return Ok(new { success = true, message = response.message, data = response.listEntity });
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
        public async Task<IActionResult> Update([FromBody] SimulacroUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _simulacroService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo el Simulacros" });
                }

                return Ok(new { success = true, message = "El Simulacro se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] SimulacroDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var result = await _simulacroService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino el Simulacro" });
                }
                return Ok(new { success = result, message = "El Simulacro se ha eliminado correctamente." });
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