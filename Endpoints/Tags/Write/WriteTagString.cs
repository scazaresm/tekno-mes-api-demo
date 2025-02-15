using System.Text.Json;
using TeknoMES.Api.Requests;
using TeknoMES.Api.Services.Plc;

namespace TeknoMES.Api.Endpoints.Tags.Write;

public class WriteTagString
{
    public static async Task<IResult> HandleAsync(TagWriteRequest request, IPlcService plcService)
    {
        try
        {
            var jsonElement = (JsonElement)request.Value;

            var value = jsonElement.GetString() 
                ?? throw new Exception("Failed to get a string from the JsonElement.");

            await plcService.WriteTagAsync<string>(request.Gateway, request.Path, request.TagName, value);

            var resultJson = new
            {
                request.Gateway,
                request.Path,
                request.TagName,
                request.Value
            };

            return Results.Json(resultJson);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Could not write string tag: {ex.Message}");
        }
    }
}
