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
    public class OrderController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/api");
        private readonly HttpClient _httpClient;

        public OrderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult ListShoppingCart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllHistoryOrderByCumtomerId()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllHistoryOrderPending()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllHistoryOrderApproved()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            try
            {
                var id = HttpContext.Session.GetString("idMember");
                
                UserViewModel model = new UserViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Customers/GetMember/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UserViewModel>(data);
                    if(string.IsNullOrEmpty(model.address) || string.IsNullOrEmpty(model.phone))
                    {
                        return Redirect("/Customer/CustomerDetail");
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Redirect("/Order/ListShoppingCart");
            }
            
        }

    }
}
