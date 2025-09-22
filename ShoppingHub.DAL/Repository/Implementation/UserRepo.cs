using ShoppingHub.DAL.DataBase;
using ShoppingHub.DAL.Entities;
using ShoppingHub.DAL.Repository.Abstraction;
using System.Collections.Specialized;

namespace ShoppingHub.DAL.Repository.Implementation
{
    public class UserRepo : IuserRepo
    {
        public readonly shoppingHubDbContext _db;

        public UserRepo(shoppingHubDbContext _context)
        {
            _db = _context;
        }
        public bool Create(User user)
        {
            try
            {
                var result = _db.Users.Add(user);
                _db.SaveChanges();
                if (result.Entity.Id is not null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)

            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                return _db.Users.First(a => a.Id == id).delete();
            }
            catch (Exception ex)

            {
                throw ex;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                var result = _db.Users.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetById(string Id)
        {
            try
            {
                var result = _db.Users.FirstOrDefault(a => a.Id == Id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(User user)
        {
            try
            {
                var result = _db.Users.FirstOrDefault(a => a.Id == user.Id);
                if (result != null)
                {

                    if (result.update(user))
                        _db.SaveChanges();
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Restore(string id)
        {
            try
            {
                return _db.Users.First(a => a.Id == id).delete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
