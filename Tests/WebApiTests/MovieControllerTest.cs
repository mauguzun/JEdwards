using JEdwards.Api.Controllers;
using JEdwards.Application.DTO;
using JEdwards.Application.Interfaces;
using JEdwards.Domain.Api;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;



namespace MyApp.Tests.Controllers
{
    [TestClass]
    public class MoviesControllerTests
    {
        private Mock<IMovieService> _mockMovieService;
        private MovieController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockMovieService = new Mock<IMovieService>();
            _controller = new MovieController(_mockMovieService.Object);
        }

        [TestMethod]
        public async Task AddMovies_SearchMovies_ReturnsOkResult()
        {
            //arange
            string testTitle = "test";
            var expectedMoviesList = new List<Movie> { new Movie(testTitle, "2000", "123", "Horror", "Poster") };

            var expectedApiResponse = new ApiResponse<List<Movie>> { Data = expectedMoviesList };
            _mockMovieService.Setup(s => s.SearchMoviesAsync(testTitle, CancellationToken.None)).ReturnsAsync(expectedApiResponse);

            // Act
            var result = (ObjectResult) await _controller.SearchMoviesByTitleAsync(new SearchRequest(testTitle), CancellationToken.None) ;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode , $"should be {HttpStatusCode.OK}");

            var resultValue = result.Value as List<Movie>;
            Assert.AreEqual(expectedMoviesList.Count, resultValue.Count, $"should be {expectedMoviesList.Count}");
            CollectionAssert.AreEqual(expectedMoviesList, resultValue);

        }

        [TestMethod]
        public async Task AddMovie_GetMovieAsync_ReturnsOkResult()
        {
            string movieId = "test";
            var expectedMovie =
                new MovieDetail(
                    "Test", "2000", movieId, "Horror", "Poster", "PG-13", "2000-01-01", "120 min", "Horror",
                    "Director", "Writer", "Actor1, Actor2", "Plot", "English", "USA", "Awards",
                    new List<Rating> { new Rating("Source1", "8/10"), new Rating("Source2", "80%") },
                    "75", 8.5f, "10,000", "2000-01-01", "Box Office", "Production", "Website", "True");
            
            var expectedApiResponse = new ApiResponse<MovieDetail> { Data = expectedMovie };

            _mockMovieService.Setup(s => s.GetMovieAsync(movieId, CancellationToken.None)).ReturnsAsync(expectedApiResponse);

            // Act
            var result = (ObjectResult)await _controller.GetMovieByIdAsync(new SearchRequest(movieId), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode, $"should be {HttpStatusCode.OK}");

            var resultValue = result.Value as MovieDetail;
            Assert.IsNotNull(resultValue);
            Assert.AreEqual(expectedMovie.Title, resultValue.Title);
        }

        [TestMethod]
        public async Task GetMovieAsync_ReturnNotFoundResult()
        {
            //asert
            _mockMovieService.Setup(s => s.GetMovieAsync("movieId", CancellationToken.None)).ReturnsAsync(new ApiResponse<MovieDetail>());

            // Act
            var result = (BadRequestObjectResult) await _controller.SearchMoviesByTitleAsync(new SearchRequest("movieId"), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, result.StatusCode);

        }

    }
}
