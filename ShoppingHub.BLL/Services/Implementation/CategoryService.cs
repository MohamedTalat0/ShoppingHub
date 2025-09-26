using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using ShoppingHub.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ShoppingHub.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IcategoryRepo _categoryRepo;

        public CategoryService(IcategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepo.GetAllCategories();
        }

        public Category GetCategoryById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid category ID");

            return _categoryRepo.GetCategoryById(id);
        }

 

    }
}

