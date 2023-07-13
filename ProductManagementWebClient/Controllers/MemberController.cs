using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementAPl.ViewModels;
using System.Data;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemberController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/api");
        private readonly HttpClient _httpClient;

        public MemberController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserViewModel> list = new List<UserViewModel>();
            HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Members/GetMembers").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                list = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(list);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel moddel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(moddel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(baseAddress + "/Members/PostMember", content).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Member Created.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                UserViewModel model = new UserViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Members/GetMember/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UserViewModel>(data);
                }
                return View(model);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }

        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(UserViewModel product)
        //{
        //    try
        //    {
        //        string data = JsonConvert.SerializeObject(product);
        //        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //        HttpResponseMessage responseMessage = _httpClient.PutAsync(baseAddress + "/Members/UpdateMember/" + product.MemberId, content).Result;
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            TempData["successMessage"] = "Member details updated.";
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        TempData["errorMessage"] = e.Message;
        //        return View();
        //    }
        //    return View();
        //}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                UserViewModel model = new UserViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Members/GetMember/" + id).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UserViewModel>(data);
                }
                return View(model);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(baseAddress + "/Members/DeleteMember/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Member details deleted.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            return View();
        }

    }
}
