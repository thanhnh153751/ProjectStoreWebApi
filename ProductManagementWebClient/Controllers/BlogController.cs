using eBookStore.ResponseModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagementAPl.ViewModels;
using System.Net;
using System.Text;

namespace ProductManagementWebClient.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        Uri baseAddressApi = new Uri("https://localhost:7012/api");
        Uri baseAddressOdata = new Uri("https://localhost:7012/odata");
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogController(HttpClient httpClient, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult BlogManager()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]    
        public IActionResult Create(BlogModel moddel)
        {
            try
            {
                string data = JsonConvert.SerializeObject(moddel);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(baseAddressApi + "/Blogs/postBlog", content).Result;
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("BlogManager");
                }
                else if (httpResponseMessage.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    dynamic errorResponse = JsonConvert.DeserializeObject(errorContent);
                    var errorsTitle = errorResponse.title;
                    ViewBag.ErrorMessage = "An error occurred: " + errorsTitle;
                    return View();
                }
                else
                {
                    TempData["errorMessage"] = "An error occurred while processing the request.";

                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["errorMessage"] = e.Message;
                return View();
            }
            
        }

        [HttpPost]
        public IActionResult UploadImage(List<IFormFile> files)
        {
            try
            {
                var filepath = "";
                foreach(IFormFile photo in Request.Form.Files)
                {
                    string serverMapPath = Path.Combine(_webHostEnvironment.WebRootPath, "images_blog", photo.FileName);
                    using(var stream = new FileStream(serverMapPath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                    filepath = "https://localhost:7141/" + "images_blog/" + photo.FileName;
                }
                return Json(new {url = filepath});
            }
            catch (Exception e)
            {
                // Handle any exceptions that may occur during the upload.
                return Json(new { uploaded = 0, error = e.Message });
            }

            //return Json(new { uploaded = 0, error = "No file uploaded." });
        }


    }
}
