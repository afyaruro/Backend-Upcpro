
using Application.Common.Exceptions;
using Application.Service.SimulacrumResult;
using Application.Service.SimulacrumResult.Commands.SimulacrumResultCreate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/SimulacrumResult")]
    public class SimulacrumResultController : ApiControllerBase
    {
        private readonly UserService _userService;
        private readonly SimulacrumResultService _SimulacrumResultService;

        public SimulacrumResultController(UserService userService, SimulacrumResultService SimulacrumResultService)
        {
            _userService = userService;
            _SimulacrumResultService = SimulacrumResultService;
        }

        [HttpPost("create-default")]
        public async Task<IActionResult> CrearSimulacrumResult([FromBody] SimulacrumResultCreateInputCommand dto)
        {
            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _SimulacrumResultService.CreateDefault(dto, userId!);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se creo el SimulacrumResult" });
                }


                return Created(nameof(CrearSimulacrumResult), new { success = true, message = "SimulacrumResult creado correctamente" });
            }
            catch (ValidationException ex)
            {
                return HandleValidationException(ex);
            }
            catch (EntityExistException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [HttpGet("get-all-by-user")]
        public async Task<IActionResult> GetAllByUser()
        {
            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _SimulacrumResultService.GetAll(userId!);

                if (response == null || response.Count == 0)
                {
                    return NotFound(new { success = false, message = "No se Encontraron SimulacrumResults para este usuario" });
                }

                return Ok(new { success = true, message = "SimulacrumResults del usuario obtenidos", data = response });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }




    }
}