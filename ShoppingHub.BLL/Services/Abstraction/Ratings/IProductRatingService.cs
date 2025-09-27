using ShoppingHub.BLL.ModelVm.RatingVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Abstraction.Ratings
{
  public  interface IProductRatingService
    {
        //(bool, string) AddRating(AddratingVM vm);
        public (bool Success, string Message) RateProduct(int productId, string userId, int rate, string? comment);
    }
}
