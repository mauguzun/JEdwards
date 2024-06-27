using JEdwards.Api.Controllers;
using JEdwards.Application.Interfaces;
using JEdwards.Domain.Entities;
using NetArchTest.Rules;


namespace ArchTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Domain_ShouldNotHaveRefernce_ToOtherProjects()
        {
            var assembly = typeof(SearchQuery).Assembly;

            var otherProjects =
                 new[] { 
                     "JEdwards.Api",
                     "JEdwards.Application.DTO",
                     "JEdwards.Application.Interfaces",
                     "JEdwards.Application.Interfaces" // etc
                 };

            var expected = Types.InAssembly(assembly)
                  .ShouldNot()
                  .HaveDependencyOnAll( otherProjects)
                  .GetResult().IsSuccessful;

            Assert.AreEqual(true, expected, $"Assembly should not have dependencies on: {string.Join(", ", otherProjects)}");
        }

        [TestMethod]
        public void JEdwardsApplicationInterfaces_ShouldHaveRefernce()
        {
            var assembly = typeof(IMovieService).Assembly;

            var requiredProjects =
                 new[] {
                     "JEdwards.Domain",
                     "JEdwards.Application.DTO" };

            var expected = Types.InAssembly(assembly)
                  .Should()
                  .HaveDependencyOnAll(requiredProjects)
                  .GetResult().IsSuccessful;

            Assert.AreEqual(true, expected, $"Assembly should have dependencies on: {string.Join(", ", requiredProjects)}");
        }
    }
}