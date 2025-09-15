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
        public int OrderID { get; private set; }
        public Order Order { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; private set; }
        public Product Product { get; set; }
        public int Quantity { get; private set; }
    }
}
