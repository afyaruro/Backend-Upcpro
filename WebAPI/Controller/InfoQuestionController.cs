
using Application.Service.InfoQuestion;
using Application.Service.InfoQuestion.Commands.InfoQuestionGetAllPage;
using Application.Service.User;
using FluentValidation;
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

        [HttpPost("get-all-sync")]
        public async Task<IActionResult> GetAllSync([FromBody] InfoQuestionGetAllPageSyncInputCommand dto)
        {
            try
            {

                var response = await _InfoQuestionService.GetAllSync(dto);

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
        public async Task<IActionResult> GetAll([FromBody] InfoQuestionGetAllPageInputCommand dto)
        {
            try
            {


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


    }
}