using CarsWeb.Data;
using CarsWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarsWeb.Controllers
{
    public class CarCategoryController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CarCategoryController(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<CarCategory> categoryList = dbContext.CarCategories;
            return View(categoryList);
        }

        //create method for our vehicle category
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarCategory carCategoryObject)
        {
            if(carCategoryObject.Name == carCategoryObject.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder cannot match the Name");
            }
            if (ModelState.IsValid)
            {
                dbContext.CarCategories.Add(carCategoryObject);
                dbContext.SaveChanges();
                TempData["success"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            return View(carCategoryObject);
        }

        //Edit
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = dbContext.CarCategories.Find(id);
            //var categoryFromDBFirst = dbContext.CarCategories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDBSingle = dbContext.CarCategories.SingleOrDefault(u => u.Id == id);
            if(categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarCategory carCategoryObject)
        {
            if (carCategoryObject.Name == carCategoryObject.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "DisplayOrder cannot match the Name");
            }
            if (ModelState.IsValid)
            {
                dbContext.CarCategories.Update(carCategoryObject);
                dbContext.SaveChanges();
                TempData["success"] = "Category updated successfully!";
                return RedirectToAction("Index");
            }
            return View(carCategoryObject);
        }

        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDB = dbContext.CarCategories.Find(id);
            //var categoryFromDBFirst = dbContext.CarCategories.FirstOrDefault(u => u.Id == id);
            //var categoryFromDBSingle = dbContext.CarCategories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCategory(int? id)
        {
            var carCategoryObject = dbContext.CarCategories.Find(id);
            if(carCategoryObject == null)
            {
                return NotFound();
            }

            dbContext.CarCategories.Remove(carCategoryObject);
            dbContext.SaveChanges();
            TempData["success"] = "Category deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
