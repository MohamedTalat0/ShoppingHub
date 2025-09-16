using ShoppingHub.DAL.Entities;

namespace ShoppingHub.DAL.Repository.Abstraction
{

    public interface IuserRepo
    {
        List<User> GetAllUsers();
        User GetById(int Id);
        bool Create(User user);
        bool Update(User user);
        bool Delete(int id);
        bool Restore(int id);
    }

}
