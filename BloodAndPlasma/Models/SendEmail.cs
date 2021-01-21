using System.Text.Json.Serialization;

namespace BloodAndPlasma.Models
{
    public class SendEmail
    {
        [JsonPropertyName("email_address")]
        public string Email { get; set; }

        [JsonPropertyName("template_id")]
        public string TemplateId { get; set; }

        [JsonPropertyName("personalisation")]
        public Personalisation Personalisation { get; set; }

        public static SendEmail Create(UserData userData, string templateId)
        {
            return new SendEmail
            {
                Email = userData.Email,
                TemplateId = templateId,
                Personalisation = new Personalisation
                {
                    FirstName = userData.FirstName,
                    DonorId = userData.DonorId
                }
            };
        }
    }
}
