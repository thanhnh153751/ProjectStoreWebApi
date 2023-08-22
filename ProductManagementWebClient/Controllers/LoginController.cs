using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using ProjectManagementAPl.Models;
using ProjectManagementAPl.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    //[Authorize(Roles = "User")]
    public class LoginController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/api");
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpGet]
        public IActionResult LoginRegister()
        {
            //try
            //{
            //    UserViewModel model = new UserViewModel();
            //    HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Members/GetMember/" + id).Result;
            //    if (responseMessage.IsSuccessStatusCode)
            //    {
            //        string data = responseMessage.Content.ReadAsStringAsync().Result;
            //        model = JsonConvert.DeserializeObject<UserViewModel>(data);
            //    }
            //    return View(model);
            //}
            //catch (Exception e)
            //{
            //    TempData["errorMessage"] = e.Message;
            //    return View();
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoLogin(UserLogin moddel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(moddel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(baseAddress + "/LoginApi/Login", content).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string token = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    AssignRoles(token);
                    deCodeJwt(token);
                    HttpContext.Session.SetString("token", token);
                    return Redirect("/Home/IndexMain");
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                {
                    //string errorContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    //dynamic errorResponse = JsonConvert.DeserializeObject(errorContent);
                    //var errorsTitle = errorResponse.title;
                    var errorsTitle = "Incorrect email and password!";
                    TempData["errorMessage"] = errorsTitle;
                    return RedirectToAction("LoginRegister");
                }
                else
                {
                    TempData["errorMessage"] = "An error occurred while processing the request.";
                    return RedirectToAction("LoginRegister");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return RedirectToAction("LoginRegister");
            }
            
        }

        public void AssignRoles(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);
            var nameRole = decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
            var emailUser = decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            if (nameRole!.Value == "1")
            {

                //Create the identity for the user  
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, emailUser!.Value),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }
            else
            {
                //Create the identity for the user  
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, emailUser!.Value),
                    new Claim(ClaimTypes.Role, "User")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }
            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }
        }

        public void deCodeJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

            var nameIdentifierClaim = decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (nameIdentifierClaim != null)
            {
                string nameIdentifierValue = nameIdentifierClaim.Value;
                HttpContext.Session.SetString("idMember", nameIdentifierValue);
            }
            var emailIdentifierClaim = decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            if (emailIdentifierClaim != null)
            {
                string emailIdentifierValue = emailIdentifierClaim.Value;
                HttpContext.Session.SetString("emailUser", emailIdentifierValue);
            }
            var roleId = decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);
            if (roleId != null)
            {
                string role = roleId.Value;
                HttpContext.Session.SetString("roleId", role);
            }
        }

        public IActionResult LogOut()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();//clear token
            return Redirect("/Home/IndexMain");

        }

    }
}
