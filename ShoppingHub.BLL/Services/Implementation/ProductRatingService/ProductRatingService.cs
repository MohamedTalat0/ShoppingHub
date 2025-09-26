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

        public ProductRatingService(IProductRatingRepo ratingRepo, IProductRepo productRepo)
        {
            _ratingRepo = ratingRepo;
            _productRepo = productRepo;
        }


        public (bool, string) AddRating(AddratingVM vm)
        {
            var product = _productRepo.GetProductByID(vm.ProductId);
            if (product == null)
                return (false, "Product not found");

            var rating = new ProductRating(vm.UserId, vm.ProductId, vm.Rate, vm.Comment);

            _ratingRepo.AddRating(rating);

            // update average
            var ratings = product.Ratings.Append(rating);
            var newAvg = ratings.Average(r => r.Rate);

            typeof(Product).GetProperty("AvgRate")!
                           .SetValue(product, newAvg);

            _ratingRepo.Save();
            return (true, "Rating added successfully");
        }
    }
}
