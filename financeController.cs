

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectnew.Data;
using projectnew.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading.Tasks;

namespace projectnew.Controllers
{
    public class financeController : Controller
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyprojectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public financeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public IActionResult Addfin(finance f)
        {
            if (ModelState.IsValid)
            {
                IRepository<finance> pr = new GenericRepository<finance>(connectionString);
                pr.Add(f);
                return Json(new { success = true, message = "Finance record added successfully!" });
            }
            return Json(new { success = false, message = "Invalid finance data!" });
        }

        //[HttpPost]
        //public IActionResult Addfin(finance f)
        //{



        //    if (ModelState.IsValid)
        //    {
        //        IRepository<finance> pr = new GenericRepository<finance>(connectionString);
        //        pr.Add(f);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(f);
        //}




    

        private void SaveProfilePicturePath(string fileName)
        {
          
            HttpContext.Session.SetString("ProfilePicture", fileName);
        }

        [HttpGet]
        public IActionResult Addfin()
        {
            var profilePicture = HttpContext.Session.GetString("ProfilePicture") ?? "pro.jpg";
            ViewData["ProfilePicture"] = profilePicture;

            return View();
        }

        public IActionResult Index()
        {
            IRepository<finance> financeRepository = new GenericRepository<finance>(connectionString);
            IEnumerable<finance> financeItems = financeRepository.GetAll();

            int totalFinanceRecords = financeItems.Count();
            ViewData["TotalFinanceRecords"] = totalFinanceRecords;

            return View(financeItems);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            IRepository<finance> pr = new GenericRepository<finance>(connectionString);
            var financeItem = pr.FindById(id);
            if (financeItem == null)
            {
                return NotFound();
            }
            return View(financeItem);
        }

        //[HttpPost]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    IRepository<finance> pr = new GenericRepository<finance>(connectionString);
        //    pr.DeleteById(id);
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            IRepository<finance> pr = new GenericRepository<finance>(connectionString);
            var financeItem = pr.FindById(id);
            if (financeItem != null)
            {
                pr.DeleteById(id);
                return Json(new { success = true, message = "Finance record deleted successfully!" });
            }
            return Json(new { success = false, message = "Finance record not found!" });
        }



        [HttpGet]
        public IActionResult GetFinance(int id)
        {
            IRepository<finance> pr = new GenericRepository<finance>(connectionString);
            var financeItem = pr.FindById(id);
            if (financeItem == null)
            {
                return Json(new { success = false, message = "Finance record not found!" });
            }
            return Json(new { success = true, finance = financeItem });
        }

        [HttpGet]
        public IActionResult GetAllFinance()
        {
            IRepository<finance> financeRepository = new GenericRepository<finance>(connectionString);
            IEnumerable<finance> financeItems = financeRepository.GetAll();

            if (financeItems != null && financeItems.Any())
            {
                return Json(new { success = true, financeList = financeItems });
            }

            return Json(new { success = false, message = "No finance records found" });
        }
    





        [HttpGet]
        public IActionResult Edit(int id)
        {
            IRepository<finance> pr = new GenericRepository<finance>(connectionString);
            var financeItem = pr.FindById(id);
            if (financeItem == null)
            {
                return NotFound();
            }
            return View(financeItem);
        }


        [HttpPost]
        public IActionResult Edit(finance finance)
        {
            if (ModelState.IsValid)
            {
                

                return Json(new { success = true, message = "Record updated successfully." });
            }
            return Json(new { success = false, message = "Failed to update record." });
        }


        //[HttpPost]
        //public IActionResult Edit(finance f)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IRepository<finance> pr = new GenericRepository<finance>(connectionString);
        //        pr.UpdateById(f.Id, f);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(f);
        //}
    }
}

























//public class financeController : Controller
//{
//    private readonly financerepository _fin;
//    public financeController(financerepository fin)
//    {
//        _fin = fin;
//    }



//    //public IActionResult Index()
//    //{
//    //    var finances = _fin.Addfin;

//    //    return View();
//    //}

