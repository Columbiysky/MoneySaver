#pragma warning disable CS1591
namespace Models
{
    public interface ICategory
    {
        int? Id { get; set; }
        int UserId { get; set; }
        string? Name { get; set; }
    }

    public class Category : ICategory
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
    }
}
