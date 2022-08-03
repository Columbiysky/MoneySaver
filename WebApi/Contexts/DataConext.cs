#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebApi.Contexts
{
    public class DataConext : DbContext
    {
        public DbSet<DataModel> Data { get; set; } = null!;

        public DataConext(DbContextOptions<DataConext> options) : base(options) { }
    }
}
