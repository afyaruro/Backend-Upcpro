
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
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/faculty")]
    public class FacultyController : ApiControllerBase
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
        public async Task<IActionResult> Create([FromBody] FacultyCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");

                var response = await _FacultyService.Create(dto);

                if (response == null)
                    return BadRequest();

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "facultad creada", });
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
        public async Task<IActionResult> GetAll([FromBody] FacultyGetAllPageInputCommand dto)
        {
            try
            {

                var response = await _FacultyService.GetAllPage(dto);

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
        public async Task<IActionResult> Update([FromBody] FacultyUpdateInputCommand dto)
        {
            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");


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
                return HandleValidationException(ex);
            }

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] FacultyDeleteInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                await CheckAccess(userId: userId!, userService: _userService, "admin");

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
                return HandleValidationException(ex);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


    }
}