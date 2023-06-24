using System.Text.Json.Serialization;
using SugarWorldNewsApiClient.Constants;

namespace SugarWorldNewsApiClient.Models
{
	public class GeoCoordinatesResponse
	{
		[JsonPropertyName("status")]
		public Statuses Status { get; set; }
		[JsonPropertyName("latitude")]
		public double Latitude { get; set; }
		[JsonPropertyName("longitude")]
		public double Longitude { get; set; }
		[JsonPropertyName("city")]
		public string? City { get; set; }
	}
}

