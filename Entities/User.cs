
namespace RestaurantAPI.Entities
{
    public class User(int id, string firstName, string lastName, string username, string passwordHash, string email, Role role)
    {
        public int Id { get; set; } = id;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public string Username { get; set; } = username;
        public string PasswordHash { get; set; } = passwordHash;
        public string Email { get; set; } = email;
        public Role Role { get; set; } = role;
    }
}
