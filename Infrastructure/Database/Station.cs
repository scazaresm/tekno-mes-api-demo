namespace TeknoMES.Api.Infrastructure.Database;

public class Station
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IpAddress { get; set; }
    public bool HasCamera { get; set; }
    public bool HasScanner { get; set; }
}
