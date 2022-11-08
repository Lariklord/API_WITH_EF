using Microsoft.EntityFrameworkCore;

namespace API_WITH_EF
{
    public class MyDbContext:DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
