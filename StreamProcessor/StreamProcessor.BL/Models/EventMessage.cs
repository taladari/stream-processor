using System.Text.Json.Serialization;

namespace StreamProcessor.BL.Models
{
    public class EventMessage
    {
        [JsonPropertyName("event_type")]
        public string EventType { get; set; }
        [JsonPropertyName("data")]
        public string Data { get; set; }
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
