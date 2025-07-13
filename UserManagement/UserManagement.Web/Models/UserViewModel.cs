using UserManagement.Web.Dtos;

namespace UserManagement.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; } = 0;
        public required string Name { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string Gender { get; set; }
        public bool EnabledUpdate { get; set; }
    }
}
