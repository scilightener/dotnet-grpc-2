using Weather;

namespace WeatherClient;

public static class DtoMapper
{
    public static string MapToString(Response dto, DateTime requestDate)
    {
        var dateRequest = requestDate.ToString("HH.mm.ss");
        var dateResponse = dto.Date.ToDateTime().ToString("dd.MM.yyyy HH:mm");
        return $"{dateRequest} погода на {dateResponse} = {dto.Temperature}C";
    }
}