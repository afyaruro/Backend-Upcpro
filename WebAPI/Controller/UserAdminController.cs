
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

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/user-admin")]
    public class UserAdminController : ControllerBase
    {
        private readonly UserService _userService;



        public UserAdminController(UserService service)
        {
            _userService = service;
        }


        [Authorize]
        [HttpPost("created-admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] CreateInputUserCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }

                var response = await _userService.CreateAdmin(dto);
                if (response == null)
                    return BadRequest("Error al crear");

                return CreatedAtAction(nameof(CreateAdmin), new { success = true, data = response, message = "Programa creado", });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPost("created-creator")]
        public async Task<IActionResult> CreateCreator([FromBody] CreateInputUserCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }


                var response = await _userService.CreateAdmin(dto);

                if (response == null)
                    return BadRequest("Error al crear");

                return CreatedAtAction(nameof(CreateCreator), new { success = true, data = response, message = "Programa creado", });
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
                return StatusCode((int)HttpStatusCode.InternalServerError,
                                  new { success = false, message = "Ocurrió un error inesperado." });
            }
        }

        [Authorize]
        [HttpPost("get-all")]
        public async Task<IActionResult> GetAll([FromBody] GetAllPageUserInputCommand dto)
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
                }


                var response = await _userService.GetAllPage(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

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
        [HttpPut("update-for-admin")]
        public async Task<IActionResult> Update([FromBody] UpdateUserForAdminCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
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
        [HttpPut("update-mail-for-admin")]
        public async Task<IActionResult> UpdateMail([FromBody] UpdateUserMailForAdminCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
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
        [HttpPut("update-password-for-admin")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordForAdminCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es válido" });
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound(new { success = false, message = "No existe el usuario" });
                }

                if (!await _userService.IsUserType("admin", userId!))
                {
                    return Unauthorized(new { success = false, message = "No está autorizado para esta acción" });
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

    }
}