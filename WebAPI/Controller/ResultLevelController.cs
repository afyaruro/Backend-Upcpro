
using Application.Common.Exceptions;
using Application.Service.Level;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;
using Application.Service.ResultLevel.Commands.GetRanking;
using Application.Service.ResultLevel.Commands.ResultLevelUpdate;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/result-level")]
    public class ResultLevelController : ApiControllerBase
    {
        private readonly ResultLevelService _service;
        private readonly UserService _userService;


        public ResultLevelController(ResultLevelService service, UserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("created")]
        public async Task<IActionResult> Create()
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }


                var response = await _service.Create(userId!);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "Error al configurar el resultado de nivel por competencia" });
                }

                return CreatedAtAction(nameof(Create), new { success = true, message = "resultado de nivel por competencia configurado", });
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
        [HttpPost("ranking-competence")]
        public async Task<IActionResult> Ranking([FromBody] RankingInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }

                var (response, posicionUser) = await _service.Ranking(dto, userId!);

                return Ok(new { success = true, message = "Top 10 obtenido", position = posicionUser, data = response });

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
        [HttpGet("get-all-by-user")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _service.GetAllUser(userId!);

                if (response.isError)
                {
                    return BadRequest(new { success = true, message = response.message });
                }

                return Ok(new { success = true, message = response.message, data = response.listEntity });

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
        public async Task<IActionResult> Update([FromBody] ResultLevelUpdateInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }


                var response = await _service.Update(dto, userId!);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo" });
                }

                return Ok(new { success = true, message = "Se ha actualizado exitosamente" });
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


    }
}