using System.Collections.Generic;
using System.Text.Json.Serialization;
using SugarWorldNewsApiClient.Constants;

namespace SugarWorldNewsApiClient.Models
{
	public class SearchNewsResponse
	{
        [JsonPropertyName("status")]
        public Statuses Status { get; set; }

        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("available")]
        public int Available { get; set; }

        [JsonPropertyName("news")]
        public List<News>? News { get; set; }
    }
}

