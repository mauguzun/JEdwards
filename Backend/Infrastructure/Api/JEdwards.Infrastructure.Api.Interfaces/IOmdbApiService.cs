using JEdwards.Application.DTO;
using JEdwards.Domain;

namespace JEdwards.Infrastructure.Api.Interfaces
{
    public interface IOmdbApiService
    {
        public Task<ApiResponse<List<Movie>>> SearchMoviesAsync(string title, CancellationToken cancellationToken);

        public Task<ApiResponse<MovieFullInfo>> GetMovieAsync(string imdbID, CancellationToken cancellationToken);

    }
}
