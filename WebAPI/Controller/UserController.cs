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
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ApiControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;



        public UserController(UserService service, JwtService jwtService)
        {
            _userService = service;
            _jwtService = jwtService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateInputCommand dto)
        {

            try
            {
                var response = await _userService.Create(dto);

                if (response == null)
                    return BadRequest("Error al crear");

                var token = _jwtService.generateToken(response.Id);
                return CreatedAtAction(nameof(Create), new { success = true, data = response, token = token, message = "Usuario creado", });
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
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateInputCommand dto)
        {

            try
            {

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAll(userId!, _userService);
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [HttpPut("update-mail")]
        public async Task<IActionResult> UpdateMail([FromBody] UserMailUpdateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAll(userId!, _userService);
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.UpdateMail(dto, userId!);
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
        [HttpPut("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UserPasswordUpdateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAll(userId!, _userService);
                if (resp != null)
                {
                    return resp;
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
                return HandleValidationException(ex);
            }

            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut("update-password-byte-mail")]
        public async Task<IActionResult> UpdatePasswordByMail([FromBody] UserPasswordByMailUpdateInputCommand dto)
        {

            try
            {
                var response = await _userService.UpdatePasswordByMail(dto);
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

        [Authorize]
        [HttpPut("update-puntaje")]
        public async Task<IActionResult> UpdatePuntaje([FromBody] UserPuntajeUpdateInputCommand dto)
        {

            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccessAll(userId!, _userService);
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.UpdatePuntaje(dto, userId!);
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


        [HttpPost("get-ranking")]
        public async Task<IActionResult> GetRanking()
        {
            try
            {
                var userId = HttpContext.User.FindFirst("uid")?.Value;
                var resp = await CheckAccess(userId: userId!, userService: _userService, "student");
                if (resp != null)
                {
                    return resp;
                }

                var response = await _userService.GetRanking(userId!);

                return Ok(new { success = true, message = "Ranking Obtenido", ranking = response });
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }




    }
}