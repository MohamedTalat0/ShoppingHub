using ShoppingHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Repository.Abstraction
{
    public interface ICartItemRepo
    {
        bool Create(CartItem item);
        List<CartItem> GetUserItems(string userID);
        bool Update(string userID, int productID, int quantity);
        bool Delete(string userID, int productID);
        bool DeleteAll(string userID);
        CartItem GetItem(string userID, int productID);

    }
}
