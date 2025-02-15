namespace TeknoMES.Api.Services.Plc;

public interface IPlcService
{
    Task<T?> ReadTagAsync<T>(string gateway, string path, string tagName);
    Task WriteTagAsync<T>(string gateway, string path, string tagName, object value);
}
