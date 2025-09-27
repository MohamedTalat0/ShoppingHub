using ShoppingHub.BLL.Helper;
using ShoppingHub.BLL.ModelVm;
using ShoppingHub.BLL.ModelVm.RatingVM;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using ShoppingHub.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Implementation
{

    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IcategoryRepo _categoryRepo;
        private readonly IProductRatingRepo _ratingrepo;
        private readonly IOrderRepo _orderRepo;
        public ProductService(IProductRepo repo, IcategoryRepo categoryRepo, IProductRatingRepo ratingrepo, IOrderRepo orderRepo)
        {
            _productRepo = repo;
            _categoryRepo = categoryRepo;
            _ratingrepo = ratingrepo;
            _orderRepo = orderRepo;
        }

        public (bool, string, GetAllProductsVM ) GetProducts(GetAllProductsVM vm)
        {
            try
            {
                var productsQuery = _productRepo.GetAllProducts().AsQueryable();
                if (!string.IsNullOrWhiteSpace(vm.SearchTerm))
                {
                    productsQuery = productsQuery
                        .Where(p => p.ProductName.ToLower().Contains(vm.SearchTerm.ToLower()));
                }

                
                if (vm.SelectedCategoryId.HasValue)
                {
                    productsQuery = productsQuery
                        .Where(p => p.CategoryId == vm.SelectedCategoryId.Value);
                }

             
                if (vm.MinPrice.HasValue)
                {
                    productsQuery = productsQuery
                        .Where(p => p.Price >= vm.MinPrice.Value);
                }

            
                if (vm.MaxPrice.HasValue)
                {
                    productsQuery = productsQuery
                        .Where(p => p.Price <= vm.MaxPrice.Value);
                }

              
                var totalItems = productsQuery.Count();
                var pagedProducts = productsQuery
                    .Skip((vm.PageNumber - 1) * vm.PageSize)
                    .Take(vm.PageSize)
                    .ToList();

           
                var productItems = pagedProducts.Select(p => new OutDisplayingProduct
                {
                    ProductID = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    ImagePath = p.ImagePath,
                    CategoryID=p.CategoryId,
                    ProductNameAR = p.ProductNameAR
                }).ToList();

             
                vm.Products = productItems;
                vm.TotalItems = totalItems;

                return (false, null, vm);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }





//      public EditProductVM? GetProductForEdit(int id)
//{
//    var product = _productRepo.GetProductByID(id);
//    if (product == null) return null;

//    return new EditProductVM
//    {
//        ProductID = product.ProductId,
//        ProductName = product.ProductName,
//        ProductNameAR = product.ProductNameAR,
//        Price = product.Price,
//        Quantity = product.Quantity,
//        Description = product.Description,
//        ExistingImagePath = product.ImagePath,
//        CategoryId = product.CategoryId
//    };
//}

//public (bool, string?) EditProduct(EditProductVM vm)
//{
//    try
//    {
//        var product = _productRepo.GetProductByID(vm.ProductID);
//        if (product == null)
//            return (true, "Product with such ID was not found");

//        string imagePath = product.ImagePath;
//        if (vm.NewImageFile != null)
//        {
//            imagePath = Load.UploadFile("Files/images/products", vm.NewImageFile);
//        }

//        product.Update(
//            vm.ProductName,
//            vm.ProductNameAR,
//            vm.DescriptionAR,
//            vm.Price,
//            vm.Quantity,
//            vm.Description,
//            imagePath,
//            vm.CategoryId
//        );

//        _productRepo.EditProduct(product);

//        return (false, null);
//    }
//    catch (Exception ex)
//    {
//        return (true, ex.Message);
//    }
//}



        public (bool, string) AddProduct(AddProductVM vm)
        {
            try
            {
                var dbproduct = new Product(
                    vm.ID,
                    vm.ProductName,
                    vm.ProductNameAR,
                    vm.DescriptionAR,
                    vm.Price,
                    vm.Quantity,
                    vm.Description,
                  
                    Load.UploadFile("Files/images/products", vm.ImageFile),
                    vm.CategoryId
                 
                );

                _productRepo.AddProduct(dbproduct);
                return (false, null);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string, ProductDetailsVM) GetProductDetails(int productId,string? userid=null)
        {
            try
            {
                var ratings = _ratingrepo.GetRatingsforProduct(productId);
                var ratedproducted = _productRepo.GetProductWithRatings(productId);
                var product = _productRepo.GetProductByID(productId);
                if (product == null)
                {
                    return (true, "Product not found.", null);
                }
                bool canRate = false;
                if (!string.IsNullOrEmpty(userid))
                {
                    canRate = _orderRepo.HasUserPurchasedProduct(userid, productId);
                }

                var vm = new ProductDetailsVM
                {
                    ProductID = product.ProductId,
                    ProductName = product.ProductName,
                    ProductNameAR = product.ProductNameAR,
                    DescriptionAR = product.DescriptionAR,
                    Price = product.Price,
                    Description = product.Description,
                    ImagePath = product.ImagePath,
                    CategoryName = product.Category?.Name ?? "Uncategorized",
                    AverageRating = product.AvgRate ?? 0,
                    
                    //Ratings = product.Ratings,  // list of all user reviews
                    CanRate = canRate,


                    //AverageRating = ratings.Any() ? ratings.Average(r => r.Rate) : 0,
                    //Ratings = ratings.Select(r => new PRatingVM
                    //   {
                    //       UserName = r.User.UserName,
                    //       Rate = r.Rate,
                    //       Comment = r.Comment
                    //   }).ToList()
                    Ratings = product.Ratings.Select(r => new ProductRatingVM
                    {
                        UserName = r.User?.UserName ?? "Anonymous",
                        Rate = r.Rate,
                        Comment = r.Comment
                    }).ToList(),
                };

                return (false, "", vm);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }

        public (bool, string?) RemoveProduct(int productId)
        {
            try
            {
                return _productRepo.Remove(productId);
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public Product? GetProductByID(int id)
        {
            return _productRepo.GetProductByID(id);
        }

        public (bool, string) UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = _productRepo.GetProductByIdEditversion(product.ProductId);
                if (existingProduct == null)
                    return (true, "Product not found");

                // ✅ EF is already tracking the product if attached
                var result = _productRepo.UpdateProduct(product);

                if (!result)
                    return (true, "Failed to update product");

                return (false, ""); // no error
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

    }

}





