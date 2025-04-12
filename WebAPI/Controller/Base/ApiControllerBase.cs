using Application.Base.Validate;
using Application.Service.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace WebAPI.Controller.Base
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected IActionResult HandleValidationException(ValidationException ex)
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

        protected async Task<IActionResult?> CheckAccess(string userId, UserService userService, string typeUser)
        {

            if (!await ValidUser.IsUserAccess(userId!, userService, typeUser))
            {
                return Unauthorized(new { success = false, message = "No cuentas con permisos para esta acción" });
            }

            return null;
        }

        protected async Task<IActionResult?> CheckAccessAdminStudent(string userId, UserService userService)
        {

            if (await ValidUser.IsUserAccess(userId!, userService, "student"))
            {
                return null;
            } else if(await ValidUser.IsUserAccess(userId!, userService, "admin"))
            {
                return null;
            }


            return Unauthorized(new { success = false, message = "No cuentas con permisos para esta acción" });

        }

        protected async Task<IActionResult?> CheckAccessAdminCreator(string userId, UserService userService)
        {

            if (await ValidUser.IsUserAccess(userId!, userService, "admin"))
            {
                return null;
            } else if(await ValidUser.IsUserAccess(userId!, userService, "creator"))
            {
                return null;
            }


            return Unauthorized(new { success = false, message = "No cuentas con permisos para esta acción" });

        }

        protected async Task<IActionResult?> CheckAccessAll(string userId, UserService userService)
        {

            if (!IsValidObjectId.IsValid(userId!))
            {
                return BadRequest("El usuario no es valido");
            }

            if (!await userService.ExistById(userId!))
            {
                return NotFound("No Existe el usuario");
            }

            return null;
        }



        protected IActionResult InternalServerError()
        {
            return StatusCode(500, new { success = false, message = "Ocurrió un error inesperado." });
        }
    }
}