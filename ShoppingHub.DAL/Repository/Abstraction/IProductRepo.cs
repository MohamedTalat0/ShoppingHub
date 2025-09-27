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
        bool updateQuantity(int productId, int quantity);


        bool UpdateProduct(Product product);
        public Product? GetProductWithRatings(int productId);

    }
}
