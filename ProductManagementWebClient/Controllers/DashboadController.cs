using BusinessObjects.Entities;
using eBookStore.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProjectManagementAPl.ViewModels;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    //[Authorize(Roles = "User")]
    public class DashboadController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/api");
        private readonly HttpClient _httpClient;

        public DashboadController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult ShowDashboad()
        {
            return View();
        }      

    }
}
