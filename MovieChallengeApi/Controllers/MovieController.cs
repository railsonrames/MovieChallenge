using Microsoft.AspNetCore.Mvc;
using MovieChallengeApi.Models;
using MovieChallengeCore.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MovieChallengeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieModel>> Get(int page = 1)
        {
            var result = await _movieService.GetMovies(page);
            var movieList = result.Select(x => new MovieModel
            {
                Title = x.Title,
                Genre = x.Genre,
                ReleaseDate = x.ReleaseDate.ToString("dd/MM/yyyy")
            });
            return movieList;
        }
    }
}
