using Microsoft.EntityFrameworkCore;

namespace TeknoMES.Api.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<Station> Stations { get; set; }
}
