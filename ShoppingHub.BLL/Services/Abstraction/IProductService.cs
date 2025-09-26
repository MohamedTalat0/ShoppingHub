using ShoppingHub.BLL.ModelVm;
using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Abstraction
{
  public  interface IProductService
    {
        public (bool, string, GetAllProductsVM) GetProducts(GetAllProductsVM vm);
        (bool, string) EditProduct(EditProductVM productvm);
        (bool, string) AddProduct(AddProductVM vm);
        (bool, string) RemoveProduct(int productId);
        (bool, string, ProductDetailsVM) GetProductDetails(int productId);
        Product? GetProductByID(int id);
        public EditProductVM? GetProductForEdit(int id);
    }
}
