using Newtonsoft.Json;

namespace VoyageVault.Components.Models
{
    public class VoyageExpenses
    {
        [JsonProperty("expenses")]
        public List<Payment> Expenses { get; set; } = new List<Payment>();
    }
}
