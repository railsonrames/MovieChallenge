using Newtonsoft.Json;
using System;

namespace MovieChallengeCore.Entities
{
    public class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
        [JsonProperty("genre_ids")]
        public int[] GenreIds { get; set; }
        public string Genre { get; set; }

        public override string ToString()
        {
            return string.Concat(Title, " ", ReleaseDate.ToString("dd/MM/yyyy"));
        }
    }
}
