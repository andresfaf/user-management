namespace UserManagement.Api.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string Gender { get; set; }
    }
}
