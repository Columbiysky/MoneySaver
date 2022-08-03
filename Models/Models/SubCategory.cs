#pragma warning disable CS1591
namespace Models
{
    public interface ISubCategory
    {
        int? Id { get; set; }
        int? LinkedCategoryId { get; set; }
        string? Name { get; set; }
    }

    public class SubCategory : ISubCategory
    {
        public int? LinkedCategoryId { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
