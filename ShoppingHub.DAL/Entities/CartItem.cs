using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class CartItem
    {
        [ForeignKey("User")]
        public string UserID { get; private set; }
        public User User { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; private set; }
        public Product Product { get; set; }
        public int Quantity { get; private set; }
        public CartItem(string UserID, int ProductID, int Quantity)
        {
            this.UserID = UserID;
            this.ProductID = ProductID;
            this.Quantity = Quantity;
        }
        public bool update(int quantity)
        {
            this.Quantity = quantity;
            return true;
        }
    }
}
