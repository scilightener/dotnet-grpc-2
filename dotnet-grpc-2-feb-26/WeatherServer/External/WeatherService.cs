using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using WeatherServer.Dto;

namespace WeatherServer.External;

public interface IWeatherService
{
    public Task<List<WeatherDto>> GetAsync();
}

public class WeatherService : IWeatherService
{
    private static List<WeatherDto>? _data;

    private const string Url = "https://archive-api.open-meteo.com/v1/archive?" +
                               "latitude=52.52&longitude=13.41&start_date=2024-01-01&end_date=2024-02-26" +
                               "&hourly=temperature_2m&timezone=Europe%2FMoscow";

    public async Task<List<WeatherDto>> GetAsync()
    {
        if (_data is not null)
            return _data;
        
        var client = new HttpClient();
        var response = await client.GetAsync(Url);
        var responseData = JsonSerializer.Deserialize<ApiResponseDto>(await response.Content.ReadAsStringAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        if (responseData is null)
            throw new BadHttpRequestException("wattafuck");
        _data = MapToWeatherDto(responseData);
        return _data;
    }

    private List<WeatherDto> MapToWeatherDto(ApiResponseDto apiResponseDto)
    {
        var response = apiResponseDto.Results;
        var list = new List<WeatherDto>(response.Time.Length);
        for (var i = 0; i < response.Time.Length; i++)
            list.Add(new WeatherDto(response.Time[i].ToUniversalTime().ToTimestamp(), response.Temperature[i]));
        return list;
    }
}