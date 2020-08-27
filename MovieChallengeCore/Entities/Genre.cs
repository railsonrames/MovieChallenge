using Newtonsoft.Json;

namespace MovieChallengeCore.Entities
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
