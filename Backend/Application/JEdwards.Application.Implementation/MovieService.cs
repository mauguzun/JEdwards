using JEdwards.Application.DTO;
using JEdwards.Application.Interfaces;
using JEdwards.Domain.Api;
using JEdwards.Domain.Entities;
using JEdwards.Infrastructure.Api.Interfaces;
using JEdwards.Infrastructure.Database.Interfaces;

namespace JEdwards.Application.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IOmdbApiService _omdbApiService;
        private readonly IDataAccessService _dataAccessService;

        public MovieService(IOmdbApiService omdbApiService, IDataAccessService dataAccessService) =>
            (_omdbApiService, _dataAccessService) = (omdbApiService, dataAccessService);

        public async Task<ApiResponse<List<Movie>>> SearchMoviesAsync(string query, CancellationToken cancellationToken)
        {
            var response = await _omdbApiService.SearchMoviesAsync(query, cancellationToken);


              
            var (_, responseErrorMessage, apiExceptionMessage) = response;

            await _dataAccessService.AddQueryAsync(new SearchQuery(query)
            {
                ErrorMessage = responseErrorMessage ?? apiExceptionMessage
            }, cancellationToken);

            return response;
        }

        public Task<ApiResponse<MovieFullInfo>> GetMovieAsync(string imdbID, CancellationToken cancellationToken) =>
            _omdbApiService.GetMovieAsync(imdbID, cancellationToken);


    }
}
