
using System.Net;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Service.Question;
using Application.Service.Question.Commands.QuestionCreate;
using Application.Service.Question.Commands.QuestionDelete;
using Application.Service.Question.Commands.QuestionGetAllPage;
using Application.Service.Question.Commands.QuestionUpdate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/question")]
    public class QuestionController : ApiControllerBase
    {

        private readonly QuestionService _QuestionService;
        private readonly UserService _userService;


        public QuestionController(QuestionService service, UserService userService)
        {
            _QuestionService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] QuestionCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");


                var response = await _QuestionService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "pregunta creada", });
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
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] QuestionGetAllPageInputCommand dto)
        {
            try
            {
                var response = await _QuestionService.GetAllPage(dto);

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
        public async Task<IActionResult> Update([FromBody] QuestionUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");

                var response = await _QuestionService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo la pregunta" });
                }

                return Ok(new { success = true, message = "La pregunta se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] QuestionDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");

                var result = await _QuestionService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino la pregunta" });
                }
                return Ok(new { success = result, message = "La pregunta se ha eliminado correctamente." });
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
    }
}