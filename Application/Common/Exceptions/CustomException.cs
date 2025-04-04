
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Exceptions;

public class CustomException : Exception
{
    public int StatusCode { get; set; }

    public CustomException() : base() { }

    public CustomException(string message, int statusCode) : base(message)
    {
        this.StatusCode = statusCode;
    }

}