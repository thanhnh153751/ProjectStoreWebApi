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
    public class PublisherController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/odata");
        private readonly HttpClient _httpClient;

        public PublisherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public List<PublisherViewModel> InitListPublisher()
        {
            List<PublisherViewModel> list = new List<PublisherViewModel>();
            HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Publishers").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string data = responseMessage.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<ODataResponse<PublisherViewModel>>(data);
                list = responseObject.Value;
            }
            return list;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.publisherList = InitListPublisher();
            ViewBag.publisherModel = null;
            if (HttpContext.Session.GetString("publisherModel") != null)
            {
                ViewBag.publisherModel = JsonConvert.DeserializeObject<PublisherViewModel>(HttpContext.Session.GetString("publisherModel"));
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PublisherViewModel moddel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(moddel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(baseAddress + "/Publishers", content).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Created!";
                    HttpContext.Session.SetString("publisherModel", JsonConvert.SerializeObject(moddel));
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
            HttpContext.Session.Remove("publisherModel");
            return Redirect("Index");
        }
      

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                PublisherViewModel model = new PublisherViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Publishers(" + id + ")").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ODataResponse<PublisherViewModel>>(data);
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
        public IActionResult Edit(PublisherViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = _httpClient.PutAsync(baseAddress + "/Publishers(" + model.pub_id + ")", content).Result;
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
                PublisherViewModel model = new PublisherViewModel();
                HttpResponseMessage responseMessage = _httpClient.GetAsync(baseAddress + "/Publishers(" + id + ")").Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string data = responseMessage.Content.ReadAsStringAsync().Result;
                    var responseObject = JsonConvert.DeserializeObject<ODataResponse<PublisherViewModel>>(data);
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
                HttpResponseMessage response = _httpClient.DeleteAsync(baseAddress + "/Publishers(" + id + ")").Result;
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
