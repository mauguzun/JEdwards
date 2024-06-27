using Microsoft.EntityFrameworkCore;

namespace JEdwards.Infrastructure.Database.Implemenations
{
    public class TestDbContext : ApplicationDbContext
    {
        public TestDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
