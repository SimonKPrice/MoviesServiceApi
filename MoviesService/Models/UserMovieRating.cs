using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesService.Models
{
    public class UserMovieRating
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int UserId { get; set; }

        
        [Required]
        [Range(1,5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
    }
}