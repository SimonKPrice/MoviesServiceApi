using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoviesService.Tests
{
    [TestClass]
    public class TestMoviesController
    {
        [TestMethod]
        public void TestQuery_ActionGenre()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            Models.Filter filter = new Models.Filter
            {
                Genres = new List<string>() { "Action" }
            };

            var result = controller.Query(filter);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Models.Movie>>));

            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(3, resultMovies.Content.Count);
        }

        [TestMethod]
        public void TestQuery_ActionGenre_Title()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            Models.Filter filter = new Models.Filter
            {
                Genres = new List<string>() { "Action" },
                Title = "Fallout"
            };

            var result = controller.Query(filter);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Models.Movie>>));

            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(1, resultMovies.Content.Count);
        }

        [TestMethod]
        public void TestQuery_ActionGenre_Title_Year_NotFound()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            Models.Filter filter = new Models.Filter
            {
                Genres = new List<string>() { "Action" },
                Title = "Fallout",
                YearOfRelease = 2008
            };

            var result = controller.Query(filter);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestQuery_ActionGenre_Year_Found()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            Models.Filter filter = new Models.Filter
            {
                Genres = new List<string>() { "Action" },
                YearOfRelease = 2008
            };

            var result = controller.Query(filter);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Models.Movie>>));

            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(1, resultMovies.Content.Count);
        }

        [TestMethod]
        public void TestTopFiveMovies()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            var result = controller.TopFiveMovies();
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<Models.Movie>>));

            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(3, resultMovies.Content.Count);

            Assert.AreEqual("Incredibles 2", resultMovies.Content[0].Title);
            Assert.AreEqual("The Dark Knight", resultMovies.Content[1].Title);
            Assert.AreEqual("Mission Impossible - Fallout", resultMovies.Content[2].Title);
        }

        [TestMethod]
        public void TestTopFiveMoviesForUser_NotFound()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            var result = controller.TopFiveMoviesByUser(8);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestTopFiveMoviesForUser_Found_Tim()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            var result = controller.TopFiveMoviesByUser(1);
            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(3, resultMovies.Content.Count);

            Assert.AreEqual("Incredibles 2", resultMovies.Content[0].Title);
            Assert.AreEqual("The Dark Knight", resultMovies.Content[1].Title);
            Assert.AreEqual("Mission Impossible - Fallout", resultMovies.Content[2].Title);
        }

        [TestMethod]
        public void TestTopFiveMoviesForUser_Found_Paul()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            var result = controller.TopFiveMoviesByUser(2);
            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(3, resultMovies.Content.Count);

            Assert.AreEqual("The Dark Knight", resultMovies.Content[0].Title);
            Assert.AreEqual("Incredibles 2", resultMovies.Content[1].Title);
            Assert.AreEqual("Mission Impossible - Fallout", resultMovies.Content[2].Title);
        }

        [TestMethod]
        public void TestTopFiveMoviesForUser_Found_Tracey()
        {
            var controller = new MoviesService.Controllers.MoviesController(new TestMoviesServiceContext());

            var result = controller.TopFiveMoviesByUser(3);
            var resultMovies = result as OkNegotiatedContentResult<List<Models.Movie>>;
            Assert.AreEqual(3, resultMovies.Content.Count);

            Assert.AreEqual("Incredibles 2", resultMovies.Content[0].Title);
            Assert.AreEqual("Mission Impossible - Fallout", resultMovies.Content[1].Title);
            Assert.AreEqual("The Dark Knight", resultMovies.Content[2].Title);
            
        }
    }
}
