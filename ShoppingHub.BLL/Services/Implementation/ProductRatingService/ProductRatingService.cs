using ShoppingHub.BLL.ModelVm.RatingVM;
using ShoppingHub.BLL.Services.Abstraction.Ratings;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using ShoppingHub.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Implementation.ProductRatingService
{
  public  class ProductRatingService:IProductRatingService
    {
        private readonly IProductRatingRepo _ratingRepo;
        private readonly IProductRepo _productRepo;
        private readonly IOrderRepo _orderRepo;
        public ProductRatingService(IProductRatingRepo ratingRepo, IProductRepo productRepo ,IOrderRepo orderRepo)
        {
            _ratingRepo = ratingRepo;
            _productRepo = productRepo;
                _orderRepo = orderRepo;
        }


        //public (bool, string) AddRating(AddratingVM vm)
        //{
        //    var product = _productRepo.GetProductByID(vm.ProductId);
        //    if (product == null)
        //        return (false, "Product not found");

        //    var rating = new ProductRating(vm.UserId, vm.ProductId, vm.Rate, vm.Comment);

        //    _ratingRepo.AddRating(rating);

        //    // update average
        //    var ratings = product.Ratings.Append(rating);
        //    var newAvg = ratings.Average(r => r.Rate);

        //    typeof(Product).GetProperty("AvgRate")!
        //                   .SetValue(product, newAvg);

        //    _ratingRepo.Save();
        //    return (true, "Rating added successfully");
        ////}
        ///
        public (bool Success, string Message) RateProduct(int productId, string userId, int rate, string? comment)
        {
            // check purchase
            var hasPurchased = _orderRepo.HasUserPurchasedProduct(userId, productId);
            if (!hasPurchased)
                return (false, "You can only rate products you’ve purchased.");

            var product = _productRepo.GetProductWithRatings(productId);
            if (product == null)
                return (false, "Product not found.");

            product.AddOrUpdateRating(userId, rate, comment);

            _productRepo.UpdateProduct(product); // Save rating + avgRate
            return (true, "Thanks for your review!");
        }


    }

}