using UserManagement.Web.Dtos;
using UserManagement.Web.Models;

namespace UserManagement.Web.Services.Interfaces
{
    public interface IUserHttpService
    {
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<ApiResponse> Add(UserViewModel user);
        Task<ApiResponse> Update(UserViewModel user);
        Task<ApiResponse> Delete(int id);
    }
}
