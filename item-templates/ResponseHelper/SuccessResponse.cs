using System.Text.Json.Serialization;

namespace Template.WebApi.Helpers;

public class SuccessResponse<T>
{
    [JsonPropertyName("success")]
    public bool Success { get; set; } = true;

    [JsonPropertyName("data")]
    public required T Data { get; set; }
}