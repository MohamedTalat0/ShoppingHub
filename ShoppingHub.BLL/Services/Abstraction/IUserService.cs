
using Microsoft.AspNetCore.Identity;
using ShoppingHub.BLL.ModelVm;
using ShoppingHub.BLL.ModelVM;

namespace ShoppingHub.Serviese
{
    public interface IUserService
    {
        public (bool, string) Create(CreateUserVM user);
        public (bool,string,UserProfileVM?) getUser(string Id);
    }

}
