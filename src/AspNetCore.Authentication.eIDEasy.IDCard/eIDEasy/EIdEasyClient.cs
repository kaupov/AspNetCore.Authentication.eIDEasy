using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCore.Authentication.eIDEasy.IDCard.eIDEasy
{
    public class EIdEasyClient
    {
        private readonly HttpClient _httpClient;

        public EIdEasyClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public EIdEasyIdCardOptions Options { get; set; }

        public async Task<UserData> PostIdCardComplete(string token)
        {
            var uriBuilder = new UriBuilder("https://id.eideasy.com");
            uriBuilder.Path += $"api/identity/{Options.ClientId}/id-card/complete";

            var requestObject = new
            {
                Secret = Options.ClientSecret,
                Token = token,
                Lang = "et",
                Country = Options.Country.ToUpperInvariant()
            };

            var jsonString = JsonSerializer.Serialize(requestObject,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var requestContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync(uriBuilder.Uri, requestContent);

            responseMessage.EnsureSuccessStatusCode();

            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<UserData>(responseString);

            return userData;
        }
    }
}
