using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static FSA.Management.Application.Domain.Constants.Permissions;

namespace RazorClassLibrary1.Dashboard_Models
{
    public class Issue
    {
        [JsonPropertyName("html_url")]
        public string Url { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("closed_at")]
        public DateTime? ClosedAt { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("labels")]
        public IEnumerable<Label> Labels { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }

        [JsonPropertyName("assignees")]
        public IEnumerable<User> Assignees { get; set; }

        [JsonPropertyName("state")]
        public IssueState State { get; set; }
    }
}
