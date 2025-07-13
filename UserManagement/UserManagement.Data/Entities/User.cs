namespace UserManagement.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string Gender { get; set; }
    }
}
