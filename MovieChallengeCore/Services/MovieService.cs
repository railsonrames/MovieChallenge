using Microsoft.Extensions.Configuration;
using MovieChallengeCore.Entities;
using MovieChallengeCore.Interfaces;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MovieChallengeCore.Services
{
    public class MovieService : IMovieService
    {
        public async Task<List<Movie>> GetMovies()
        {
            var accessInfo = GetAccessInfo();

            try
            {
                var client = RestService.For<IMovieApiService>(accessInfo.Uri);
                var genreResult = await client.GetGenres(accessInfo.Key);
                var jObjectGenres = JObject.Parse(genreResult);
                var genreList = jObjectGenres["genres"].ToObject<Genre[]>();
                var movieResult = await client.GetMovies(accessInfo.Key);
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

        //protected async Task<List<Genre>> GetGenreList(IMovieApiService client)
        //{
        //    var genreResult = await client.GetGenres("6ccc4110e732d20c6731baad9e5db5df");
        //    var jObjectGenres = JObject.Parse(genreResult);
        //    var genreList = jObjectGenres["genres"].ToObject<Genre[]>();
        //}

        //protected async Task<List<Movie>> GetMovieList()
        //{

        //}

        protected ApiAccessInfo GetAccessInfo()
        {
            var configuration = new ConfigurationBuilder();
            var file = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configuration.AddJsonFile(file, optional: false);
            var config = configuration.Build();

            try
            {
                return new ApiAccessInfo
                {
                    Uri = config.GetSection("ApiUri").Value,
                    Key = config.GetSection("ApiKey").Value
                };
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
