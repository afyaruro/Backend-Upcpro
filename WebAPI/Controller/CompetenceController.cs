using System.Net;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Service.Competence;
using Application.Service.Competence.Commands.CompetenceCreate;
using Application.Service.Competence.Commands.CompetenceDelete;
using Application.Service.Competence.Commands.CompetenceGetAllPage;
using Application.Service.Competence.Commands.CompetenceUpdate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/competence")]
    public class CompetenceController : ControllerBase
    {
        private readonly CompetenceService _competenceService;
        private readonly UserService _userService;


        public CompetenceController(CompetenceService service, UserService userService)
        {
            _competenceService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] CreateInputCompetenceCommand dto)
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
                var response = await _competenceService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "competencia creada", });
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
        public async Task<IActionResult> GetAll([FromBody] GetAllPageCompetenceInputCommand dto)
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
                var response = await _competenceService.GetAllPage(dto);

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
        public async Task<IActionResult> Update([FromBody] UpdateCompetenceCommand dto)
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
                var response = await _competenceService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo la competencia" });
                }

                return Ok(new { success = true, message = "La competencia se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] DeleteCompetenceCommand dto)
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
                var result = await _competenceService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino la competencia" });
                }
                return Ok(new { success = result, message = "La competencia se ha eliminado correctamente." });
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