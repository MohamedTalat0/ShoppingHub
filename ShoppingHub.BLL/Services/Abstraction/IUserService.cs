
using ShoppingHub.BLL.ModelVM;

namespace ShoppingHub.BLL.Service.Abstraction
{
    public interface IUserService
    {
        public (bool, string) Create(CreateUserVM user);
        //public (bool, string, List<GetAllVM>) GetAll();
    }
}
