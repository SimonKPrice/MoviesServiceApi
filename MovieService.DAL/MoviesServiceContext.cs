using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesService.DAL
{
    public class MoviesServiceContext : DbContext, IMoviesServiceContext
    {
        public MoviesServiceContext() : base("name=MoviesServiceContext")
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<RegisteredUser> RegisteredUsers { get; set; }
        public DbSet<MovieUserRating> UserMovieRatings { get; set; }

        public int SaveChanged()
        {
            return base.SaveChanges();
        }
    }
}
