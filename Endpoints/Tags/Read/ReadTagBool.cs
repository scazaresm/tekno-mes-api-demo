using TeknoMES.Api.Requests;
using TeknoMES.Api.Services.Plc;

namespace TeknoMES.Api.Endpoints.Tags.Read;

public static class ReadTagBool
{
    public static async Task<IResult> HandleAsync(TagReadRequest request, IPlcService plcService)
    {
        try
        {
            var value = await plcService.ReadTagAsync<bool>(request.Gateway, request.Path, request.TagName);

            var resultJson = new
            {
                request.Gateway,
                request.Path,
                request.TagName,
                value
            };

            return Results.Json(resultJson);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Could not read bool tag: {ex.Message}");
        }
    }
}
