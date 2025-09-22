using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Repository.Abstraction
{
    public interface IProductRepo
    {
        Product GetItem(int productID);
    }
}
