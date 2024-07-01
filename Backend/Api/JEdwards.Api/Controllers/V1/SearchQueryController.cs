using JEdwards.Domain.Entities;
using JEdwards.Infrastructure.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JEdwards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class SearchQueryController : ControllerBase
    {
        private readonly IDataAccessService _dataAccessService;
        public SearchQueryController( IDataAccessService dataAccessService) => _dataAccessService = dataAccessService;

        /// <summary>
        /// Retrieves the list of latest search queries.
        /// </summary>
        /// <returns>A list of search queries.</returns>
        /// <response code="200">Returns the list of search queries.</response>
        /// <response code="204">Returns no content if the list of search queries is empty.</response>
        /// <response code="500">Returns an error if an exception occurs.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchQuery>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetLatestSearchQueriesAsync(CancellationToken cancellationToken)
        {
            var latestSearches = await _dataAccessService.GetLatestQueriesAsync(cancellationToken);
            return latestSearches is null ? NoContent():  Ok(latestSearches);
        }
    }
}
