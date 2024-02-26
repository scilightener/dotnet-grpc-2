using Grpc.Core;
using Grpc.Net.Client;
using Weather;
using WeatherClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5163");

var client = new WeatherService.WeatherServiceClient(channel);

var serverData = client.WeatherStream(new Request());

var responseStream = serverData.ResponseStream;

var cancellationTokenSource = new CancellationTokenSource();

Console.WriteLine("press enter to cancel");
Task.Run(() =>
{
    Console.ReadLine();
    cancellationTokenSource.Cancel();
});

try
{
    await foreach(var response in responseStream.ReadAllAsync(cancellationTokenSource.Token))
    {
        Console.WriteLine(DtoMapper.MapToString(response, DateTime.UtcNow));
    }
}
catch (Exception)
{
    Console.WriteLine("cancellation requested");
}