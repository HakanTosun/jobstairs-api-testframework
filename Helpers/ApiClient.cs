using RestSharp;
using System.Text.Json;

namespace JobStairsSpecflowTest.Helpers
{
    public static class ApiClient
    {
        private static readonly RestClient _client = new RestClient("http://localhost:5122");

        public static RestResponse Post(string endpoint, object body)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("Content-Type", "application/json");

            var jsonData = JsonSerializer.Serialize(body);
            request.AddJsonBody((object)body);

            return _client.Execute(request);
        }
    }
}