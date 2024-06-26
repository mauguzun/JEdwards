using JEdwards.Application.Interfaces;
using JEdwards.Domain;
using JEdwards.Infrastructure.Database.Interfaces;
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
        public async Task<IActionResult> GetMovieById(string imdbID, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(imdbID))
                return BadRequest("Title is required.");
        

            var movie = await _movieService.GetMovieAsync(imdbID,  cancellationToken);
            return movie switch
            {
                { Data: null, ApiExceptionMessage: not null } => Unauthorized(movie.ApiExceptionMessage),
                { Data: null } => BadRequest(movie.ResponseErrorMessage),
                _ => Ok(movie.Data)
            };
        }


        [HttpPost("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Movie))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> SearchMovies(string title, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title is required.");

            var searchResults = await _movieService.SearchMoviesAsync(title, cancellationToken);

            return searchResults switch
            {
                { Data: null, ApiExceptionMessage: not null } => Unauthorized(searchResults.ApiExceptionMessage),
                { Data: null } => BadRequest(searchResults.ResponseErrorMessage),
                _ => Ok(searchResults.Data)
            };
        }
    }
}
