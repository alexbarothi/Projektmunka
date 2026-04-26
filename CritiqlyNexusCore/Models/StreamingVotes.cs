using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CritiqlyNexusCore.Models
{
    public class StreamingVotes
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("movie_id")]
        public int MovieId { get; set; }

        [JsonPropertyName("netflix")]
        public int Netflix { get; set; }

        [JsonPropertyName("disney")]
        public int Disney { get; set; }

        [JsonPropertyName("hbo")]
        public int Hbo { get; set; }

        [JsonPropertyName("apple")]
        public int Apple { get; set; }

        [JsonPropertyName("amazon")]
        public int Amazon { get; set; }

        [JsonPropertyName("verified_platform")]
        public string? VerifiedPlatform { get; set; }

        [JsonPropertyName("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
