using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SugarWorldNewsApiClient.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortDirection
	{
		ASC,
		DESC
	}
}

