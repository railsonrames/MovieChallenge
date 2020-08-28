using MovieChallengeCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieChallengeCore.Interfaces
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMovies(int page);
    }
}
