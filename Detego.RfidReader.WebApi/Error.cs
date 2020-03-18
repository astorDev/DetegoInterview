using System;
using System.Net;
using Microsoft.Extensions.Hosting;

namespace Detego.RfidReader.WebApi
{
    public class Error
    {
        public HttpStatusCode Code { get; set; }
        public string Reason { get; set; }
        
        public string Details { get; set; }

        public static Error Unknown => new Error
        {
            Code = HttpStatusCode.InternalServerError,
            Reason = "Unknown"
        };

        public static Error Interpret(Exception exception, IHostEnvironment environment)
        {
            var error = interpret(exception);
            if (environment.IsDevelopment())
            {
                error.Details = exception.ToString();
            }

            return error;
        }

        private static Error interpret(Exception exception)
        {
            return Unknown;
        }
    }
}