
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class EntityExistException : CustomException
    {
        public EntityExistException(string message)
            : base(message, 409)
        {
        }

    }
}