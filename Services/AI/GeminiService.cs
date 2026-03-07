using REPS_backend.Data;
using REPS_backend.DTOs.Rutinas;
using REPS_backend.Models;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace REPS_backend.Services.AI
{
    public class GeminiService : IAIService
    {
        private readonly string _apiKey;
        private readonly string _baseUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public GeminiService(IConfiguration configuration, ApplicationDbContext context)
        {
            _apiKey = configuration["Gemini:ApiKey"] ?? string.Empty;
            _httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
            _context = context;
        }

        public async Task<string> AnalyzeWorkoutAsync(Sesion sesion)
        {
            var prompt = $"Actúa como un entrenador experto. Analiza la siguiente sesión y dame un resumen y consejos en ESPAÑOL (máximo 3 frases). " +
                         $"Highlight any achievements or suggest improvements based on volume/intensity if possible.\n\n" +
                         $"Workout: {sesion.NombreRutinaSnapshot}\n" +
                         $"Duration: {sesion.DuracionRealMinutos} mins\n" +
                         $"Exercises:\n";

            if (sesion.SeriesRealizadas != null)
            {
                foreach (var serie in sesion.SeriesRealizadas)
                {
                    prompt += $"- Exercise ID {serie.EjercicioId}: {serie.RepsRealizadas} reps @ {serie.PesoUsado}kg (Success: {serie.Completada})\n";
                }
            }

            return await CallGeminiApiAsync(prompt);
        }

        public async Task<string> GetWorkoutTipsAsync(string workoutName, List<string> muscles)
        {
            var musclesList = muscles != null && muscles.Any() ? string.Join(", ", muscles) : "muscles generales";
            var prompt = $"Eres un entrenador personal experto. El usuario acaba de terminar un entrenamiento de '{workoutName}' trabajando: {musclesList}. " +
                         $"Dame exactamente 2 consejos breves y motivadores en ESPAÑOL. " +
                         $"Formato: devuelve solo los 2 consejos separados por punto y aparte, sin numeraci\u00f3n, sin emojis, sin intro. " +
                         $"M\u00e1ximo 25 palabras por consejo.";

            try
            {
                return await CallGeminiApiAsync(prompt);
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task<RutinaDetalleDto> GenerateRoutineAsync(RutinaIARequestDto dto)
        {
            var ejerciciosDb = await _context.Ejercicios.Select(e => new { e.Id, e.Nombre }).ToListAsync();
            var ejerciciosLista = string.Join(", ", ejerciciosDb.Select(e => $"{e.Id}:{e.Nombre}"));

            var officialExercises = "Press de Banca, Sentadilla, Peso Muerto, Dominadas, Remo Barra, Press Militar, Curl Bíceps, Fondos Tríceps, Zancadas, Plancha, Press Inclinado, Aperturas con Mancuernas, Cruce de Poleas, Prensa de Piernas, Curl Femoral, Extensiones de Cuádriceps, Elevaciones de Gemelos, Jalón al Pecho, Remo Gironda, Pull-over, Elevaciones Laterales, Pájaros (Deltoides Posterior), Encogimientos de Hombros, Curl Martillo, Curl Predicador, Extensión de Tríceps Polea, Press Francés, Crunch Abdominal, Elevación de Piernas Colgado, Russian Twists";

            var prompt = $@"
Genera una rutina de entrenamiento personalizada en formato JSON basándote en:
- Objetivo: {dto.Goal}
- Nivel: {dto.Level}
- Días: {dto.Days}
- Duración: {dto.Duration}
- Músculos: {string.Join(", ", dto.Muscles)}
- Equipo: {string.Join(", ", dto.Equipment)}
- Notas: {dto.Notes}

Usa EXCLUSIVAMENTE estos nombres de ejercicio si es posible: {officialExercises}.
Si necesitas uno fuera de la lista, usa un nombre descriptivo estándar.

Responde ÚNICAMENTE con el objeto JSON siguiendo esta estructura (usa minúsculas para las claves):
{{
  ""nombre"": ""Nombre creativo de la rutina"",
  ""nivel"": ""{dto.Level}"",
  ""duracionMinutos"": 60,
  ""ejercicios"": [
    {{
      ""ejercicioId"": 0, 
      ""nombreEjercicio"": ""Nombre exacto del ejercicio"",
      ""series"": 3,
      ""repeticiones"": ""12""
    }}
  ]
}}
";

            var jsonResponse = await CallGeminiApiAsync(prompt);
            
            // Limpieza robusta: buscar el primer '{' y el último '}' por si hay markdown
            int firstBrace = jsonResponse.IndexOf('{');
            int lastBrace = jsonResponse.LastIndexOf('}');
            if (firstBrace != -1 && lastBrace != -1 && lastBrace > firstBrace) {
                jsonResponse = jsonResponse.Substring(firstBrace, lastBrace - firstBrace + 1);
            }

            if (string.IsNullOrEmpty(jsonResponse)) throw new Exception("AI returned empty response.");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

            try
            {
                Console.WriteLine($"[Gemini JSON Payload]:\n{jsonResponse}");
                var rutina = JsonSerializer.Deserialize<RutinaDetalleDto>(jsonResponse, options);
                return rutina ?? new RutinaDetalleDto();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"[Gemini Parsing ERROR]: {ex.Message}\n[Payload]: {jsonResponse}");
                throw new Exception($"Failed to parse AI response. Error: {ex.Message}");
            }
        }

        private async Task<string> CallGeminiApiAsync(string textPrompt)
        {
            if (string.IsNullOrEmpty(_apiKey)) return "Error: Gemini API key is missing.";

            var requestBody = new
            {
                contents = new[]
                {
                    new { parts = new[] { new { text = textPrompt } } }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            int maxRetries = 5;
            int delay = 5000;

            for (int i = 0; i <= maxRetries; i++)
            {
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}?key={_apiKey}", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(responseString);

                    if (doc.RootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                    {
                        var firstCandidate = candidates[0];
                        if (firstCandidate.TryGetProperty("content", out var contentElem) &&
                            contentElem.TryGetProperty("parts", out var parts) &&
                            parts.GetArrayLength() > 0)
                        {
                            return parts[0].GetProperty("text").GetString() ?? "";
                        }
                    }
                    return "";
                }

                if ((int)response.StatusCode == 429 || (int)response.StatusCode == 503)
                {
                    if (i == maxRetries)
                    {
                        var errorMsg = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Gemini API Error (Rate Limit/Unavailable after retries): {errorMsg}");
                    }

                    await Task.Delay(delay);
                    delay *= 2;
                    continue;
                }

                var otherErrorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gemini API Error ({response.StatusCode}): {otherErrorMsg}");
            }

            return "";
        }
    }
}
