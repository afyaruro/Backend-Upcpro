
using Application.Common.Exceptions;
using Application.Service.Certificado;
using Application.Service.Certificado.Commands.CertificadoCreate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/certificado")]
    public class CertificadoController : ApiControllerBase
    {
        private readonly UserService _userService;
        private readonly CertificadoService _certificadoService;

        public CertificadoController(UserService userService, CertificadoService certificadoService)
        {
            _userService = userService;
            _certificadoService = certificadoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CrearCertificado([FromBody] CertificadoCreateInputCommand dto)
        {
            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _certificadoService.Create(dto, userId!);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se creo el certificado" });
                }


                return Created(nameof(CrearCertificado), new { success = true, message = "Certificado creado correctamente" });
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

        [HttpGet("GetAllByUser")]
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

                var response = await _certificadoService.GetAll(userId!);

                if (response == null || response.Count == 0)
                {
                    return NotFound(new { success = false, message = "No se Encontraron certificados para este usuario" });
                }

                return Ok(new { success = true, message = "Certificados del usuario obtenidos", data = response });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


    }
}