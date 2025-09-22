using ShoppingHub.BLL.Helper;
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
        
        
    }
}





