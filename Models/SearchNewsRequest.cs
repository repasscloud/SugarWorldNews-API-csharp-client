using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SugarWorldNewsApiClient.Constants;

namespace SugarWorldNewsApiClient.Models
{
	public class SearchNewsRequest
	{
		public string? Text { get; set; }
		public List<string>? SourceCountries = new List<string>();
		public string? Language { get; set; }

		[Range(-1, 1, ErrorMessage = "Number must be between -1 and 1.")]
		public double MinSentiment { get; set; } = -0.8;

		[Range(-1, 1, ErrorMessage = "Number must be between -1 and 1.")]
        public double MaxSentiment { get; set; } = 0.8;
		public DateTime? EarliestPublishDate { get; set; }
		public DateTime? LatestPublishDate { get; set; }
		public List<string>? NewsSources = new List<string>();
		public List<string>? Authors = new List<string>();
		public string? Entities { get; set; }
		public List<string>? LocationFilter = new List<string>();
        public Sort? Sort { get; set; }
		public SortDirection? SortDirection { get; set; }

		[Range(0, 1000, ErrorMessage = "Number must be between 0 and 1000.")]
		public int Offset { get; set; }

        [Range(1, 100, ErrorMessage = "Number must be between 1 and 100.")]
        public int Number { get; set; }

		public string? ApiKey { get; set; }
    }
}
