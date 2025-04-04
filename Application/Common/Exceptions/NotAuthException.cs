using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class NotAuthException : CustomException
    {
        public NotAuthException(string message)
            : base(message, 401)
        {
        }
    }
}