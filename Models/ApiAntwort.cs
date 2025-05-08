using System.Text.Json.Serialization;

namespace JobStairsSpecflowTest.Models
{
    public class ApiAntwort
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
    }
}