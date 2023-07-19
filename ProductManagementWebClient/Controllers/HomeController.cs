using eBookStore.ResponseModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProductManagementWebClient.Models;
using ProjectManagementAPl.Models;
using ProjectManagementAPl.ViewModels;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Uri baseAddress = new Uri("https://localhost:7012/");
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexMain()
        {
            var allCategory = getAllCategory();
            ViewBag.AllCategory = allCategory;
            return View();
        }

        [HttpGet]
        public IActionResult IndexMain2(int categoryId)
        {
            ViewBag.categoryId = categoryId;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public List<CategoryModelApi> getAllCategory()
        {           
            HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "odata/Categorys").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<ODataResponse<CategoryModelApi>>(data);
                var list = responseObject.Value;
                return list;
            }
            return new List<CategoryModelApi>();
        }

        [HttpPost]
        public async Task<IActionResult> RegiterUser(UserRegiter user)
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMessage"] = "Invalid data. Please correct the errors.";
                return View("Index");
            }
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("https://localhost:7012/api/Regiter", content).Result;
            
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Regiter Success.";               
                return RedirectToAction("Index");
            }
            else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                string errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
                TempData["errorMessage"] = errorMessage;
            }
            else
            {
                TempData["errorMessage"] = "An error occurred while registering the user.";
            }
            return View("Index");

        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserLogin user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("https://localhost:7012/api/Login", content).Result;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Login Success.";
                string token = httpResponseMessage.Content.ReadAsStringAsync().Result;

                AssignRoles(token);
                deCodeJwt(token);
                HttpContext.Session.SetString("token", token);
                return RedirectToAction("Index");
            }
            TempData["errorMessage"] = "Incorrent Userld or Password!";
            return View("Index");

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
            return Redirect("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}