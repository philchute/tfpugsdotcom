using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tfpugsdotcom.Models.ApiResponses
{
    public class ApiGameServer
    {
        [JsonPropertyName("internal_name")]
        public string InternalName { get; set; } = string.Empty;

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("ip_address")]
        public string IpAddress { get; set; } = string.Empty;

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("connect_password")]
        public string? ConnectPassword { get; set; }

        [JsonPropertyName("steam_connect_url")]
        public string SteamConnectUrl { get; set; } = string.Empty;

        [JsonPropertyName("game_ids")]
        public List<string> GameIds { get; set; } = new List<string>();

        [JsonPropertyName("region")]
        public string Region { get; set; } = string.Empty;

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; } = new List<string>();
    }

    public class ApiLiveMatchTeamPlayer
    {
        [JsonPropertyName("discord_id")]
        public string DiscordId { get; set; } = string.Empty;

        [JsonPropertyName("name_from_context")]
        public string NameFromContext { get; set; } = string.Empty;
    }

    public class ApiLiveMatchTeam
    {
        [JsonPropertyName("team_label")]
        public string TeamLabel { get; set; } = string.Empty;

        [JsonPropertyName("avg_elo")]
        public int AvgElo { get; set; }

        [JsonPropertyName("players")]
        public List<ApiLiveMatchTeamPlayer> Players { get; set; } = new List<ApiLiveMatchTeamPlayer>();
    }

    public class ApiLiveMatchUnofficialScores
    {
        [JsonPropertyName("team1")]
        public int Team1 { get; set; }

        [JsonPropertyName("team2")]
        public int Team2 { get; set; }
    }

    public class ApiLiveMatch
    {
        [JsonPropertyName("match_id")]
        public string MatchId { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("base_game_id")]
        public string BaseGameId { get; set; } = string.Empty;

        [JsonPropertyName("game_name")]
        public string GameName { get; set; } = string.Empty;

        [JsonPropertyName("map_name")]
        public string MapName { get; set; } = string.Empty;

        [JsonPropertyName("server_name")]
        public string ServerName { get; set; } = string.Empty;

        [JsonPropertyName("server_ip")]
        public string ServerIp { get; set; } = string.Empty;

        [JsonPropertyName("server_port")]
        public int ServerPort { get; set; }

        [JsonPropertyName("server_password")]
        public string? ServerPassword { get; set; }

        [JsonPropertyName("teams")]
        public List<ApiLiveMatchTeam> Teams { get; set; } = new List<ApiLiveMatchTeam>();

        [JsonPropertyName("is_moneyball")]
        public bool IsMoneyball { get; set; }

        [JsonPropertyName("is_tournament_match")]
        public bool IsTournamentMatch { get; set; }

        [JsonPropertyName("tournament_id")]
        public string? TournamentId { get; set; }

        [JsonPropertyName("tournament_round")]
        public string? TournamentRound { get; set; }

        [JsonPropertyName("start_time")]
        public DateTime? StartTime { get; set; }

        [JsonPropertyName("creation_time")]
        public DateTime CreationTime { get; set; }

        [JsonPropertyName("unofficial_scores")]
        public ApiLiveMatchUnofficialScores? UnofficialScores { get; set; }

        [JsonPropertyName("interaction_channel_id")]
        public string InteractionChannelId { get; set; } = string.Empty;

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; } = string.Empty;
    }
} 