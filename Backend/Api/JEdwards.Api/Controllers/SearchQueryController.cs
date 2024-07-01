using JEdwards.Application.Interfaces;
using JEdwards.Domain;
using JEdwards.Domain.Entities;
using JEdwards.Infrastructure.Database.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JEdwards.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchQueryController : ControllerBase
    {
        private readonly IDataAccessService _dataAccessService;
        public SearchQueryController( IDataAccessService dataAccessService) => _dataAccessService = dataAccessService;

        /// <summary>
        /// GetLatestSearches
        /// </summary>
        /// <returns>List of searchQueries</returns>
        /// <response code="200">List of searchQueries</response>
        /// <response code="204">No contents if searchQueries list are null</response>
        /// <response code="500">If any expcetion</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchQuery>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetLatestSearches(CancellationToken cancellationToken)
        {
            var latestSearches = await _dataAccessService.GetLatestQueriesAsync(cancellationToken);
            return latestSearches is null ? NoContent():  Ok(latestSearches);
        }
    }
}
