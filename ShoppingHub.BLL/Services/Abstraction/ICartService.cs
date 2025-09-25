using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingHub.BLL.ModelVm.Cart;
using ShoppingHub.DAL.Entities;

namespace ShoppingHub.BLL.Services.Abstraction
{
    public interface ICartService
    {
        (bool, string) AddToCart(CreateCartItemVM item);
        (bool, string) GetItem(int productID, string userId);
        (bool, string, List<ViewCartVM>) ViewCart(string userID);
        (bool, string) UpdateQuantity(int productID, string userId, int quantity);
        (bool, string) RemoveItem(string userID, int productID);
        (bool, string) ClearCart(string userID);
        (bool, string, List<CartItem>) GetAllUserItems(string userID);

    }
}
