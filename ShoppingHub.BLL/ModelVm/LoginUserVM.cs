using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingHub.BLL.ModelVm
{
    public class LoginUserVM
    {
        [Required(ErrorMessage = "Name must not be empty")]
        public string UserName { get; set;}
        [Required(ErrorMessage = "Name must not be empty")]
        public string Password { get; set;}
    }
}
