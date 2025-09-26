using Azure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm
{
    public class UserProfileVM
    {
        public string userName {get ; set;}
        public string email {get ; set;}
        public string userImage { get ; set;}
        public string Address { get;  set; }
        public string phoneNumber { get; set;}
        public int totalOrders {  get; set;}
    }
}
