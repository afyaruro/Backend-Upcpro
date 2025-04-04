using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Base.CommandBase.User;
using Application.Base.Validate;
using Application.Common.Exceptions;
using Application.Jwt;
using Application.Service.User;
using Application.Service.User.Commands.UserLogin;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService, UserService userService)
        {

            _jwtService = jwtService;
            _userService = userService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand dto)
        {
            try
            {
                var response = await _userService.Login(dto);

                if (response.isError)
                    return BadRequest(new { success = false, message = response.message });

                var token = _jwtService.generateToken(response.entity!.Id);


                return Ok(new { success = true, message = response.message, token = token, data = response.entity });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (NotAuthException ex)
            {
                return Unauthorized(new { success = false, message = ex.Message });
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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefrehsToken([FromBody] UserIdCommand command)
        {
            try
            {
                if (!IsValidObjectId.IsValid(command.UserId!))
                {
                    return BadRequest(new { success = false, message = "El usuario no es valido." });
                }


                if (await _userService.ExistById(command.UserId))
                {
                    Console.WriteLine("Existe");
                    Console.WriteLine(await _userService.IsUserType("student", command.UserId));
                    if (await _userService.IsUserType("student", command.UserId))
                    {
                        Console.WriteLine("No es Admin");
                        var response = _jwtService.generateToken(command.UserId);
                        return Ok(new { success = true, token = response });
                    }
                    Console.WriteLine(" es Admin");
                }

                return Unauthorized(new { success = false, message = "No tienes permisos." });


            }
            catch (Exception)
            {
                return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
            }
        }
    }
}