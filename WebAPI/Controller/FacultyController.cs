
using System.Net;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Service.Faculty;
using Application.Service.Faculty.Commands.FacultyCreate;
using Application.Service.Faculty.Commands.FacultyDelete;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Application.Service.Faculty.Commands.FacultyUpdate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/faculty")]
    public class FacultyController : ControllerBase
    {
        private readonly FacultyService _FacultyService;
        private readonly UserService _userService;

        public FacultyController(FacultyService service, UserService userService)
        {
            _FacultyService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] CreateInputFacultyCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }
                var response = await _FacultyService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "facultad creada", });
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
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] GetAllPageFacultyInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }
                var response = await _FacultyService.GetAllPage(dto);

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

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateFacultyCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }
                var response = await _FacultyService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo la facultad" });
                }

                return Ok(new { success = true, message = "La facultad se ha actualizado exitosamente" });
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

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteFacultyCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }
                var result = await _FacultyService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino la facultad" });
                }
                return Ok(new { success = result, message = "La facultad se ha eliminado correctamente." });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }


    }
}