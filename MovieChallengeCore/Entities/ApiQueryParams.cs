using Refit;

namespace MovieChallengeCore.Entities
{
    public class ApiQueryParams
    {
        [AliasAs("api_key")]
        public string ApiKey { get; set; }
        [AliasAs("page")]
        public int Page { get; set; }
    }
}
