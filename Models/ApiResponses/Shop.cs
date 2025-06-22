using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class ShopItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("item_url")]
        public string? ItemUrl { get; set; }

        [JsonPropertyName("cost")]
        public int Cost { get; set; }

        [JsonPropertyName("effect_type")]
        public string EffectType { get; set; } = string.Empty;

        [JsonPropertyName("availability")]
        public string Availability { get; set; } = string.Empty;

        [JsonPropertyName("requires_param")]
        public bool RequiresParam { get; set; }

        [JsonPropertyName("param_name")]
        public string? ParamName { get; set; }
    }
} 