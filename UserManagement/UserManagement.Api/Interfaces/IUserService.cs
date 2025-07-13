using UserManagement.Api.Dtos;

namespace UserManagement.Api.Interfaces
{
    public interface IUserService
    {
        Task Add(UserDto user);
        Task<IEnumerable<UserDto>> GetAll();
        Task Delete(int id);
        Task Update(UserDto user);
    }
}
