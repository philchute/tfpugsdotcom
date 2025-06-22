using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class PlayerProfileApiResponse
    {
        [JsonPropertyName("discord_id")]
        public string DiscordId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("gems")]
        public long? Gems { get; set; }

        [JsonPropertyName("xp")]
        public long? Xp { get; set; }

        [JsonPropertyName("game_specific_stats")]
        public List<GameSpecificStats> GameSpecificStats { get; set; } = new List<GameSpecificStats>();

        [JsonPropertyName("achievements")]
        public List<Achievement> Achievements { get; set; } = new List<Achievement>();
    }

    public class GameSpecificStats
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("elo")]
        public int Elo { get; set; }

        [JsonPropertyName("rank")]
        public int? Rank { get; set; }

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
        public DateTime LastPlayed { get; set; }
    }

    public class Achievement
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

        [JsonPropertyName("unlocked_at")]
        public DateTime UnlockedAt { get; set; }

        [JsonPropertyName("game_id")]
        public string? GameId { get; set; }
    }

    public class EloHistoryApiResponse
    {
        [JsonPropertyName("discord_id")]
        public string DiscordId { get; set; } = string.Empty;

        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("current_elo")]
        public int CurrentElo { get; set; }

        [JsonPropertyName("history")]
        public List<EloHistoryEntry> History { get; set; } = new List<EloHistoryEntry>();
    }

    public class EloHistoryEntry
    {
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("new_elo")]
        public int NewElo { get; set; }

        [JsonPropertyName("change_amount")]
        public int ChangeAmount { get; set; }

        [JsonPropertyName("match_display_id")]
        public string? MatchDisplayId { get; set; }

        [JsonPropertyName("change_reason")]
        public string ChangeReason { get; set; } = string.Empty;
    }
} 