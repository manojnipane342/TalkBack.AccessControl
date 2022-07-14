namespace TalkBackAccessControl.Data.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = null!;

        public byte[]? PasswordHash { get; set; } = null!;

        public byte[]? PasswordSalt { get; set; } = null!;

    }
}