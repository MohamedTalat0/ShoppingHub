using ShoppingHub.BLL.ModelVm.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Abstraction
{
    public interface ICartService
    {
        (bool, string) AddToCart(string userID,CartItemVM item);
        (bool, string) GetItem(int productID, string userId);
        (bool, string, ViewCartVM) ViewCart(string userID);
        (bool, string) UpdateQuantity(int productID, string userId, int quantity);
        (bool, string) RemoveItem(string userID, int productID);
        (bool, string) ClearCart(string userID);

    }
}
