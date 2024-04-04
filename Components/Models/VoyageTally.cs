using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace VoyageVault.Components.Models
{
    [JsonSerializable(typeof(VoyageTally))]
    internal partial class MyJsonContext : JsonSerializerContext { }

    public class VoyageTally
    {
        public List<Payment> Expenses { get; set; } = new List<Payment>();

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
    }
}
