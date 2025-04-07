
using Application.Common.Exceptions;
using Application.Jwt;
using Application.Service.User;
using Application.Service.User.Commands.UserCreate;
using Application.Service.User.Commands.UserLogin;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controller.Base;

namespace WebAPI.Controller
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ApiControllerBase
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
                return HandleValidationException(ex);
            }


            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefrehsToken([FromBody] UserAuthInputCommand command)
        {
            try
            {

                await CheckAccess(userId: command.UserId!, userService: _userService, typeUser: "student");

                var token = _jwtService.generateToken(command.UserId);
                return Ok(new { success = true, token = token });


            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}