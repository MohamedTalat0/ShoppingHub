using Microsoft.EntityFrameworkCore;
using ShoppingHub.DAL.DataBase;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Repository.Implementation
{
  public  class ProductRatingRepo:IProductRatingRepo
    {
        private readonly shoppingHubDbContext _context;

        public ProductRatingRepo(shoppingHubDbContext context)
        {
            _context = context;
        }

        public void AddRating(ProductRating rating)
        {
            _context.ProductRatings.Add(rating);
        }
        //explain
        public List<ProductRating> GetRatingsforProduct(int productId)
        {
            return _context.ProductRatings
                           .Where(r => r.ProductID == productId)
                           .Include(r => r.User)
                           .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    

    }
}
