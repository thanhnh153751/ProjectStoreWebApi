using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductManagementWebClient.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SetTokenMiddleware
    {
        private readonly RequestDelegate _next;
        Uri baseAddress = new Uri("https://localhost:7012/api");

        public SetTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            
            // Use the access token to call a protected web API.

            var accessToken = httpContext.Session.GetString("token");

            if (!string.IsNullOrEmpty(accessToken))
            {
                var httpClient = httpContext.RequestServices.GetService<HttpClient>();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ViewDataMiddlewareExtensions
    {
        public static IApplicationBuilder UseViewDataMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetTokenMiddleware>();
        }
    }
}
