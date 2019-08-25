using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MoviesService.DAL;

namespace MoviesService.Tests
{
    public class TestMoviesServiceContext : IMoviesServiceContext
    {
        public TestMoviesServiceContext()
        {
            Movies = new TestDbSet<Movie>();
            Movies.Add(new Movie() { Id = 1, Title = "Incredibles 2", Genre = "Animated", YearOfRelease = 2018, RunningTime = 125 });
            Movies.Add(new Movie() { Id = 2, Title = "The Dark Knight", Genre = "Action", YearOfRelease = 2008, RunningTime = 152 });
            Movies.Add(new Movie() { Id = 3, Title = "Mission Impossible - Fallout", Genre = "Action", YearOfRelease = 2018, RunningTime = 148 });
            Movies.Add(new Movie() { Id = 4, Title = "Black Panther", Genre = "Action", YearOfRelease = 2018, RunningTime = 135 });
            Movies.Add(new Movie() { Id = 5, Title = "Ocean's 8", Genre = "Comedy", YearOfRelease = 2018, RunningTime = 111 });

            RegisteredUsers = new TestDbSet<RegisteredUser>();
            RegisteredUsers.Add(new RegisteredUser() { Id = 1, Firstname = "Tim", Lastname = "Pepper" });
            RegisteredUsers.Add(new RegisteredUser() { Id = 2, Firstname = "Paul", Lastname = "Rogers" });
            RegisteredUsers.Add(new RegisteredUser() { Id = 3, Firstname = "Tracey", Lastname = "Peters" });

            UserMovieRatings = new TestDbSet<MovieUserRating>();
            UserMovieRatings.Add(new MovieUserRating() { Id = 1, MovieId = 1, UserId = 1, Rating = 5 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 2, MovieId = 1, UserId = 2, Rating = 3 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 3, MovieId = 1, UserId = 3, Rating = 2 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 4, MovieId = 2, UserId = 1, Rating = 3 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 5, MovieId = 2, UserId = 2, Rating = 4 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 6, MovieId = 2, UserId = 3, Rating = 1 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 7, MovieId = 3, UserId = 1, Rating = 1 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 8, MovieId = 3, UserId = 2, Rating = 2 });
            UserMovieRatings.Add(new MovieUserRating() { Id = 9, MovieId = 3, UserId = 3, Rating = 2 });
        }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<MovieUserRating> UserMovieRatings { get; set; }

        public void Dispose()
        {
        }

        public int SaveChanges()
        {

            return 0;
        }
    }
}
