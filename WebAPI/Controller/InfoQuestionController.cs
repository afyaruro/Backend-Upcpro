
using System.Net;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Service.InfoQuestion;
using Application.Service.InfoQuestion.Commands.InfoQuestionCreate;
using Application.Service.InfoQuestion.Commands.InfoQuestionDelete;
using Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage;
using Application.Service.InfoQuestion.Commands.InfoQuestionUpdate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoQuestionController : ControllerBase
    {

        private readonly InfoQuestionService _InfoQuestionService;
        private readonly UserService _userService;


        public InfoQuestionController(InfoQuestionService service, UserService userService)
        {
            _InfoQuestionService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] CreateInputInfoQuestionCommand dto)
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

                var response = await _InfoQuestionService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "Informacio de preguntas creada", });
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
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] GetAllPageInfoQuestionInputCommand dto)
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
                var response = await _InfoQuestionService.GetAllPage(dto);

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
        public async Task<IActionResult> Update([FromBody] UpdateInfoQuestionCommand dto)
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
                var response = await _InfoQuestionService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo la informacion de preguntas" });
                }

                return Ok(new { success = true, message = "La informacion de preguntas se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] DeleteInfoQuestionCommand dto)
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
                var result = await _InfoQuestionService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino la informacion de preguntas" });
                }
                return Ok(new { success = result, message = "La informacion de preguntas se ha eliminado correctamente." });
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