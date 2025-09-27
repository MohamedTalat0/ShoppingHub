using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace ShoppingHub.BLL.ModelVm.UserVM
{
    
    public class CreateUserVM
    {
        [Required(ErrorMessage ="Name must not be empty")]
        public string Name { get;  set; }
        [Required(ErrorMessage = "Email must not be empty")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password must not be empty")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Phone number must not be empty")]
       public string PhoneNumber { get; set; }
        
        [Required(ErrorMessage = "Address must not be empty")]
        public string Address { get;  set; }

        public DateTime createdOn { get; set; }= DateTime.Now;

        public IFormFile? profileImage { get; set; }


    }
}
