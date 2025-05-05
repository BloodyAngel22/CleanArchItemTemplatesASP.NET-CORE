using System.Text.Json.Serialization;

namespace Template.WebApi.Helpers;

public class ErrorResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; } = false;

    [JsonPropertyName("message")]
    public string? Message { get; set; }
}