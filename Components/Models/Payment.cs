using Newtonsoft.Json;

namespace VoyageVault.Components.Models
{

    public class Payment
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}

