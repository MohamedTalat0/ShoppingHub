
using Microsoft.AspNetCore.Identity;
using ShoppingHub.BLL.ModelVM;

namespace ShoppingHub.Serviese
{
    public interface IUserService
    {
        public (bool, string) Create(CreateUserVM user);
    }

}
