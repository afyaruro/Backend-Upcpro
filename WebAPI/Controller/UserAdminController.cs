
using System.Net;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Jwt;
using Application.Service.User;
using Application.Service.User.Commands.UserCreate;
using Application.Service.User.Commands.UserGetAllPage;
using Application.Service.User.Commands.UserUpdate;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/user-admin")]
    public class UserAdminController : ApiControllerBase
    {
        private readonly UserService _userService;



        public UserAdminController(UserService service)
        {
            _userService = service;
        }


        [Authorize]
        [HttpPost("created-admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] UserCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }


                var response = await _userService.CreateForAdmin(dto, "admin");
                if (response == null)
                    return BadRequest("Error al crear");

                return CreatedAtAction(nameof(CreateAdmin), new { success = true, data = response, message = "Programa creado", });
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
        [HttpPost("created-creator")]
        public async Task<IActionResult> CreateCreator([FromBody] UserCreateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.CreateForAdmin(dto, "creator");

                if (response == null)
                    return BadRequest("Error al crear");

                return CreatedAtAction(nameof(CreateCreator), new { success = true, data = response, message = "Usuario administrador creado", });
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
        public async Task<IActionResult> GetAll([FromBody] UserGetAllPageInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.GetAllPage(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

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
        [HttpPut("update-for-admin")]
        public async Task<IActionResult> Update([FromBody] UserForAdminUpdateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.UpdateForAdmin(dto);
                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo el usuario" });
                }

                return Ok(new { success = true, message = "El usuario se ha actualizado exitosamente" });
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
        [HttpPut("update-mail-for-admin")]
        public async Task<IActionResult> UpdateMail([FromBody] UserMailUpdateForAdminInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.UpdateMailForAdmin(dto);

                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo el correo del usuario" });
                }

                return Ok(new { success = true, message = "El correo se ha actualizado exitosamente" });
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
        [HttpPut("update-password-for-admin")]
        public async Task<IActionResult> UpdatePassword([FromBody] UserPasswordForAdminUpdateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "admin");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.UpdatePasswordForAdmin(dto);
                if (!response)
                {
                    return BadRequest(new { success = false, message = "No se actualizo la contraseña del usuario" });
                }

                return Ok(new { success = true, message = "La contraseña se ha actualizado exitosamente" });
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