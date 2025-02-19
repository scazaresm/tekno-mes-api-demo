using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using TeknoMES.Api.Endpoints.Tags.Read;
using TeknoMES.Api.Endpoints.Tags.Write;
using TeknoMES.Api.Infrastructure.Database;
using TeknoMES.Api.Services.Plc;
using TeknoMES.Api.Services.Plc.ControlLogix;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Host=localhost;Port=5432;Database=tekno;Username=myuser;Password=mypassword";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 8083);
});

builder.Services.AddCors();

builder.Services.AddSignalR();

builder.Services.AddSingleton<IPlcTagFactory>(provider => new PlcTagFactory()); 

builder.Services.AddSingleton<ITagMonitoringService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<TagMonitoringService>>();
    var tagFactory = provider.GetRequiredService<IPlcTagFactory>();
    var hubContext = provider.GetRequiredService<IHubContext<PlcHub>>();

    return new TagMonitoringService(logger, tagFactory, hubContext);
});

builder.Services.AddScoped<IPlcService>(provider =>
{
    var tagFactory = provider.GetRequiredService<IPlcTagFactory>();

    return new ControlLogixService(tagFactory);
});



var app = builder.Build();

app.UseCors(builder => builder
               .AllowAnyHeader()
               .AllowAnyMethod()
               .SetIsOriginAllowed((host) => true)
               .AllowCredentials()
           );

app.MapHub<PlcHub>("/plchub");


app.MapGet("/", () => "Tekno MES API.");


app.MapPost("/tags/string/read", ReadTagString.HandleAsync);
app.MapPost("/tags/bool/read", ReadTagBool.HandleAsync);
app.MapPost("/tags/string/write", WriteTagString.HandleAsync);


app.MapPost("/tags/dint/monitor-start", async (StartMonitoringRequest request, ITagMonitoringService tagMonitoringService) =>
{
    await tagMonitoringService
        .StartMonitoringAsync<int>(request.Gateway, request.Path, request.CommandTagName, request.IntervalMilliseconds);
});

app.MapPost("/tags/dint/monitor-stop", (StopMonitoringRequest request, ITagMonitoringService tagMonitoringService) =>
{
    tagMonitoringService.StopMonitoring(request.Gateway, request.Path, request.CommandTagName);
});


app.MapPost("/stations", async (AppDbContext db, Station station) =>
{
    db.Stations.Add(station);
    await db.SaveChangesAsync();
    return Results.Created($"/stations/{station.Id}", station);
});


app.MapGet("/stations", async (AppDbContext db) => await db.Stations.ToListAsync());



app.Run();

record StartMonitoringRequest(string Gateway, string Path, string CommandTagName, int IntervalMilliseconds);
record StopMonitoringRequest(string Gateway, string Path, string CommandTagName);
