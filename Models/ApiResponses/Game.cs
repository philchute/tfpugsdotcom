using System.Text.Json.Serialization;


namespace tfpugsdotcom.Models.ApiResponses
{
    public class Game
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("elo_shared_with_game_id")]
        public string? EloSharedWithGameId { get; set; }

        [JsonPropertyName("is_tournament_game")]
        public bool IsTournamentGame { get; set; }

        [JsonPropertyName("participant_type")]
        public string ParticipantType { get; set; } = "player"; // Default to player
    }
} 