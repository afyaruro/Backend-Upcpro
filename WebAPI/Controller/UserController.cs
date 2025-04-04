using System.Net;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Jwt;
using Application.Service.User;
using Application.Service.User.Commands.UserCreate;
using Application.Service.User.Commands.UserUpdate;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;



        public UserController(UserService service, JwtService jwtService)
        {
            _userService = service;
            _jwtService = jwtService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateInputUserCommand dto)
        {

            try
            {
                var response = await _userService.Create(dto);

                if (response == null)
                    return BadRequest("Error al crear");

                return CreatedAtAction(nameof(Create), new { success = true, data = response, message = "Usuario creado", });
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
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand dto)
        {

            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest("El usuario no es valido");
                }


                if (!await _userService.ExistById(userId!))
                {
                    return NotFound("No Existe el usuario");
                }

                var response = await _userService.Update(dto, userId!);
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
        [HttpPut("update-mail")]
        public async Task<IActionResult> UpdateMail([FromBody] UpdateUserMailCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest("El usuario no es valido");
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound("No Existe el usuario");
                }

                var response = await _userService.UpdateMail(dto, userId);
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
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;

                if (!IsValidObjectId.IsValid(userId!))
                {
                    return BadRequest("El usuario no es valido");
                }

                if (!await _userService.ExistById(userId!))
                {
                    return NotFound("No Existe el usuario");
                }

                var response = await _userService.UpdatePassword(dto, userId!);
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