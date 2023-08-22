using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using ProductManagementWebClient.Controllers;
using ProductManagementWebClient.Middleware;
using System.Net.Sockets;
using System.Text;

namespace ProductManagementWebClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            
            builder.Services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromMinutes(20);
                op.Cookie.HttpOnly = true;
                op.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            

            //httpClient for middleware
            builder.Services.AddScoped<HttpClient>();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            

            
            app.Use(async (context, next) =>
            {
                await new SetTokenMiddleware(next).InvokeAsync(context);
            });
            app.UseMiddleware<ViewDataMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=IndexMain}/{id?}");

            app.MapControllerRoute(
            name: "customRoute",
            pattern: "{controller=Home}/{action=IndexMain2}/{id}/{key}");

            app.Run();
        }
    }
}