using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    // For GET /api/racemaps/toptimes
    public class RaceRecord
    {
        [JsonPropertyName("map_name")]
        public string MapName { get; set; } = string.Empty;

        [JsonPropertyName("player_discord_id")]
        public long PlayerDiscordId { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; } = string.Empty;

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("best_time_seconds")]
        public double BestTimeSeconds { get; set; }

        [JsonPropertyName("set_at")]
        public DateTime SetAt { get; set; }
    }

    // For GET /api/racemaps/{map_name}/leaderboard
    public class RaceMapLeaderboardEntry
    {
        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; } = string.Empty;

        [JsonPropertyName("player_discord_id")]
        public long PlayerDiscordId { get; set; }

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("best_time_seconds")]
        public double BestTimeSeconds { get; set; }

        [JsonPropertyName("set_at")]
        public DateTime SetAt { get; set; }
    }
} 