using JEdwards.Domain.Entities;
using JEdwards.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JEdwards.Infrastructure.Database.Implemenations
{
    public class DataAccessService : IDataAccessService
    {
        private readonly ApplicationDbContext _context;
        private const int queryCount = 5;

        public DataAccessService(ApplicationDbContext context) => _context = context;

        public async Task AddQueryAsync(SearchQuery apiQuery, CancellationToken cancellationToken)
        {
            await _context.AddAsync(apiQuery, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<List<SearchQuery>> GetLatestQueriesAsync(CancellationToken cancellationToken)
            => _context.SearchQueries.OrderByDescending(q => q.Date).Take(5).ToListAsync(cancellationToken);

    }
}