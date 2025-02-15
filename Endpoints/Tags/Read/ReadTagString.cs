using PlcDemoBackend.Requests;
using PlcDemoBackend.Services.Plc;

namespace PlcDemoBackend.Endpoints.Tags.Read;

public static class ReadTagString
{
    public static async Task<IResult> HandleAsync(TagReadRequest request, IPlcService plcService)
    {
        try
        {
            var value = await plcService.ReadTagAsync<string>(request.Gateway, request.Path, request.TagName)
                ?? throw new Exception("Could not read tag value.");

            var resultJson = new
            {
                Gateway = request.Gateway,
                Path = request.Path,
                TagName = request.TagName,
                Value = value
            };

            return Results.Json(resultJson);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Could not read string tag: {ex.Message}");
        }
    }
}
