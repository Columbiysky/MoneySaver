#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Contexts
{
    public class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;

        public CategoryContext(DbContextOptions<CategoryContext> options) 
            : base(options) { }
    }
}
