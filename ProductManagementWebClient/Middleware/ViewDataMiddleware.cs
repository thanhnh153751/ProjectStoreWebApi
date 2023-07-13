using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProductManagementWebClient.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ViewDataMiddleware
    {
        private readonly RequestDelegate _next;

        public ViewDataMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if(httpContext.Session.GetString("idMember") != null)
            {
                httpContext.Items["idMember"] = httpContext.Session.GetString("idMember");
            }
            if (httpContext.Session.GetString("roleId") != null)
            {
                httpContext.Items["roleId"] = httpContext.Session.GetString("roleId");
            }
            if (httpContext.Session.GetString("token") != null)
            {
                httpContext.Items["token"] = httpContext.Session.GetString("token");
            }

            await _next(httpContext);
        }
    }

}
