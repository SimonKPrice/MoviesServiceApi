using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoviesService;
using MoviesService.DAL;

namespace MoviesService.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private IMoviesServiceContext db = new MoviesServiceContext();

        public MoviesController()
        {
        }

        public MoviesController(IMoviesServiceContext context)
        {
            db = context;
        }


        [HttpGet]
        [Route("Query")]
        public IHttpActionResult Query([FromBody]Models.Filter filter)
        {
            if (filter != null && (!string.IsNullOrEmpty(filter.Title) || filter.YearOfRelease.HasValue || filter.Genres?.Count > 0))
            {
                List<Models.Movie> movies = db.Movies.Where(m=>((!string.IsNullOrEmpty(filter.Title) && m.Title.Contains(filter.Title)) || string.IsNullOrEmpty(filter.Title)) &&
                                                            ((filter.YearOfRelease.HasValue && m.YearOfRelease == filter.YearOfRelease.Value) || !filter.YearOfRelease.HasValue) && 
                                                            ((filter.Genres !=null && filter.Genres.Contains(m.Genre)) || filter.Genres==null || filter.Genres.Count==0))
                                                        .Select(m=>new Models.Movie()
                                                        {
                                                            Id = m.Id,
                                                            Title=m.Title,
                                                            RunningTime = m.RunningTime,
                                                            YearOfRelease = m.YearOfRelease,
                                                            AverageRating = db.UserMovieRatings.Count(mr => mr.MovieId == m.Id)>0 ? RoundToPointFive((float)db.UserMovieRatings.Where(mr => mr.MovieId == m.Id).Average(mr=>mr.Rating)) : (float?)null
                                                        }).ToList();

                if (movies?.Count > 0)
                    return Ok(movies);
                else
                    return NotFound();
                                                    
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("TopFiveMovies")]
        public IHttpActionResult TopFiveMovies()
        {
            List<Models.Movie> movies = db.Movies.Select(m => new Models.Movie()
            {
                Id = m.Id,
                Title = m.Title,
                RunningTime = m.RunningTime,
                YearOfRelease = m.YearOfRelease,
                AverageRating = db.UserMovieRatings.Count(mr => mr.MovieId == m.Id) > 0 ? (float)db.UserMovieRatings.Where(mr => mr.MovieId == m.Id).Average(mr => mr.Rating) : (float?)null
            }).OrderByDescending(r => r.AverageRating).ThenBy(m => m.Title)
            .Where(r=>r.AverageRating.HasValue)
            .Take(5).ToList()
            .Select(r => { r.AverageRating = RoundToPointFive(r.AverageRating.Value); return r; })
            .ToList();

            if (movies?.Count > 0)
                return Ok(movies);
            else
                return NotFound();
        }

        // api/movies/TopFiveMoviesByUser
        [HttpGet]
        [Route("TopFiveMoviesByUser/{Id:int}")]
        public IHttpActionResult TopFiveMoviesByUser(int Id)
        {
            if (Id > 0)
            {
                List<Models.Movie> movies = (from m in db.Movies
                                             join mr in db.UserMovieRatings on m.Id equals mr.MovieId
                                             where mr.UserId == Id
                                             orderby mr.Rating descending, m.Title ascending
                                             select new Models.Movie()
                                             {
                                                 Id = m.Id,
                                                 Title = m.Title,
                                                 RunningTime = m.RunningTime,
                                                 YearOfRelease = m.YearOfRelease,
                                                 AverageRating = db.UserMovieRatings.Count(mr => mr.MovieId == m.Id) > 0 ? (float)db.UserMovieRatings.Where(mr => mr.MovieId == m.Id).Average(mr => mr.Rating) : (float?)null
                                             })
                                             .Where(r => r.AverageRating.HasValue)
                                             .Take(5).ToList()
                                             .Select(r => { r.AverageRating = RoundToPointFive(r.AverageRating.Value); return r; })
                                             .ToList();

                if (movies?.Count > 0)
                    return Ok(movies);
                else
                    return NotFound();
            }
            else
                return BadRequest();
        }

        // api/movies/addusermovierating
        [HttpPost]
        [Route("AddMovieRating")]
        public IHttpActionResult AddMovieRating([FromBody]Models.UserMovieRating movieRating)
        {
            if (ModelState.IsValid)
            {
                // Check if movie exists
                Movie movie = db.Movies.Where(m => m.Id == movieRating.MovieId).FirstOrDefault();

                if (movie == null)
                    return NotFound();

                // Check if user exists
                RegisteredUser user = db.RegisteredUsers.Where(u => u.Id == movieRating.UserId).FirstOrDefault();

                if (user == null)
                    return NotFound();

                // Get any existing movie rating for this user and movie combination
                MovieUserRating movieuserrating = db.UserMovieRatings.Where(mr => mr.UserId == movieRating.UserId && mr.MovieId == movieRating.MovieId).FirstOrDefault();

                if (movieuserrating != null)
                    movieuserrating.Rating = movieRating.Rating;
                else
                {
                    movieuserrating = new MovieUserRating() { MovieId = movieRating.MovieId, UserId = movieRating.UserId, Rating = movieRating.Rating };
                    db.UserMovieRatings.Add(movieuserrating);
                }

                db.SaveChanges();

                return Ok();
            }
            else
                return BadRequest();
        }


        private float RoundToPointFive(float value)
        {
            var rn = value % 0.5 == 0 ? 1 : 0;
            return (float)Math.Round(value, rn);
        }
    }
}
