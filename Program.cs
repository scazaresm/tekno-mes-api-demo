using TeknoMES.Api.Endpoints.Tags.Read;
using TeknoMES.Api.Endpoints.Tags.Write;
using TeknoMES.Api.Services.Plc;
using TeknoMES.Api.Services.Plc.ControlLogix;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPlcService>(provider => new ControlLogixService());

var app = builder.Build();

app.MapGet("/", () => "Tekno MES API.");

app.MapPost("/tags/read/string", ReadTagString.HandleAsync);
app.MapPost("/tags/read/bool", ReadTagBool.HandleAsync);
app.MapPost("/tags/write/string", WriteTagString.HandleAsync);

app.Run();
