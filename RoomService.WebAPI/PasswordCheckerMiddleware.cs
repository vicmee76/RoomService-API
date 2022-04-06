using System.Net;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RoomService.WebAPI
{
    public class PasswordCheckerMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "passwordKey";


        public PasswordCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Response.StatusCode = 403;
                return;
            }
            var apiKey = "passwordKey123456789";

            if (!apiKey.Equals(extractedApiKey))
            {
                context.Response.StatusCode = 403;
                return;
            }

            await _next(context);
        }
    }
}
