namespace WebApi.Models
{
    public interface ICategory
    {
        int? Id { get; set; }
        int UserId {get;set;}
        string Name { get; set; }
    }

    public interface ISubCategory
    {
        int? Id { get; set; }
        int? LinkedCategoryId { get; set; }
        string Name { get; set; }
    }

    public class Category : ICategory
    {
        public int? Id { get ; set ; }
        public int UserId { get ; set ; }
        public string Name { get ; set ; }
    }

    public class SubCategory : ISubCategory
    {
        public int? LinkedCategoryId { get ; set ; }
        public int? Id { get ; set ; }
        public string Name { get ; set ; }
    }
}
