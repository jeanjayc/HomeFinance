using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class AlterarValorPago
{
    [JsonProperty("price")]
    public decimal Price { get; set; }
    [JsonProperty("status")]
    public bool Status { get; set; }
}
