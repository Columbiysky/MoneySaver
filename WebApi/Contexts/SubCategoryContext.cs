#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Contexts
{
    public class SubCategoryContext : DbContext
    {
        public DbSet<SubCategory> SubCategories { get; set; } = null!;

        public SubCategoryContext(DbContextOptions<SubCategoryContext> options)
            : base(options) { }
    }
}
