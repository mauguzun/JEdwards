using JEdwards.Application.DTO;
using JEdwards.Domain;

namespace JEdwards.Application.Interfaces
{
    public interface IMovieService
    {
        Task<ApiResponse<List<Movie>>> SearchMoviesAsync(string query, CancellationToken cancellationToken);
        Task<ApiResponse<MovieFullInfo>> GetMovieAsync(string imdbID, CancellationToken cancellationToken);
    }
}
