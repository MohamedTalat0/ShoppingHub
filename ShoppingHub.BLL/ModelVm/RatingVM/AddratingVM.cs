using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm.RatingVM
{
 public   class ratingVM
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
}
