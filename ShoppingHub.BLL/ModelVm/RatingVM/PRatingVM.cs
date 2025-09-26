using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm.RatingVM
{
   public class PRatingVM
    {
        public string UserName { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
    }
}
