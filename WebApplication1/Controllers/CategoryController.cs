using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable <Category> categoryObjectList = _db.Categories;
            return View(categoryObjectList);
        }

        //Get Action
        public IActionResult Create()
        {
            return View();
        }

        //Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get Action
        public IActionResult Edit(int? id)
        {
            if (id==null || id ==0)
            {
                return NotFound();
            }
            var categorybyID = _db.Categories.Find(id);

            if (categorybyID == null)
            {
                return NotFound();
            }
            return View(categorybyID);
        }

        //Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categorybyID = _db.Categories.Find(id);

            if (categorybyID == null)
            {
                return NotFound();
            }
            return View(categorybyID);
        }

        //Post Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
             return RedirectToAction("Index");
        }
    }
}
