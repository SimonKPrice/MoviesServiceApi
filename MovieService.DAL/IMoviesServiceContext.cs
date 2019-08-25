using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesService.DAL
{
    public interface IMoviesServiceContext : IDisposable
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<RegisteredUser> RegisteredUsers { get; set; }
        DbSet<MovieUserRating> UserMovieRatings { get; set; }

        int SaveChanges();
    }
}
