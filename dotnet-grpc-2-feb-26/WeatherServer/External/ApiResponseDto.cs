using System.Text.Json.Serialization;

namespace WeatherServer.External;

public class ApiResponseDto
{
    [JsonPropertyName("hourly")]
    public required HourlyResults Results { get; set; }
}

public class HourlyResults
{
    [JsonPropertyName("time")]
    public required DateTime[] Time { get; set; }
    
    [JsonPropertyName("temperature_2m")]
    public required double?[] Temperature { get; set; }
}