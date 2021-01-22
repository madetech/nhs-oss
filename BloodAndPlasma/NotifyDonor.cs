using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using BloodAndPlasma.Models;

namespace BloodAndPlasma
{
    public class NotifyDonor
    {
        private readonly HttpClient _client;
        private readonly NotifyDonorSettings _settings;

        public NotifyDonor(IHttpClientFactory client, IOptions<NotifyDonorSettings> notifyDonorSettings)
        {
            _client = client.CreateClient(nameof(NotifyDonor));
            _settings = notifyDonorSettings.Value;
        }

        [FunctionName(nameof(NotifyDonor))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Post), Route = null)] HttpRequest req,
            ILogger log)
        {
            var data = await req.ReadAsync<UserData>();
            var sendEmail = SendEmail.Create(data, _settings.EmailTemplate);

            var response = await _client.SendToNotifyAsync(sendEmail, _settings);
            var content = await response.Content.ReadAsStringAsync();

            log.LogInformation("Sent email");
            //log.LogInformation(JsonConvert.SerializeObject(response));

            return new OkObjectResult(new { message = "email sent" });
        }
    }
}
