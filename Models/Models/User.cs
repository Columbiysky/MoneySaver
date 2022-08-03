#pragma warning disable CS1591
namespace Models
{
    public interface IUser
    {
        public int? Id { get; set; }
        public string? Login { get; set; }
        public string? Pass { get; set; }
    }

    public class User : IUser
    {
        public int? Id { get; set; }
        public string? Login { get; set; }
        public string? Pass { get; set; }
    }
}
