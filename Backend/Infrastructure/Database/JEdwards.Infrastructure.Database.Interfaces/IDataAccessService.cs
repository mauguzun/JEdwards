using JEdwards.Domain.Entities;

namespace JEdwards.Infrastructure.Database.Interfaces
{
    public interface IDataAccessService
    {
        Task AddQueryAsync(SearchQuery query ,CancellationToken cancellationToken);
        Task<List<SearchQuery>> GetLatestQueriesAsync(CancellationToken cancellationToken);
    }
}
