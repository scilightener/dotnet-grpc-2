using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WeatherServer;
using WeatherServer.External;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IWeatherService, WeatherService>();

builder.Services.AddGrpc();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.RequireHttpsMetadata = false;
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = JwtConstants.Issuer,
            ValidAudience = JwtConstants.Audience,
            IssuerSigningKey =
                new SymmetricSecurityKey(JwtConstants.Key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<WeatherServer.Services.WeatherService>();
app.MapGrpcService<WeatherServer.Services.JwtService>();
app.MapGrpcService<WeatherServer.Services.SecretService>();

app.Run();