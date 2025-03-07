using System.Text.Json.Serialization;

namespace RazorClassLibrary1.Dashboard_Models
{
    public class User
    {
        [JsonPropertyName("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;

        [JsonPropertyName("login")]
        public string Login { get; set; } = string.Empty;
    }
}
