using ShoppingHub.DAL.Entities;

namespace ShoppingHub.DAL.Repository.Abstraction
{

    public interface IuserRepo
    {
        List<User> GetAllUsers();
        User GetById(string Id);
        bool Create(User user);
        //bool Update(User user);
        bool Delete(string id);
        bool Restore(string id);
    }

}
