using Microsoft.Identity.Client;
using ShoppingHub.BLL.Helper;
using ShoppingHub.BLL.ModelVm;
using ShoppingHub.BLL.ModelVM;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using ShoppingHub.Serviese;





namespace ShoppingHub.BLL.Service.Implementaion
{

    public  class UserService : IUserService
    {
        private readonly IuserRepo userRepo;
        
        public UserService(IuserRepo _repo)
        {
            userRepo = _repo;
        }
        public (bool, string) Create(CreateUserVM user)
        {
            try
            {
                User dbuser= new User(
            //        user.Name,user.Email,
            //user.PhoneNumber,
                Role.USER,Load.UploadFile("Files/images/usersImages",
                user.profileImage),
                user.Address==null?"Test":user.Address, user.createdOn.ToString());
                
               
                var result = userRepo.Create(dbuser);
                return (!result, !result ? "There is an error!!" : "");
            }
            catch (Exception ex)
            {
                return (true, ex.Message);
            }
        }

       public (bool,string,UserProfileVM?) getUser(string Id)
        {
            try
            {
                var dbUser = userRepo.GetById(Id);
                if (dbUser != null)
                {
                    var result = new UserProfileVM();
                    result.Address = dbUser.Address;
                    result.userName = dbUser.UserName;
                    result.email = dbUser.Email;
                    result.phoneNumber = dbUser.PhoneNumber;
                    result.userImage = dbUser.ImagePath;
                    result.totalOrders=dbUser.Orders.Count;
                    return (false, "", result);
                }
                else
                {
                    return (false, "didnt find user", null);
                }
                    
            }
            catch (Exception ex) {
                return (false, $"{ex}", null);
            }
           
            
        }
    }
}





