using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class ApiSystemAchievement
    {
        [JsonPropertyName("achievement_key")]
        public string AchievementKey { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("display_value")]
        public string? DisplayValue { get; set; }

        [JsonPropertyName("icon_url")]
        public string? IconUrl { get; set; }

        [JsonPropertyName("game_id")]
        public string? GameId { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        [JsonPropertyName("is_automated")]
        public bool IsAutomated { get; set; }

        [JsonPropertyName("is_singular_holder")]
        public bool IsSingularHolder { get; set; }

        [JsonPropertyName("holder_count")]
        public int HolderCount { get; set; }

        [JsonPropertyName("holder_percentage")]
        public double? HolderPercentage { get; set; }
    }
} 