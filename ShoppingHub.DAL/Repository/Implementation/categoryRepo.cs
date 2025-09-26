using ShoppingHub.DAL.DataBase;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ShoppingHub.DAL.Repository.Implementation.categoryRepo;

namespace ShoppingHub.DAL.Repository.Implementation
{
    public class categoryRepo : IcategoryRepo
    {
        public readonly shoppingHubDbContext _shoppingHubDbContext;
        public categoryRepo(shoppingHubDbContext _context)
        {
            _shoppingHubDbContext = _context;
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                return _shoppingHubDbContext.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching categories.", ex);
            }
        }

        public Category GetCategoryById(int id)
        {
            try
            {
                return _shoppingHubDbContext.Categories.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred ", ex);
            }
        }




    }
}
