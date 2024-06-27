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
      

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MovieFullInfo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetMovieById(Request query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Title is required.");
        

            var movie = await _movieService.GetMovieAsync(query.Query,  cancellationToken);
            return movie switch
            {
                { Data: null, ApiExceptionMessage: not null } => Unauthorized(movie.ApiExceptionMessage),
                { Data: not null } => Ok(movie.Data),
                _ => BadRequest(movie?.ResponseErrorMessage)
            };
        }


        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> SearchMovies(Request query, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Title is required.");

            var searchResults = await _movieService.SearchMoviesAsync(query.Query, cancellationToken);
          
            return searchResults switch
            {
                { Data: null, ApiExceptionMessage: not null } => Unauthorized(searchResults.ApiExceptionMessage),
                { Data: not null } => Ok(searchResults.Data),
                _ => BadRequest(searchResults?.ResponseErrorMessage)
            };
        }
    }
 
}
