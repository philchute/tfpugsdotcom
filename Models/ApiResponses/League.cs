using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class ApiLeague
    {
        [JsonPropertyName("league_id")]
        public string LeagueId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }

    public class ApiLeagueDetails : ApiLeague
    {
        [JsonPropertyName("current_week")]
        public int CurrentWeek { get; set; }

        [JsonPropertyName("standings")]
        public List<LeagueStanding> Standings { get; set; } = new List<LeagueStanding>();

        [JsonPropertyName("matches")]
        public List<LeagueMatch> Matches { get; set; } = new List<LeagueMatch>();
    }

    public class LeagueStanding
    {
        [JsonPropertyName("team_id")]
        public string TeamId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("draws")]
        public int Draws { get; set; }
    }

    public class LeagueMatch
    {
        [JsonPropertyName("league_match_id")]
        public string LeagueMatchId { get; set; } = string.Empty;

        [JsonPropertyName("match_display_id")]
        public string? MatchDisplayId { get; set; }

        [JsonPropertyName("week")]
        public int Week { get; set; }

        [JsonPropertyName("team1_id")]
        public string Team1Id { get; set; } = string.Empty;

        [JsonPropertyName("team2_id")]
        public string Team2Id { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("winner_team_id")]
        public string? WinnerTeamId { get; set; }

        [JsonPropertyName("score_team1")]
        public int? ScoreTeam1 { get; set; }

        [JsonPropertyName("score_team2")]
        public int? ScoreTeam2 { get; set; }
    }
} 