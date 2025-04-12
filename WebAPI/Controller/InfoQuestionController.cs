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
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/info-question")]
    public class InfoQuestionController : ApiControllerBase
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
        public async Task<IActionResult> Create([FromBody] InfoQuestionCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin"); 
                if (resp != null)
                {
                    return resp;
                }

                var response = await _InfoQuestionService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "Informacio de preguntas creada", });
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
        public async Task<IActionResult> GetAll([FromBody] InfoQuestionGetAllPageInputCommand dto)
        {
            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAdminStudent(userId: userId!, userService: _userService); 
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InfoQuestionUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin"); 
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] InfoQuestionDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin"); 
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}