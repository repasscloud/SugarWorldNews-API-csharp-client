using System.Text.Json.Serialization;

namespace SugarWorldNewsApiClient.Models
{
	public class News
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("author")]
        public string? Author { get; set; }

        [JsonPropertyName("language")]
        public string? Language { get; set; }

        [JsonPropertyName("source_country")]
        public string? SourceCountry { get; set; }

        [JsonPropertyName("sentiment")]
        public double Sentiment { get; set; }
    }
}

