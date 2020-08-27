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
            var client = RestService.For<IMovieApiService>(accessInfo.Uri);

            try
            {
                var genreList = await GetGenreList(client, accessInfo.Key);
                var movieList = await GetMovieList(client, accessInfo.Key);

                foreach (var movie in movieList)
                {
                    var lastGenre = movie.GenreIds.Last();
                    foreach (var genreId in movie.GenreIds)
                    {
                        movie.Genre += genreList.First(x => x.Id == genreId).Name;
                        movie.Genre += genreId == lastGenre ? "." : ", ";
                    }
                }

                return movieList.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected async Task<List<Genre>> GetGenreList(IMovieApiService client, string key)
        {
            var genreResult = await client.GetGenres(key);
            var jObjectGenres = JObject.Parse(genreResult);
            return jObjectGenres["genres"].ToObject<List<Genre>>();
        }

        protected async Task<List<Movie>> GetMovieList(IMovieApiService client, string key)
        {
            var movieResult = await client.GetMovies(key);
            var jObjectMovies = JObject.Parse(movieResult);
            return jObjectMovies["results"].ToObject<List<Movie>>();
        }

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
