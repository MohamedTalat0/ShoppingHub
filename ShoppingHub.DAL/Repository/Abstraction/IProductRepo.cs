using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Repository.Abstraction
{
  public  interface IProductRepo
    {
        List<Product> GetAllProducts();
        Product GetProductByID(int id);
        (bool,string?) AddProduct(Product product);
        //public void EditProduct(Product product);
         (bool, string?) Remove(int id);

        Product GetProductByIdEditversion(int id);
<<<<<<< HEAD
        bool updateQuantity(int productId, int quantity);

=======
>>>>>>> 4ea336c7eaf79dcd14e18f5816e4732f8b4d1e73
        bool UpdateProduct(Product product);
        public Product? GetProductWithRatings(int productId);

    }
}
