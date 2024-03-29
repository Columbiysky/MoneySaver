﻿#pragma warning disable CS1591
using Microsoft.EntityFrameworkCore;
using Models;

namespace WebApi.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
