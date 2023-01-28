using Microsoft.EntityFrameworkCore;
using miniapi.Models;

namespace miniapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
    }
}