using JEdwards.Application.DTO;
using JEdwards.Domain.Api;

namespace JEdwards.Application.Interfaces
{
    public interface IMovieService
    {
        Task<ApiResponse<List<Movie>>> SearchMoviesAsync(string query, CancellationToken cancellationToken);
        Task<ApiResponse<MovieDetail>> GetMovieAsync(string imdbID, CancellationToken cancellationToken);
    }
}
