using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.DAL.Entities
{
    public class ProductRating
    {
        [ForeignKey("User")]
        public string UserID { get; private set; }
        public User User { get; set; }
        [ForeignKey("Product")]
        public int ProductID { get; private set; }
        public Product Product { get; set; }
        public int Rate { get; private set; }
    }
}
