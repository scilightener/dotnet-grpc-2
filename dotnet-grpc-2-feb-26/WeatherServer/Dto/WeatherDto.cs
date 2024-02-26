using Google.Protobuf.WellKnownTypes;

namespace WeatherServer.Dto;

public record WeatherDto(Timestamp Date, double? Temperature);