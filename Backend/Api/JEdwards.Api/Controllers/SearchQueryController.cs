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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchQuery>))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(string))]
        
        public async Task<IActionResult> GetLatestSearches(CancellationToken cancellationToken)
        {
            var latestSearches = await _dataAccessService.GetLatestQueriesAsync(cancellationToken);
            return latestSearches is null ? NoContent():  Ok(latestSearches);
        }
    }
}
