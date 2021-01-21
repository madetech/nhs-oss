using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BloodAndPlasma.Startup))]
namespace BloodAndPlasma
{
    public class NotifyDonorSettings
    {
        public string NotifyApiKey { get; set; }
        public string EmailTemplate { get; set; }
    }
}
