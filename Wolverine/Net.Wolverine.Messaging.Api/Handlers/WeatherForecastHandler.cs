namespace Net.Wolverine.Messaging.Handlers;

public class WeatherForecastHandler
{
    private readonly ILogger<WeatherForecastHandler> _logger;

    public WeatherForecastHandler(ILogger<WeatherForecastHandler> logger)
    {
        _logger = logger;
    }

    public void Handle(
        WeatherForecastRequest weatherForecast)
    {
        this._logger.LogInformation("{WeatherForecast} handled", weatherForecast);
    }

}
