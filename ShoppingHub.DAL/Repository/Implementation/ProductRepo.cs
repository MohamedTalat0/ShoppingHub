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
    public  class ProductRepo: IProductRepo
    {
            private readonly shoppingHubDbContext Db;
            public ProductRepo(shoppingHubDbContext Db)
            {
                this.Db = Db;
            }
            public Product GetItem(int productID)
            {
                var result = Db.Products.FirstOrDefault(x => x.ProductId == productID);
                return result;
            }
    }
}
