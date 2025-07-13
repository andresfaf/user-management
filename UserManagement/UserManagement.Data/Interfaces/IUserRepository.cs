using UserManagement.Data.Entities;

namespace UserManagement.Data.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<IEnumerable<User>> GetAll();
        Task Delete(int id);
        Task Update(User user);
    }
}
