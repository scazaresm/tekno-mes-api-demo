namespace TeknoMES.Api.Services.Plc.ControlLogix;

public interface ITagMonitoringService
{
    Task StartMonitoringAsync<T>(string gateway, string path, string commandTagName, int intervalMilliseconds);
    void StopMonitoring(string gateway, string path, string commandTagName);
}
