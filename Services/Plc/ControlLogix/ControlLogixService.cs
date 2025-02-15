namespace PlcDemoBackend.Services.Plc.ControlLogix;

public class ControlLogixService : IPlcService
{
    public async Task<T?> ReadTagAsync<T>(string gateway, string path, string tagName)
    {
        var tag = ControlLogixTagFactory.CreateTag(typeof(T), gateway, path, tagName);

        var value = await tag.ReadAsync();

        return (T?)value;
    }
}
