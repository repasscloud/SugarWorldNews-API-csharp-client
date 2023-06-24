using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using SugarWorldNewsApiClient.Constants;
using SugarWorldNewsApiClient.Models;

namespace SugarWorldNewsApiClient
{
    /// <summary>
    /// Use this to get results from https://api.worldnewsapi.com/
    /// </summary>
    public class SugarWorldNewsApiClient
    {
        private string BASE_URL = "https://api.worldnewsapi.com/";
        private readonly HttpClient _httpClient;
        private string ApiKey;

        public SugarWorldNewsApiClient(string apiKey, HttpClient? httpClient)
        {
            ApiKey = apiKey;

            _httpClient = httpClient!;
            _httpClient.DefaultRequestHeaders.Add("user-agent", "SugarWorldNews-API-Client/0.1");
            _httpClient.DefaultRequestHeaders.Add("x-api-key", ApiKey);
        }

        public async Task<GeoCoordinatesResponse> GetGeoCoordinatesResponseAsync(GeoCoordinatesRequest request)
        {
            // build the querystring
            var queryParams = new List<string>();

            // search city, country
            if (!string.IsNullOrEmpty(request?.Location))
                queryParams.Add("location=" + request?.Location);

            // api-key
            if (!string.IsNullOrWhiteSpace(request?.ApiKey))
                queryParams.Add("api-key=" + request.ApiKey);

            // join values
            var queryString = string.Join("&", queryParams.ToArray());

            return await MakeGeoCoordinatesRequestAsync("search-news", queryString);
        }

        private async Task<GeoCoordinatesResponse> MakeGeoCoordinatesRequestAsync(string endpoint, string queryString)
        {
            // create return object
            var articlesResponse = new GeoCoordinatesResponse();

            // make hte http request
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + queryString);
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content!.ReadAsStringAsync();
                articlesResponse.Status = Statuses.Ok;
                if (!string.IsNullOrWhiteSpace(json))
                {
                    // convert json to object
                    var apiResponse = JsonSerializer.Deserialize<GeoCoordinatesResponse>(json);
                    articlesResponse.Latitude = apiResponse!.Latitude;
                    articlesResponse.Longitude = apiResponse!.Longitude;
                    articlesResponse.City = apiResponse?.City;
                }
                else
                {
                    articlesResponse.Status = Statuses.Error;
                    articlesResponse.Longitude = 0.0;
                    articlesResponse.Latitude = 0.0;
                    articlesResponse.City = "No Data";
                }
            }
            else
            {
                articlesResponse.Status = Statuses.Error;
                articlesResponse.Longitude = 0.0;
                articlesResponse.Latitude = 0.0;
                articlesResponse.City = "No Data";
            }

