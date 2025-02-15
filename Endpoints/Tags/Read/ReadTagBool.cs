using PlcDemoBackend.Requests;
using PlcDemoBackend.Services.Plc;
using PlcDemoBackend.Services.Plc.ControlLogix;

namespace TeknoPlcDemo.Api.Endpoints.Tags.Read;

public static class ReadTagBool
{
    public static async Task<IResult> HandleAsync(TagReadRequest request, IPlcService plcService)
    {
        try
        {
            var value = await plcService.ReadTagAsync<bool>(request.Gateway, request.Path, request.TagName);

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
            return Results.Problem(ex.Message);
        }
    }
}
