
using Application.Common.Exceptions;
using Application.Service.Competence;
using Application.Service.Competence.Commands.CompetenceCreate;
using Application.Service.Competence.Commands.CompetenceDelete;
using Application.Service.Competence.Commands.CompetenceUpdate;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/competence")]
    public class CompetenceController : ApiControllerBase
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
        public async Task<IActionResult> Create([FromBody] CompetenceCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _competenceService.Create(dto);

                if (response == null)
                {
                    return BadRequest(new { success = false, message = "Error al crear la competencia" });
                }

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "competencia creada", });
            }

            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] CompetenceGetAllPageInputCommand dto)
        {
            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAll(userId: userId!, userService: _userService);
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CompetenceUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] CompetenceDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}