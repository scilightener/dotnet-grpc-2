using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using SecretInfo;

namespace WeatherServer.Services;

[Authorize]
public class SecretService : SecretInfo.SecretService.SecretServiceBase
{
    public override Task<Response> GetSecret(Request request, ServerCallContext context)
    {
        return Task.FromResult(new Response { Secret = "SECRET SECRET" });
    }
}