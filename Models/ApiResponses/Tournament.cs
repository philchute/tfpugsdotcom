using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class TournamentsApiResponse
    {
        [JsonPropertyName("tournaments")]
        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();

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

    public class TournamentMatch
    {
        [JsonPropertyName("match_id")]
        public string MatchId { get; set; } = string.Empty;

        [JsonPropertyName("match_display_id")]
        public string MatchDisplayId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("winner_team")]
        public int? WinnerTeam { get; set; }

        [JsonPropertyName("score_team1")]
        public int? ScoreTeam1 { get; set; }

        [JsonPropertyName("score_team2")]
        public int? ScoreTeam2 { get; set; }
    }

    public class Participant
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("avatar_display_string")]
        public string? AvatarDisplayString { get; set; }

        [JsonPropertyName("avatar_url")]
        public string? AvatarUrl { get; set; }

        [JsonPropertyName("guild_id")]
        public string? GuildId { get; set; }
    }

    public class Standing
    {
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("participant")]
        public Participant? Participant { get; set; }
    }

    public class Tournament
    {
        [JsonPropertyName("tournament_id")]
        public string TournamentId { get; set; } = string.Empty;

        [JsonPropertyName("display_id")]
        public string DisplayId { get; set; } = string.Empty;

        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("players_per_match")]
        public int PlayersPerMatch { get; set; }

        [JsonPropertyName("current_round")]
        public string? CurrentRound { get; set; }

        [JsonPropertyName("total_rounds")]
        public int TotalRounds { get; set; }

        [JsonPropertyName("bracket_type")]
        public string? BracketType { get; set; }

        [JsonPropertyName("standings")]
        public List<Standing>? Standings { get; set; }

        [JsonPropertyName("participants")]
        public List<Participant> Participants { get; set; } = new();

        [JsonPropertyName("bracket_data")]
        public BracketData? BracketData { get; set; }

        [JsonPropertyName("matches_structure")]
        public List<List<TournamentMatch>>? MatchesStructure { get; set; }

        [JsonPropertyName("winner_id")]
        public string? WinnerId { get; set; }
        
        [JsonPropertyName("winner_name")]
        public string? WinnerName { get; set; }
        
        [JsonPropertyName("winner_type")]
        public string? WinnerType { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("interaction_channel_id")]
        public string InteractionChannelId { get; set; } = string.Empty;

        [JsonPropertyName("guild_id")]
        public string? GuildId { get; set; }
    }

    public class BracketData
    {
        [JsonPropertyName("teams")]
        public List<List<Participant?>> Teams { get; set; } = new();

        [JsonPropertyName("results")]
        public JsonElement Results { get; set; }
    }
} 