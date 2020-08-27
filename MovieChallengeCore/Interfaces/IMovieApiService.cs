using Refit;
using System.Threading.Tasks;

namespace MovieChallengeCore.Interfaces
{
    public interface IMovieApiService
    {
        [Get("/movie/upcoming")]
        Task<string> GetMovies(string api_key, int page);
        [Get("/genre/movie/list")]
        Task<string> GetGenres(string api_key);
    }
}
