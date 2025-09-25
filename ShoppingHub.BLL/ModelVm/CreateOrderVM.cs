using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm
{
public class Address
{
    public string City { get; set; }
    public string Street { get; set; }
    public string Building { get; set; }
}
public class CreateOrderVM
    {
        [Required]
        public Address Address { get; set; }
        [Required]
        public string PaymentMethod {  get; set; }


    }
}
