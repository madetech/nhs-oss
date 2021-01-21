using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Notify.Interfaces;
using Microsoft.Extensions.Options;

namespace BloodAndPlasma
{
    public class NotifyDonor
    {
        private readonly IAsyncNotificationClient _client;
        private readonly NotifyDonorSettings _settings;

        public NotifyDonor(IAsyncNotificationClient client, IOptions<NotifyDonorSettings> notifyDonorSettings)
        {
            _client = client;
            _settings = notifyDonorSettings.Value;
        }

        [FunctionName("NotifyDonor")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<SendEmail>(requestBody);

            var personalisation = new Dictionary<string, dynamic> { { "first_name", data.FirstName }, { "donor_id", data.DonorId } };

            var response = await _client.SendEmailAsync(data.Email, _settings.EmailTemplate, personalisation);

            log.LogInformation("Sent email");
            log.LogInformation(JsonConvert.SerializeObject(response));

            return new OkObjectResult(new { message = "email sent" });
        }

        private class SendEmail
        {
            public string Email { get; set; }
            public string FirstName { get; set; }
            public int DonorId { get; set; }
        }
    }
}
