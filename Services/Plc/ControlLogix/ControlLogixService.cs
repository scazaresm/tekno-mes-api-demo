namespace TeknoMES.Api.Services.Plc.ControlLogix;

public class ControlLogixService(IPlcTagFactory tagFactory) : IPlcService
{
    public async Task<T?> ReadTagAsync<T>(string gateway, string path, string tagName)
    {
        using var tag = tagFactory.Create(typeof(T), gateway, path, tagName);

        var value = await tag.ReadAsync();

        return (T?)value;
    }

    public async Task WriteTagAsync<T>(string gateway, string path, string tagName, object value)
    {
        using var tag = tagFactory.Create(typeof(T), gateway, path, tagName);

        tag.Value = value;
        await tag.WriteAsync();
    }
}