//    public IActionResult Index()
//    {
//        var finances = _fin.GetFinances();  // Assuming you have a method to fetch finance data
//        return View(finances);
//    }

//    //[HttpGet]
//    //public ViewResult Add()
//    //{
//    //    return View();
//    //}
//    [HttpGet]
//    public IActionResult Addfin()
//    {
//        return View();
//    }


//    [HttpPost]
//    public IActionResult Addfin(finance f)
//    {
//        if (ModelState.IsValid)
//        {
//            _fin.Addfin(f);
//            return RedirectToAction(nameof(Index));
//        }
//        else
//        {
//            // If ModelState is not valid, return to the Add view with the model
//            return View("Addfin", f); // Return the "Add" view with the model
//        }
//    }

//    //[HttpGet]
//    //public IActionResult AddfinForm() // Renamed to AddfinForm
//    //{
//    //    return View(new finance { ProfilePicturePath = "images" });
//    //}

//    [HttpGet]
//    public IActionResult Edit(int id)
//    {
//        var financeItem = _fin.GetFinances().FirstOrDefault(f => f.Id == id);
//        if (financeItem == null)
//        {
//            return NotFound();
//        }
//        return View(financeItem);
//    }

//    [HttpPost]
//    public IActionResult Edit(finance updatedFinance)
//    {
//        if (ModelState.IsValid)
//        {
//            _fin.UpdateFinance(updatedFinance);
//            return RedirectToAction(nameof(Index));
//        }
//        return View(updatedFinance);
//    }

//    [HttpGet]
//    public IActionResult Delete(int id)
//    {
//        var financeItem = _fin.GetFinances().FirstOrDefault(f => f.Id == id);
//        if (financeItem == null)
//        {
//            return NotFound();
//        }
//        return View(financeItem);
//    }

//    [HttpPost]
//    public IActionResult DeleteConfirmed(int id)
//    {
//        var financeItem = _fin.GetFinances().FirstOrDefault(f => f.Id == id);
//        if (financeItem == null)
//        {
//            return NotFound();
//        }

//        _fin.DeleteFinance(financeItem);
//        return RedirectToAction(nameof(Index));
//    }
//}


//         //[HttpPost]
//    //    public IActionResult Addfin(finance f, IFormFile profilePicture)
//    //    {
//    //        if (ModelState.IsValid)
//    //        {
//    //            if (profilePicture != null && profilePicture.Length > 0)
//    //            {
//    //                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
//    //                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profilePicture.FileName);
//    //                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

////                using (var stream = new FileStream(filePath, FileMode.Create))
////                {
////                    profilePicture.CopyTo(stream);
////                }

////                f.ProfilePicturePath = "images" + uniqueFileName;
////            }
////            else
////            {
////                f.ProfilePicturePath = "images";
////            }

////            _fin.Addfin(f);
////            return RedirectToAction(nameof(Index));
////        }
////        else
////        {
////            if (string.IsNullOrEmpty(f.ProfilePicturePath))
////            {
////                f.ProfilePicturePath = "images";
////            }
////            return View(f);
////        }
////    }



//    //    [HttpPost]
//    //    public IActionResult Addfin(finance f, IFormFile profilePicture)
//    //    {
//    //        if (ModelState.IsValid)
//    //        {
//    //            if (profilePicture != null && profilePicture.Length > 0)
//    //            {
//    //                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
//    //                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profilePicture.FileName);
//    //                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

////                using (var stream = new FileStream(filePath, FileMode.Create))
////                {
////                    profilePicture.CopyTo(stream);
////                }

////                f.ProfilePicturePath = "images" + uniqueFileName;
////            }
////            else
////            {
////                f.ProfilePicturePath = "images";
////            }

////            _fin.Addfin(f);
////            return RedirectToAction(nameof(Index));
////        }
////        else
////        {
////            if (string.IsNullOrEmpty(f.ProfilePicturePath))
////            {
////                f.ProfilePicturePath = "images";
////            }
////            return View(f);
////        }
////    }
////}





