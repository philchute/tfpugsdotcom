using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class MatchDetailsApiResponse
    {
        [JsonPropertyName("match_data")]
        public MatchFullData? MatchData { get; set; }

        [JsonPropertyName("team1_players_details")]
        public List<TeamPlayerEloDetail> Team1PlayersDetails { get; set; } = new List<TeamPlayerEloDetail>();

        [JsonPropertyName("team2_players_details")]
        public List<TeamPlayerEloDetail> Team2PlayersDetails { get; set; } = new List<TeamPlayerEloDetail>();
    }

    public class MatchFullData
    {
        [JsonPropertyName("match_id")]
        public string MatchId { get; set; } = string.Empty;

        [JsonPropertyName("match_display_id")]
        public string MatchDisplayId { get; set; } = string.Empty;

        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("region_key")]
        public string RegionKey { get; set; } = string.Empty;

        [JsonPropertyName("server_internal_name")]
        public string ServerInternalName { get; set; } = string.Empty;

        [JsonPropertyName("server_display_name")]
        public string? ServerDisplayName { get; set; }

        [JsonPropertyName("map_name")]
        public string MapName { get; set; } = string.Empty;

        [JsonPropertyName("map_display_name")]
        public string? MapDisplayName { get; set; }

        [JsonPropertyName("winner_team")]
        public int? WinnerTeam { get; set; }

        [JsonPropertyName("score_team1")]
        public int? ScoreTeam1 { get; set; }

        [JsonPropertyName("score_team2")]
        public int? ScoreTeam2 { get; set; }

        [JsonPropertyName("match_start_time")]
        public DateTime MatchStartTime { get; set; }

        [JsonPropertyName("match_end_time")]
        public DateTime MatchEndTime { get; set; }

        [JsonPropertyName("duration_seconds")]
        public int? DurationSeconds { get; set; }

        [JsonPropertyName("is_moneyball")]
        public bool IsMoneyball { get; set; }

        [JsonPropertyName("is_map_test")]
        public bool IsMapTest { get; set; }

        [JsonPropertyName("is_no_elo")]
        public bool IsNoElo { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("stats_urls")]
        public List<string>? StatsUrls { get; set; }

        [JsonPropertyName("top_players_data")]
        public TopPlayersData? TopPlayersData { get; set; }
    }

    public class TopPlayersData
    {
        [JsonPropertyName("top3")]
        public List<TopPlayerEntry>? Top3 { get; set; }

        [JsonExtensionData]
        public Dictionary<string, JsonElement> OtherAwards { get; set; } = new Dictionary<string, JsonElement>();
    }

    public class TopPlayerEntry
    {
        [JsonPropertyName("playerId")]
        public int? PlayerId { get; set; }

        [JsonPropertyName("playerName")]
        public string PlayerName { get; set; } = string.Empty;

        [JsonPropertyName("steamId")]
        public string SteamId { get; set; } = string.Empty;

        [JsonPropertyName("points")]
        public double? Points { get; set; }
    }

    public class SinglePlayerAward
    {
        [JsonPropertyName("playerId")]
        public int? PlayerId { get; set; }

        [JsonPropertyName("playerName")]
        public string PlayerName { get; set; } = string.Empty;

        [JsonPropertyName("steamId")]
        public string SteamId { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public JsonElement Value { get; set; }
    }

    public class TeamPlayerEloDetail
    {
        [JsonPropertyName("discord_id")]
        public string DiscordId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("elo_change")]
        public int? EloChange { get; set; }
    }
} 