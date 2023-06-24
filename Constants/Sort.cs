using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SugarWorldNewsApiClient.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Sort
    {
        [EnumMember(Value = "publish-time")]
        PublishTime,
        [EnumMember(Value = "sentiment")]
        Sentiment
    }
}

