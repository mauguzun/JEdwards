using JEdwards.Domain.Api;

namespace JEdwards.Infrastructure.Api.Implemenations.Responses
{
    public record MoviesSearchResponse(List<Movie> Search, int TotalResults, string Response) { }

}
