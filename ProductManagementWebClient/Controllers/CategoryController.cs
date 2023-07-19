using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementAPl.ViewModels;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/odata");
        private readonly HttpClient _httpClient;

        public CategoryController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult ListCategoryManager()
        {
            return View();
        }
    }
}
