using Microsoft.AspNetCore.Mvc;
using ShoppingHub.BLL.Services.Abstraction;
//using ShoppingHub.BLL.Services.Implementation;
//using ShoppingHub.DAL.Entities;

//namespace ShoppingHub.PL.Controllers.Product
//{

//namespace ShoppingHub.PL.Controllers
//    {
//        public class CategoryController : Controller
//        {
//            private readonly ICategoryService categoryService;

//            public CategoryController(ICategoryService _categoryService)
//            {
//                categoryService = _categoryService;
//            }

//            // ✅ List all categories
//            public IActionResult Index()
//            {
//                var categories = categoryService.GetAllCategories();
//                return View(categories);
//            }

//            // ✅ View details
//            public IActionResult Details(int id)
//            {
//                var category = categoryService.GetCategoryById(id);
//                if (category == null)
//                {
//                    return NotFound();
//                }
//                return View(category);
//            }

//            // ✅ Add category
//            [HttpGet]
//            public IActionResult AddCategory()
//            {
//                return View();
//            }

//            [HttpPost]
//            public IActionResult AddCategory(Category category)
//            {
//                if (!ModelState.IsValid)
//                    return View(category);

//                categoryService.AddCategory(category);
//                return RedirectToAction("Index");
//            }

//            // ✅ Edit category
//            [HttpGet]
//            public IActionResult Edit(int id)
//            {
//                var category = categoryService.GetCategoryById(id);
//                if (category == null)
//                {
//                    return NotFound();
//                }
//                return View(category);
//            }

//            [HttpPost]
//            public IActionResult Edit(Category category)
//            {
//                if (!ModelState.IsValid)
//                    return View(category);

//                categoryService.UpdateCategory(category);
//                return RedirectToAction("Index");
//            }

//            // ✅ Delete category
//            [HttpGet]
//            public IActionResult Delete(int id)
//            {
//                var category = categoryService.GetCategoryById(id);
//                if (category == null)
//                {
//                    return NotFound();
//                }
//                return View(category);
//            }

//            public IActionResult Remove()
//            {

//                return View();
//            }
//            [HttpPost]
//            public IActionResult Remove(int id)
//            {
//                if (!ModelState.IsValid)
//                    return Content("NOT FOUND");

//                categoryService.Removecategory(id);
//                return RedirectToAction("GetAllProducts");
//            }
//        }
//    }

//}

