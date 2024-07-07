using System.Text.Json.Serialization;

namespace Dworks.Application.Requests
{
    public class ApiRequest
    {
        [JsonIgnore]
        public long UserId { get; set; }
    }
}
