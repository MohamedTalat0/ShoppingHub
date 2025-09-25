using ShoppingHub.BLL.ModelVm.Cart;
using ShoppingHub.BLL.Services.Abstraction;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.Services.Implementation
{
    public class CartService:ICartService
    {
        private readonly ICartItemRepo _cartItemRepo;
        private readonly IProductRepo _productRepo;
        public CartService(ICartItemRepo _cartItemRepo, IProductRepo _productRepo)
        {
            this._cartItemRepo = _cartItemRepo;
            this._productRepo = _productRepo;
        }
        public (bool, string) AddToCart(CreateCartItemVM cartitem)
        {
            try
            {
                var item = _cartItemRepo.GetItem(cartitem.UserID, cartitem.ProductID);
                if (item != null)
                {
                    var product = _productRepo.GetItem(item.ProductID);
                    int maxQuantity = product.Quantity;
                    int newQuantity = 1 + item.Quantity;
                    if (newQuantity > maxQuantity)
                        return (true, "Quantity exceed the stock!!!!");
                    else
                    {
                        var result = _cartItemRepo.Update(item.UserID, item.ProductID, newQuantity);
                        return (false, null);
                    }
                }
                else
                {
                    var newCartItem = new CartItem(item.UserID, item.ProductID, item.Quantity);
                    var result = _cartItemRepo.Create(newCartItem);
                    if (result)
                        return (false, null);
                    else
                        return (true, "There is a problem in adding item!!");
                }
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string) ClearCart(string userID)
        {
            try
            {
                var result = _cartItemRepo.DeleteAll(userID);
                if (result)
                    return (false, null);
                else
                    return (true, "There is an error in deleting items !!!");
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string) GetItem(int productID, string userId)
        {
            try
            {
                var result = _cartItemRepo.GetItem(userId, productID);
                if (result != null)
                {
                    return (false, null);
                }
                else
                    return (true, "Item not found!!!");
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string) RemoveItem(string userID, int productID)
        {
            try
            {
                var result = _cartItemRepo.Delete(userID, productID);
                if (result)
                    return (false, null);
                else
                    return (true, "There is an error in deleting item !!!");
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string) UpdateQuantity(int productID, string userId, int quantity)
        {
            try
            {
                var item = _cartItemRepo.GetItem(userId, productID);
                if (item != null)
                {
                    var product = _productRepo.GetItem(productID);
                    int maxQuantity = product.Quantity;
                    int newQuantity = quantity + item.Quantity;
                    if (newQuantity > maxQuantity)
                        return (true, "Quantity exceed the stock!!!!");
                    else
                    {
                        var result = _cartItemRepo.Update(userId, productID, newQuantity);
                        return (false, null);
                    }
                }
                else
                    return (true, "Item not found!!!");
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

        public (bool, string, List<ViewCartVM>) ViewCart(string userID)
        {
            try
            {
                var result = _cartItemRepo.GetUserItems(userID);
                //List<Product> products = new List<Product>();
                //foreach (var item in result) 
                //{
                //    var product = _productRepo.GetItem(item.ProductID);
                //    products.Add(product);
                //}
                //var final = mapper.Map<List<ViewCartVM>>(products);
                List<ViewCartVM> items = new List<ViewCartVM>();
                foreach (var item in result)
                {
                    var product = _productRepo.GetItem(item.ProductID);
                    items.Add(new ViewCartVM()
                    {
                        Name = product.ProductName,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        MaxQuantity = product.Quantity,
                        ImagePath = product.ImagePath,
                        ProductID = item.ProductID
                    }
                    );
                }
                return (false, null, items);
            }
            catch (Exception ex)
            {
                return (true, ex.Message, null);
            }
        }
        public (bool,string,List<CartItem>) GetAllUserItems(string userID) {
            try
            {
                var result=_cartItemRepo.GetUserItems(userID);
                if (result != null) return (false, null, result);         
                
                return (true, "There is no items in cart", null);

            }
            catch (Exception ex)
            {

                return (true, ex.Message, null);
            }
        }
    }
}
