using Microsoft.AspNetCore.Identity;

namespace ShoppingHub.DAL.Entities
{
    public class User : IdentityUser
    {
        public string Role { get; private set; }
        public string ImagePath { get; private set; }
        public string Address {  get; private set; }
        public bool isDeleted { get; private set; } = false;
        public string createdOn { get; private set; }
        public string deletedOn { get; private set; }
        public string lastUpdatedOn { get; private set; }
        public List<Order> Orders { get; private set; } = new List<Order>();
        public List<CartItem> cartItems { get; private set; } = new List<CartItem>();
        public List<ProductRating> Ratings { get; private set; } = new List<ProductRating>();

        public User (
            string Role ,
            string ImagePath, 
            string Address,string createdOn) {
            this.Role = Role;
            this.ImagePath = ImagePath;
            this.Address = Address;
            this.isDeleted = false;
            this.deletedOn = "NotDeleted";
            this.lastUpdatedOn = "Not Updated";
            this.createdOn=createdOn.ToString();
        }

        public User()
        {
        }        
        
        public void updateProfile(string userName,string image,string address)
        {
            this.ImagePath=image;
            this.Address=address;
            this.UserName = userName;
            this.lastUpdatedOn = DateTime.Now.ToString();
        }
        public bool delete() { 
            return this.isDeleted=true;
        }
        public bool restore()
        {
            return this.isDeleted=false;
        }
        public void changeRole(string role)
        {
            Role = role ;
        }

    }
}
