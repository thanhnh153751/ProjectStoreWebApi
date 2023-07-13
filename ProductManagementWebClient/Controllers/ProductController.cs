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
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7012/odata");
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(HttpClient httpClient, IWebHostEnvironment webHostEnvironment)
        {
            _httpClient = httpClient;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult ListProductManager()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Generate a unique file name or use the original file name
                string fileName = imageFile.FileName;

                // Get the path to the wwwroot/images directory
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Create the full path for saving the image file
                string filePath = Path.Combine(imagePath, fileName);

                // Save the image file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                // Return the file name or any other response if needed
                return Ok(fileName);
            }

            // Return an error response if the file is not provided
            return BadRequest("Image file not found.");
        }


    }
}
