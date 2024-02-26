using Grpc.Core;
using Weather;
using WeatherServer.External;

namespace WeatherServer.Services;

public class WeatherService : Weather.WeatherService.WeatherServiceBase
{
    private readonly IWeatherService _weatherService;

    public WeatherService(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public override async Task WeatherStream(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
    {
        var data = await _weatherService.GetAsync();
        var i = 0;
        while (!context.CancellationToken.IsCancellationRequested)
        {
            if (i >= data.Count)
                break;
            await responseStream.WriteAsync(
                new Response
                {
                    Date = data[i].Date,
                    Temperature = data[i].Temperature ?? 0
                });
            i += 2;
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        Console.WriteLine("the cancellation is requested from the client or the max number of fetched data is reached");
    }
}