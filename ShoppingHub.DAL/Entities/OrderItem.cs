using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class OrderItem
    {
        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        //public OrderItem(int orderID, int productID, int quantity)
        //{
        //    OrderID = orderID;
        //    ProductID = productID;
        //    Quantity = quantity;
        //}
    }
}
