using Microsoft.Azure.WebJobs;
using Notify.Client;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notify.Interfaces;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(BloodAndPlasma.Startup))]
namespace BloodAndPlasma
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<NotifyDonorSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("NotifyDonorSettings").Bind(settings);
                });
            builder.Services.AddTransient<IAsyncNotificationClient, NotificationClient>(sp => 
                {
                    var settings = sp.GetRequiredService<IOptions<NotifyDonorSettings>>();
                    return new NotificationClient(settings.Value.NotifyApiKey);
                });
        }
    }
}
