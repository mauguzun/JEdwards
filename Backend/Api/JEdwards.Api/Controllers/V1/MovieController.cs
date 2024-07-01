using JEdwards.Application.Interfaces;
using JEdwards.Domain.Api;
using Microsoft.AspNetCore.Mvc;

namespace JEdwards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService) => _movieService = movieService;

        /// <summary>
        /// Retrieves a movie by its ID.
        /// </summary>
        /// <param name="query">The request containing the movie ID.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>The requested movie.</returns>
        /// <response code="200">Returns the movie.</response>
        /// <response code="400">Returns an error if the request is invalid.</response>
        /// <response code="401">Returns an error if there is an API key error.</response>
        /// <response code="500">Returns an error if an exception occurs.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieDetail))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))] 
        public async Task<IActionResult> GetMovieByIdAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Title is required.");
        

            var movie = await _movieService.GetMovieAsync(query.Query,  cancellationToken);
            return movie switch
            {
                { Data: not null } => Ok(movie.Data),
                _ => BadRequest(movie?.ResponseErrorMessage)
            };
        }

        /// <summary>
        /// Searches for movies by a given query.
        /// </summary>
        /// <param name="query">The request containing the search criteria.</param>
        /// <param name="cancellationToken">Token to cancel the request.</param>
        /// <returns>A list of movies matching the search criteria.</returns>
        /// <response code="200">Returns a list of movies.</response>
        /// <response code="400">Returns an error if the request is invalid.</response>
        /// <response code="401">Returns an error if there is an API key error.</response>
        /// <response code="500">Returns an error if an exception occurs.</response>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Movie>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> SearchMoviesByTitleAsync(SearchRequest query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Title is required.");

            var searchResults = await _movieService.SearchMoviesAsync(query.Query, cancellationToken);
          
            return searchResults switch
            {
                { Data: not null } => Ok(searchResults.Data),
                _ => BadRequest(searchResults?.ResponseErrorMessage)
            };
        }
    }
 
}
