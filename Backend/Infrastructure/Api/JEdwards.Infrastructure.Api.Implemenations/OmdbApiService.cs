using JEdwards.Application.DTO;
using JEdwards.Domain.Api;
using JEdwards.Infrastructure.Api.Implemenations.Responses;
using JEdwards.Infrastructure.Api.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;


namespace JEdwards.Infrastructure.Api.Implemenations
{
    public class OmdbApiService : IOmdbApiService
    {
        private readonly string _apiKey;
        private const string _apiUrl = "http://www.omdbapi.com/";

        public OmdbApiService(string apiKey) => _apiKey = apiKey;

        public async Task<ApiResponse<List<Movie>>> SearchMoviesAsync(string title, CancellationToken cancellationToken)
        {
            RestClient client = _RestClientWithSetting();

            var request = new RestRequest($"?apikey={_apiKey}&s={title}", Method.Get);
            var response = await client.ExecuteAsync<MoviesSearchResponse>(request, cancellationToken);

            return response switch
            {
                { ErrorException: not null } => throw response.ErrorException,
                { Data: { TotalResults: 0 } } => new ApiResponse<List<Movie>>() { ResponseErrorMessage = JsonConvert.DeserializeObject<ResponseError>(response.Content).Error },
                { Data: { Search: var searchResults } } => new ApiResponse<List<Movie>>() { Data = searchResults },
                _ => throw new NotImplementedException($"{nameof(SearchMoviesAsync)} query {title} raise not implemeneted excptions"),
            };

        }

        public async Task<ApiResponse<MovieFullInfo>> GetMovieAsync(string imdbID, CancellationToken cancellationToken)
        {
            RestClient client = _RestClientWithSetting();

            var request = new RestRequest($"?apikey={_apiKey}&i={imdbID}&plot=short", Method.Get);
            var response = await client.ExecuteAsync<MovieFullInfo>(request, cancellationToken);

            return response switch
            {
                { ErrorException: not null } => throw response.ErrorException,
                { Data.Response: "False" } => new ApiResponse<MovieFullInfo> { ResponseErrorMessage = JsonConvert.DeserializeObject<ResponseError>(response.Content).Error },
                { Data: not null }   => new ApiResponse<MovieFullInfo> { Data = response.Data },
                _ => throw new NotImplementedException($"{nameof(GetMovieAsync)} query {imdbID} raise not implemeneted excptions"),
            };

        }

        private static RestClient _RestClientWithSetting()
        {
            var jsonSettings = new JsonSerializerSettings { FloatParseHandling = FloatParseHandling.Decimal };
            var client = new RestClient(_apiUrl, configureSerialization: s => s.UseNewtonsoftJson(jsonSettings));
            return client;
        }
    }


}
