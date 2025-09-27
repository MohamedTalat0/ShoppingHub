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
using ShoppingHub.BLL.Helper;
using System.Security.Claims;
using ShoppingHub.BLL.Services.Abstraction.Ratings;



namespace ShoppingHub.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IProductRatingService productRatingService;
        private readonly IOrderService orderservice;
        
        public ProductController(IProductService _productservice, ICategoryService _categoryService , IProductRatingService _productRatingService,IOrderService _orderservice)
        {
            productService = _productservice;
            categoryService = _categoryService;
            productRatingService = _productRatingService;
            orderservice = _orderservice;
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

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

        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var vm = productService.GetProductForEdit(id);
        //    if (vm == null) return NotFound();

        //    vm.Categories = categoryService.GetAllCategories();
        //    return View(vm);
        //}

        //[HttpPost]
        //public IActionResult Edit(EditProductVM vm)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        vm.Categories = categoryService.GetAllCategories();
        //        TempData["Error"] = "Invalid input.";
        //        return View(vm);
        //    }

        //    var (isError, message) = productService.EditProduct(vm);

        //    if (isError)
        //    {
        //        vm.Categories = categoryService.GetAllCategories();
        //        TempData["Error"] = message ?? "Something went wrong while updating the product.";
        //        return View(vm);
        //    }

        //    TempData["Success"] = "Product updated successfully!";
        //    return RedirectToAction("GetAllProducts");
        //}




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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productService.GetProductByID(id);
            if (product == null) return NotFound();

            var vm = new EditProductVM
            {
                ProductID = product.ProductId,
                ProductName = product.ProductName,
                ProductNameAR = product.ProductNameAR,
                Price = product.Price,
                Quantity = product.Quantity,
                Description = product.Description,
                DescriptionAR = product.DescriptionAR,
                CategoryId = product.CategoryId,
                ExistingImagePath = product.ImagePath
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditProductVM vm)
        {
            if (!ModelState.IsValid)
            {
                // Re-populate categories if needed
                vm.Categories = categoryService.GetAllCategories();
                return View(vm);
            }

            var product = productService.GetProductByID(vm.ProductID);
            if (product == null) return NotFound();

            // ✅ Safe update logic (like Identity’s way)
            // Update only if changed
            if (vm.Price != product.Price) product.UpdatePrice(vm.Price);
            if (vm.Quantity != product.Quantity) product.UpdateQuantity(vm.Quantity);
            if (vm.Description != product.Description || vm.DescriptionAR != product.DescriptionAR)
                product.UpdateDescription(vm.Description, vm.DescriptionAR);
            if (vm.ProductName != product.ProductName || vm.ProductNameAR != product.ProductNameAR)
                product.UpdateNames(vm.ProductName, vm.ProductNameAR);
            if (vm.CategoryId != product.CategoryId)
                product.CategoryId = vm.CategoryId;
            // ✅ Handle image replacement like profile pic
            if (vm.NewImageFile != null)
            {
                if (!string.IsNullOrEmpty(vm.ExistingImagePath))
                {
                    Load.RemoveFile("Files/images/products", vm.ExistingImagePath);
                }
                var newImagePath = Load.UploadFile("Files/images/products", vm.NewImageFile);
                product.UpdateImage(newImagePath);
            }

            // ✅ Save back via service
            var (isError, message) = productService.UpdateProduct(product);

            if (isError)
            {
                TempData["Error"] = message ?? "Something went wrong while updating the product.";
                vm.Categories = categoryService.GetAllCategories();
                return View(vm);
            }

            TempData["Success"] = "Product updated successfully!";
            return RedirectToAction("GetAllProducts");
        }

        //[HttpPost]
        //public IActionResult AddRating(int productId, int rate, string? comment)
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        // user is not logged in → redirect or show error
        //        return RedirectToAction("Login", "Account");
        //    }

        //    var product = productService.GetProductByID(productId);
        //    if (product == null) return NotFound();

        //    product.AddOrUpdateRating(userId, rate, comment);
        //    productService.UpdateProduct(product);

        //    TempData["Success"] = "Thanks for your feedback!";
        //    return RedirectToAction("Details", new { id = productId });
        //}

        [HttpPost]
        public IActionResult RateProduct(int productId, int rate, string? comment)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

          

            // ✅ Call service instead of _context
            var result = productRatingService.RateProduct(productId, userId, rate, comment);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction("Details", new { id = productId });
            }

            TempData["Success"] = result.Message;
            return RedirectToAction("Details", new { id = productId });
        }

    }
}
