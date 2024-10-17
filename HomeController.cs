using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Models;
using projectnew.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace projectnew.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadFile(UploadViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
        //        var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
        //        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await model.File.CopyToAsync(fileStream);
        //        }

        //        // Store the profile picture path in the session
        //        HttpContext.Session.SetString("ProfilePicture", uniqueFileName);

        //        return RedirectToAction("Addfin", "Finance");
        //    }

        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> UploadFile(UploadViewModel model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                try
                {
                    var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    // Store the profile picture path in the session
                    HttpContext.Session.SetString("ProfilePicture", uniqueFileName);

                    return Json(new { success = true, message = "File uploaded successfully!" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error occurred while uploading file: " + ex.Message });
                }
            }

            return Json(new { success = false, message = "Please select a valid file." });
        }




        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}


        //[AllowAnonymous]
        //[Authorize]
        //[Authorize(Policy = "BusinessHoursOnly")]
        //[Authorize(Policy = "departmentaccess")]

        public IActionResult Myproject()
        {
            string? data = string.Empty;

            if (!HttpContext.Request.Cookies.ContainsKey("first_visit"))
            {

                HttpContext.Response.Cookies.Append("first_visit", DateTime.Now.ToString());
                data = "You are visiting first time";
            }

            else
            {
                data = HttpContext.Request.Cookies["first_visit"];
                data = "Welcome back! Last visited: " + data;
            }

            return View((object)data);

     
            

        }


       



        public IActionResult Profile()
            {
            return View();
            }
        public IActionResult News()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
}
}