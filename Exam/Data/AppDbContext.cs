using Exam.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
         : base(options) { }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Role> Roles { get; set; }

    }
}
