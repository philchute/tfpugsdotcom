using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class LeaderboardApiResponse
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("metric")]
        public string Metric { get; set; } = string.Empty;

        [JsonPropertyName("leaderboard")]
        public List<LeaderboardEntry> Leaderboard { get; set; } = new List<LeaderboardEntry>();

        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("current_page")]
        public int CurrentPage { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("next_page_url")]
        public string? NextPageUrl { get; set; }

        [JsonPropertyName("prev_page_url")]
        public string? PrevPageUrl { get; set; }
    }

    public class LeaderboardEntry
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("discord_id")]
        public string DiscordId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("elo")]
        public int Elo { get; set; }

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

        [JsonPropertyName("gems")]
        public long? Gems { get; set; }

        [JsonPropertyName("xp")]
        public long? Xp { get; set; }

        [JsonPropertyName("achievement_count")]
        public int? AchievementCount { get; set; }
    }
} 