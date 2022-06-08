using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Contexts
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;

        public CategoryContext(DbContextOptions<CategoryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

    public class SubCategoryContext : DbContext
    {
        public DbSet<SubCategory> SubCategories { get; set; } = null!;

        public SubCategoryContext(DbContextOptions<SubCategoryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }

}
