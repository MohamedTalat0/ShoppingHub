//using Microsoft.AspNetCore.Mvc;
//using ShoppingHub.BLL.ModelVm.RatingVM;
//using ShoppingHub.BLL.Services.Implementation.ProductRatingService;
//using ShoppingHub.BLL.Services.Abstraction.Ratings;
//public class RatingController : Controller
//{
//    private readonly IProductRatingService _ratingservice;

//    public RatingController(IProductRatingService service)
//    {
//        _ratingservice = service;
//    }

//    [HttpPost]
//    public IActionResult Add(ProductRatingVM vm)
//    {
//        if (!ModelState.IsValid)
//            return BadRequest("Invalid data");

//        var (success, message) = _ratingservice.AddRating(vm);

//        if (success)
//        {
//            TempData["Success"] = message;
//            return RedirectToAction("Details", "Product", new { id = vm.ProductId });
//        }

//        TempData["Error"] = message;
//        return RedirectToAction("Details", "Product", new { id = vm.ProductId });
//    }
//}

