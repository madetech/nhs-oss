using System.Text.Json.Serialization;

namespace BloodAndPlasma.Models
{
    public class Personalisation
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("donor_id")]
        public int DonorId { get; set; }
    }
}
