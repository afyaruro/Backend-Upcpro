
using Application.Common.Exceptions;
using Application.Service.Level;
using Application.Service.Level.Commands.LevelCreate;
using Application.Service.Level.Commands.LevelDelete;
using Application.Service.Level.Commands.LevelUpdate;
using Application.Service.Faculty.Commands.FacultyGetAllPage;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/level")]
    public class LevelController : ApiControllerBase
    {
        private readonly LevelService _LevelService;
        private readonly UserService _userService;


        public LevelController(LevelService service, UserService userService)
        {
            _LevelService = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create([FromBody] LevelCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");

                var response = await _LevelService.Create(dto);

                if (response == null)
                {
                    return BadRequest(new { success = false, message = "Error al crear el nivel" });
                }

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "nivel creado", });
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
        public async Task<IActionResult> GetAll([FromBody] LevelGetAllPageInputCommand dto)
        {
            try
            {

                var response = await _LevelService.GetAllPage(dto);

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
        public async Task<IActionResult> Update([FromBody] LevelUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");


                var response = await _LevelService.Update(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo el nivel" });
                }

                return Ok(new { success = true, message = "El nivel se ha actualizado exitosamente" });
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
        public async Task<IActionResult> Delete([FromBody] LevelDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");

                var result = await _LevelService.Delete(dto);
                if (!result)
                {
                    return BadRequest(new { success = false, message = "No se elimino el nivel" });
                }
                return Ok(new { success = result, message = "El nivel se ha eliminado correctamente." });
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