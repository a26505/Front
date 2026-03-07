using System.Collections.Generic;

namespace REPS_backend.DTOs.Rutinas
{
    public class RutinaIARequestDto
    {
        [System.Text.Json.Serialization.JsonPropertyName("goal")]
        public string Goal { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("level")]
        public string Level { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("days")]
        public int Days { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("duration")]
        public string Duration { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonPropertyName("muscles")]
        public List<string> Muscles { get; set; } = new();

        [System.Text.Json.Serialization.JsonPropertyName("equipment")]
        public List<string> Equipment { get; set; } = new();

        [System.Text.Json.Serialization.JsonPropertyName("notes")]
        public string Notes { get; set; } = string.Empty;
    }
}
