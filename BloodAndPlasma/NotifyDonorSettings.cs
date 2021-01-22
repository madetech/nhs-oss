using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(BloodAndPlasma.Startup))]
namespace BloodAndPlasma
{
    public class NotifyDonorSettings
    {
        public string KeyName { get; set; }
        public string Iss { get; set; }
        public string SecretKey { get; set; }
        public string EmailTemplate { get; set; }
        public string BaseAddress { get; set; }
        public string EmailEndpoint { get; set; }
    }
}
