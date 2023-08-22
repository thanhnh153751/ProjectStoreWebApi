using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementAPl.ViewModels;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    //[Authorize(Roles = "User")]
    public class CustomerController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/api");
        private readonly HttpClient _httpClient;

        public CustomerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [HttpGet]
        public IActionResult CustomerDetail()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult Index(int id)
        //{
        //    try
        //    {
        //        UserViewModel model = new UserViewModel();
        //        HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Members/GetMember/" + id).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            string data = responseMessage.Content.ReadAsStringAsync().Result;
        //            model = JsonConvert.DeserializeObject<UserViewModel>(data);
        //        }
        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["errorMessage"] = e.Message;
        //        return View();
        //    }

        //}

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    try
        //    {
        //        UserViewModel model = new UserViewModel();
        //        HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Members/GetMember/" + id).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            string data = responseMessage.Content.ReadAsStringAsync().Result;
        //            model = JsonConvert.DeserializeObject<UserViewModel>(data);
        //        }
        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["errorMessage"] = e.Message;
        //        return View();
        //    }

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(UserViewModel user)
        //{
        //    try
        //    {
        //        string data = JsonConvert.SerializeObject(user);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //        HttpResponseMessage responseMessage = _httpClient.PutAsync(baseAddress + "/Members/UpdateMember/" + user.user_id, content).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            TempData["successMessage"] = "Member details updated.";
        //            return RedirectToAction("Index", new { id = user.user_id });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["errorMessage"] = e.Message;
        //        return View();
        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult GetMyOrder(int id)
        //{
        //    try
        //    {
        //        List<OrderViewModel> orders = new List<OrderViewModel>();
        //        HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Orders/FindOrdersByMenberId/" + id).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            string data = responseMessage.Content.ReadAsStringAsync().Result;
        //            orders = JsonConvert.DeserializeObject<List<OrderViewModel>>(data);
        //        }
        //        return View(orders);
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["errorMessage"] = e.Message;
        //        return View();
        //    }
        //}

        //[HttpGet]
        //public IActionResult MyOrderDetails(int id)
        //{
        //    try
        //    {
        //        List<OrderDetailViewModel> model = new List<OrderDetailViewModel>();
        //        HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/OrderDetails/GetAllOrdersDetailByOrderId/" + id).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            string data = responseMessage.Content.ReadAsStringAsync().Result;
        //            model = JsonConvert.DeserializeObject<List<OrderDetailViewModel>>(data);
        //        }
        //        return View(model);
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["errorMessage"] = e.Message;
        //        return View();
        //    }
        //}

    }
}
