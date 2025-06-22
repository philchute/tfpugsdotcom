using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class RecentMatchesApiResponse
    {
        [JsonPropertyName("matches")]
        public List<Match> Matches { get; set; } = new List<Match>();

        [JsonPropertyName("player_details")]
        public Dictionary<string, PlayerDetail> PlayerDetails { get; set; } = new Dictionary<string, PlayerDetail>();

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

    public class PlayerDetail
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }
    }

    public class Match
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

        [JsonPropertyName("map_name")]
        public string MapName { get; set; } = string.Empty;

        [JsonPropertyName("team1_players")]
        public string Team1Players { get; set; } = string.Empty; // Comma-separated Discord IDs

        [JsonPropertyName("team2_players")]
        public string Team2Players { get; set; } = string.Empty; // Comma-separated Discord IDs

        [JsonPropertyName("team1_id")]
        public string? Team1Id { get; set; }

        [JsonPropertyName("team1_name")]
        public string? Team1Name { get; set; }

        [JsonPropertyName("team2_id")]
        public string? Team2Id { get; set; }

        [JsonPropertyName("team2_name")]
        public string? Team2Name { get; set; }

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

        [JsonPropertyName("tournament")]
        public TournamentInfo? Tournament { get; set; }

        [JsonPropertyName("league")]
        public LeagueInfo? League { get; set; }
    }

    public class TournamentInfo
    {
        [JsonPropertyName("tournament_id")]
        public string TournamentId { get; set; } = string.Empty;

        [JsonPropertyName("display_id")]
        public string DisplayId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("round")]
        public string? Round { get; set; }
    }

    public class LeagueInfo
    {
        [JsonPropertyName("league_id")]
        public string LeagueId { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("week")]
        public int? Week { get; set; }
    }
} 