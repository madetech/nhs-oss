using BloodAndPlasma;
using BloodAndPlasma.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpExtensions
    {
        public static async Task<T> ReadAsync<T>(this HttpRequest request)
        {
            using var streamReader = new StreamReader(request.Body);
            var requestBody = await streamReader.ReadToEndAsync();
            return JsonSerializer.Deserialize<T>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static async Task<HttpResponseMessage> SendToNotifyAsync(this HttpClient client, SendEmail sendEmail, NotifyDonorSettings settings)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(settings.EmailEndpoint, UriKind.Relative),
                Method = HttpMethod.Post,
                Content = JsonContent.Create(sendEmail)
            };

            var jwtToken = JwtTokenFactory.Create(settings.SecretKey, settings.Iss);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            return await client.SendAsync(request);
        }
    }
}
