using MovieChallengeCore.Entities;
using MovieChallengeCore.Interfaces;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieChallengeCore.Services
{
    public class MovieService : IMovieService
    {
        public async Task<List<Movie>> GetMovies()
        {
            try
            {
                var client = RestService.For<IMovieApiService>("https://api.themoviedb.org/3");
                var genreResult = await client.GetGenres("6ccc4110e732d20c6731baad9e5db5df");
                var jObjectGenres = JObject.Parse(genreResult);
                var genreList = jObjectGenres["genres"].ToObject<Genre[]>();
                var movieResult = await client.GetMovies("6ccc4110e732d20c6731baad9e5db5df");
                var jObjectMovies = JObject.Parse(movieResult);
                var movies = jObjectMovies["results"].ToObject<Movie[]>();
                foreach (var movie in movies)
                {
                    var lastGenre = movie.GenreIds.Last();
                    foreach (var genreId in movie.GenreIds)
                    {
                        movie.Genre += genreList.First(x => x.Id == genreId).Name;
                        movie.Genre += genreId == lastGenre ? "." : ", ";
                    }
                }
                return movies.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
