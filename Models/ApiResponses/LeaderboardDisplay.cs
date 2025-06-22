using System;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class LeaderboardDisplay
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
        
        [JsonPropertyName("elo")]
        public int? Elo { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("draws")]
        public int Draws { get; set; }

        [JsonPropertyName("matches_played")]
        public int MatchesPlayed { get; set; }

        [JsonPropertyName("win_percentage")]
        public double? WinPercentage { get; set; }

        [JsonPropertyName("last_played")]
        public DateTime? LastPlayed { get; set; }
        
        // Player-specific properties
        [JsonPropertyName("gems")]
        public long? Gems { get; set; }

        [JsonPropertyName("xp")]
        public long? Xp { get; set; }

        [JsonPropertyName("achievement_count")]
        public int? AchievementCount { get; set; }

        // Locally calculated properties
        public bool? IsActive { get; set; }
        public int? ActiveRank { get; set; }
    }
} 