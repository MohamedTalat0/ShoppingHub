using Microsoft.EntityFrameworkCore;
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
    public  class CartItemRepo:ICartItemRepo
    {
        private readonly shoppingHubDbContext Db;
        public CartItemRepo(shoppingHubDbContext Db)
        {
            this.Db = Db;
        }
        public bool Create(CartItem item)
        {
            try
            {
                var result = Db.CartItems.Add(item);
                var rows = Db.SaveChanges();
                if (result.Entity.ProductID > 0 && !string.IsNullOrWhiteSpace(result.Entity.UserID))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string userID, int productID)
        {
            try
            {
                var Item = Db.CartItems.Where(x => x.UserID == userID && x.ProductID == productID).FirstOrDefault();
                if (Item != null)
                {
                    Db.CartItems.Remove(Item);
                    Db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAll(string userID)
        {
            try
            {
                var Items = Db.CartItems.Where(x => x.UserID == userID).ToList();
                if (Items.Count > 0)
                {
                    Db.CartItems.RemoveRange(Items);
                    Db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CartItem GetItem(string userID, int productID)
        {
            var result = Db.CartItems.FirstOrDefault(x => x.UserID == userID && x.ProductID == productID);
            return result;
        }

        public List<CartItem> GetUserItems(string userID)
        {
            try
            {
                var result = Db.CartItems.Include(ci => ci.Product).Where(x => x.UserID == userID).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(string userID, int productID, int quantity)
        {
            try
            {
                var result = Db.CartItems.FirstOrDefault(x => x.UserID == userID && x.ProductID == productID);
                if (result != null)
                {
                    if (result.update(quantity))
                        Db.SaveChanges();
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
