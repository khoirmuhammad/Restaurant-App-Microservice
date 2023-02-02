namespace UserService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Deleted { get; set; }

        public Customer Customer { get; set; } = default!;
    }
}
