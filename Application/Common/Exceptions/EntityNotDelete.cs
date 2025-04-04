using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions
{
    public class EntityNotDelete : CustomException
    {
        public EntityNotDelete(string message)
            : base(message, 400)
        {
        }
    }
}