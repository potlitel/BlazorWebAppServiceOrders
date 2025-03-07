using System.Text.Json.Serialization;

namespace RazorClassLibrary1.Dashboard_Models
{
    public class Label
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("color")]
        public string? Color { get; set; }
    }
}
