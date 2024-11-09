using Microsoft.EntityFrameworkCore;
using CDNApi.Models;

namespace CDNApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<ApiUser> ApiUsers { get; set; }

    }
}
