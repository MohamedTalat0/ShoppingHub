using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        //public double? AvgRate { get; private set; }

        [ForeignKey("Category")]
        public int? CATID { get; set; }
        public Category? Category { get; set; }
        public List<CartItem> cartItems { get; private set; } = new List<CartItem>();
        public List<OrderItem> orderItems { get; private set; } = new List<OrderItem>();
        public List<ProductRating> Ratings { get; private set; } = new List<ProductRating>();
    }
}
