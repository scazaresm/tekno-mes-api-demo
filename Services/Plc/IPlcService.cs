namespace PlcDemoBackend.Services.Plc;

public interface IPlcService
{
    Task<T?> ReadTagAsync<T>(string gateway, string path, string tagName);
}
