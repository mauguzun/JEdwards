using JEdwards.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JEdwards.Infrastructure.Database.Implemenations
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SearchQuery> SearchQueries { get; set; }

        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
        
    }
}
