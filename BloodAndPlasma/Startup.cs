using Microsoft.Azure.WebJobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(BloodAndPlasma.Startup))]
namespace BloodAndPlasma
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();
            builder.Services.AddOptions<NotifyDonorSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(nameof(NotifyDonorSettings)).Bind(settings);
                });
            builder.Services.AddHttpClient(nameof(NotifyDonor), client => {
                client.BaseAddress = new Uri(context.Configuration[$"{nameof(NotifyDonorSettings)}:{nameof(NotifyDonorSettings.BaseAddress)}"]);
            });
        }
    }
}
