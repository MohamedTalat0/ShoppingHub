using Microsoft.AspNetCore.Mvc;
using ShoppingHub.BLL.ModelVm;
using ShoppingHub.BLL.Service.Implementaion;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Plugins;
using ShoppingHub.BLL.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using ShoppingHub.DAL.DataBase;



namespace ShoppingHub.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        public ProductController(IProductService _productservice ,ICategoryService _categoryService)
        {
            productService = _productservice;
            categoryService = _categoryService;
        }

        public IActionResult GetAllProducts(GetAllProductsVM productvm)
        {
            
            if (productvm == null)
            {
                productvm = new GetAllProductsVM
                {
                    PageNumber = 1,
                    PageSize = 10
                };
            }

            if (productvm.PageNumber <= 0) productvm.PageNumber = 1;
            if (productvm.PageSize <= 0) productvm.PageSize = 10;

            
            var (hasError, errorMessage, result) = productService.GetProducts(productvm);

            if (hasError || result == null)
            {
                ModelState.AddModelError("", errorMessage ?? "Something went wrong fetching products.");
                result = new GetAllProductsVM
                {
                    Products = new List<OutDisplayingProduct>(),
                    Categories = new List<Category>()
                };
            }

            
            result.Categories = categoryService.GetAllCategories() ?? new List<Category>();

            return View(result);
        }




        public IActionResult Details(int id)
        {
            var (hasError, errorMessage, product) = productService.GetProductDetails(id);

            if (hasError)
            {
            
                TempData["ErrorMessage"] = errorMessage;
                return RedirectToAction("GetAllProducts");

            }

            return View(product);
           
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var vm = new AddProductVM
            {
                Categories = categoryService.GetAllCategories()
            };

            return View(vm); 
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductVM vm)
        {

            if (!ModelState.IsValid)
            {
                //TempData["Error"] = "Something went wrong adding the product.";
                
                vm.Categories = categoryService.GetAllCategories();
                return View(vm);
            }




            var (isError, message) = productService.AddProduct(vm);

            if (isError)
            {
                //TempData["Error"] = message ?? "Something went wrong while adding the product.";

                vm.Categories = categoryService.GetAllCategories();
                return View(vm);
            }

            
            return RedirectToAction("GetAllProducts");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var vm = productService.GetProductForEdit(id);
            if (vm == null) return NotFound();

            vm.Categories = categoryService.GetAllCategories();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(EditProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categories = categoryService.GetAllCategories();
                TempData["Error"] = "Invalid input.";
                return View(vm);
            }

            var (isError, message) = productService.EditProduct(vm);

            if (isError)
            {
                vm.Categories = categoryService.GetAllCategories();
                TempData["Error"] = message ?? "Something went wrong while updating the product.";
                return View(vm);
            }

            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction("GetAllProducts");
        }




        [HttpGet]
        public IActionResult Remove(int id)
        {
            var vm = productService.GetProductByID(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Removeconfirmation(int id)
        {
            var (isError, message) = productService.RemoveProduct(id);

            if (isError)
            {
                TempData["Error"] = message ?? "Something went wrong while deleting the product.";
            }
            else
            {
                TempData["Success"] = "Product deleted successfully!";
            }

            return RedirectToAction("GetAllProducts");
        }




    }
}
