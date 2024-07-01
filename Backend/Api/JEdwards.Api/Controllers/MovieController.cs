using JEdwards.Application.Interfaces;
using JEdwards.Domain.Api;
using Microsoft.AspNetCore.Mvc;

namespace JEdwards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService) => _movieService = movieService;

        /// <summary>
        /// Get movie by id
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Movie </returns>
        /// <response code="200">List of movies</response>
        /// <response code="400">On bad resuest</response>
        /// <response code="401">On api key error</response>
        /// <response code="500">If any expcetion</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetMovieById(Request query, CancellationToken cancellationToken)
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
        /// Search move by query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Movie lists</returns>
        /// <response code="200">List of movies</response>
        /// <response code="400">On bad resuest</response>
        /// <response code="401">On api key error</response>
        /// <response code="500">If any expcetion</response>
        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> SearchMovies(Request query, CancellationToken cancellationToken)
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