            return articlesResponse;
        }


        public async Task<SearchNewsResponse> GetSearchNewsResponseAsync(SearchNewsRequest request)
        {
            // build the querystring
            var queryParams = new List<string>();

            // text
            if (!string.IsNullOrWhiteSpace(request?.Text))
                queryParams.Add("text=" + request.Text);

            // source-countries
            if (request?.SourceCountries?.Count > 0)
                queryParams.Add("source-countries=" + string.Join(",", request.SourceCountries));

            // language
            if (!string.IsNullOrWhiteSpace(request?.Language))
                queryParams.Add("language=" + request.Language);

            // min-sentiment
            if (request?.MinSentiment >= -1 && request.MinSentiment <= 1)
                queryParams.Add("min-sentiment=" + request.MinSentiment);

            // max-sentiment
            if (request?.MaxSentiment >= -1 && request.MaxSentiment <= 1)
                queryParams.Add("max-sentiment=" + request.MaxSentiment);

            // earliest-publish-date
            if (request?.EarliestPublishDate != null)
                queryParams.Add("earliest-publish-date=" + request?.EarliestPublishDate?.ToString("yyyy-MM-dd"));

            // latest-publish-date
            if (request?.LatestPublishDate != null)
                queryParams.Add("latest-publish-date=" + request?.LatestPublishDate?.ToString("yyyy-MM-dd"));

            // news-sources
            if (request?.NewsSources?.Count > 0)
                queryParams.Add("news-sources=" + string.Join(",", request.NewsSources));

            // authors
            if (request?.Authors?.Count > 0)
                queryParams.Add("authors=" + string.Join(",", request.Authors));

            // entities
            if (!string.IsNullOrWhiteSpace(request?.Entities))
                queryParams.Add("entities=" + request.Entities);

            // location-filter
            if (request?.LocationFilter?.Count > 0)
                queryParams.Add("location-filter=" + string.Join(",", request.LocationFilter));

            // sort
            if ((bool)(request?.Sort.HasValue ?? false))
                queryParams.Add("sort=" + request?.Sort!.Value.ToString().ToLowerInvariant());

            // sort-direction
            if ((bool)(request?.SortDirection.HasValue ?? false))
                queryParams.Add("sort-direction=" + request?.Sort!.Value.ToString().ToLowerInvariant());

            // offset
            if (request?.Offset >= 0 && request?.Offset <= 1000)
                queryParams.Add("offset=" + request.Offset.ToString());

            // number
            if (request?.Number >= 1 && request?.Number <= 100)
                queryParams.Add("offset=" + request.Number.ToString());

            // api-key
            if (!string.IsNullOrWhiteSpace(request?.ApiKey))
                queryParams.Add("api-key=" + request.ApiKey);

            // join values
            var queryString = string.Join("&", queryParams.ToArray());

            return await MakeSearchNewsRequestAsync("search-news", queryString);
        }

        private async Task<SearchNewsResponse> MakeSearchNewsRequestAsync(string endpoint, string queryString)
        {
            // create return object
            var articlesResponse = new SearchNewsResponse();

            // make hte http request
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + queryString);
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content!.ReadAsStringAsync();
                articlesResponse.Status = Statuses.Ok;
                if (!string.IsNullOrWhiteSpace(json))
                {
                    // convert json to object
                    var apiResponse = JsonSerializer.Deserialize<SearchNewsResponse>(json);
                    articlesResponse.Offset = (int)(apiResponse?.Offset ?? 0);
                    articlesResponse.Number = (int)(apiResponse?.Number ?? 0);
                    articlesResponse.Available = (int)(apiResponse?.Available ?? 0);
                    articlesResponse.News = apiResponse?.News;
                }
                else
                {
                    articlesResponse.Status = Statuses.Error;
                    articlesResponse.Offset = 0;
                    articlesResponse.Number = 0;
                    articlesResponse.Available = 0;
                    articlesResponse.News = new List<News>();
                }
            }
            else
            {
                articlesResponse.Status = Statuses.Error;
                articlesResponse.Offset = 0;
                articlesResponse.Number = 0;
                articlesResponse.Available = 0;
                articlesResponse.News = new List<News>();
            }

            return articlesResponse;
        }


        public async Task<ExtractNewsResponse> GetExtractNewsResponseAsync(ExtractNewsRequest request)
        {
            // build the querystring
            var queryParams = new List<string>();

            // url
            if (!string.IsNullOrWhiteSpace(request.Url))
            {
                string encodedString = Uri.EscapeDataString(request.Url);
                queryParams.Add("url=" + encodedString);
            }

            // analyze
            if (!string.IsNullOrWhiteSpace(request.Analyze.ToString()))
            {
                queryParams.Add("analyze=" + request.Analyze.ToString().ToLower());
            }
            else
            {
                queryParams.Add("analyze=false");
            }

            // join values
            var queryString = string.Join("&", queryParams.ToArray());

            return await MakeExtractNewsRequestAsync("extract_news", queryString);
        }

        private async Task<ExtractNewsResponse> MakeExtractNewsRequestAsync(string endpoint, string queryString)
        {
            // create return object
            var articlesResponse = new ExtractNewsResponse();

            // make the http request
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, BASE_URL + endpoint + "?" + queryString);
            var httpResponse = await _httpClient.SendAsync(httpRequest);

            if (httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content!.ReadAsStringAsync();
                articlesResponse.Status = Statuses.Ok;
                if (!string.IsNullOrWhiteSpace(json))
                {
                    // convert json to object
                    var apiResponse = JsonSerializer.Deserialize<ExtractNewsResponse>(json);
                    articlesResponse.Author = apiResponse?.Author;
                    articlesResponse.Image = apiResponse?.Image;
                    articlesResponse.Language = apiResponse?.Language;
                    articlesResponse.Sentiment = apiResponse!.Sentiment;
                    articlesResponse.SourceCountry = apiResponse?.SourceCountry;
                    articlesResponse.Text = apiResponse?.Text;
                    articlesResponse.Title = apiResponse?.Title;
                    articlesResponse.Url = apiResponse?.Url;
                }
                else
                {
                    articlesResponse.Status = Statuses.Error;
                    articlesResponse.Text = httpResponse.StatusCode.ToString();
                }
            }
            else
            {
                articlesResponse.Status = Statuses.Error;
                articlesResponse.Text = httpResponse.StatusCode.ToString();
            }

            return articlesResponse;
        }
    }
}

