using PlcDemoBackend.Endpoints.Tags.Read;
using PlcDemoBackend.Services.Plc;
using PlcDemoBackend.Services.Plc.ControlLogix;
using TeknoPlcDemo.Api.Endpoints.Tags.Read;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPlcService>(provider => new ControlLogixService());

var app = builder.Build();

app.MapGet("/", () => "Tekno MES API.");

app.MapPost("/tags/read/string", ReadTagString.HandleAsync);
app.MapPost("/tags/read/bool", ReadTagBool.HandleAsync);

app.Run();
