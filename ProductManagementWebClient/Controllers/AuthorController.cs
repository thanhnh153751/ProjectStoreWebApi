using eBookStore.ResponseModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementAPl.ViewModels;
using System.Net;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AuthorController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/odata");
        private readonly HttpClient _httpClient;

        public AuthorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<AuthorViewModel> InitListAuthor()
        {
            List<AuthorViewModel> list = new List<AuthorViewModel>();
            HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Authors").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<ODataResponse<AuthorViewModel>>(data);
                list = responseObject.Value;
            }
            return list;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.authorList = InitListAuthor();
            ViewBag.authorModel = null;
            if (HttpContext.Session.GetString("authorModel") != null)
            {
                ViewBag.authorModel = JsonConvert.DeserializeObject<AuthorViewModel>(HttpContext.Session.GetString("authorModel"));
            }


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthorViewModel moddel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(moddel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(baseAddress + "/Authors", content).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Created!";
                    HttpContext.Session.SetString("authorModel", JsonConvert.SerializeObject(moddel));
                    return RedirectToAction("Index");
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    dynamic errorResponse = JsonConvert.DeserializeObject(errorContent);
                    var errorsTitle = errorResponse.title;
                    TempData["errorMessage"] = errorsTitle;
                }
                else
                {
                    TempData["errorMessage"] = "An error occurred while processing the request.";
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearModel()
        {
            HttpContext.Session.Remove("authorModel");
            return Redirect("Index");
        }
      

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                AuthorViewModel model = new AuthorViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Authors(" + id + ")").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ODataResponse<AuthorViewModel>>(data);
                    var list = responseObject.Value;
                    model = list.First();
                }
                return View(model);
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _httpClient.PutAsync(baseAddress + "/Authors(" + model.author_id + ")", content).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "details updated.";
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
        public IActionResult Delete(int id)
        {
            try
            {
                AuthorViewModel model = new AuthorViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Authors(" + id + ")").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ODataResponse<AuthorViewModel>>(data);
                    var list = responseObject.Value;
                    model = list.First();
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
                HttpResponseMessage response = _httpClient.DeleteAsync(baseAddress + "/Authors(" + id + ")").Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "details deleted.";
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
