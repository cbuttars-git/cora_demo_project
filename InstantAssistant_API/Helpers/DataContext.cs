namespace InstantAssistant.Api.Helpers;

using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using InstantAssistant.Api.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("InstantAssistant"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientsInfo>()
        .Property(e => e.IAConfigData)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            v => JsonSerializer.Deserialize<IAConfigData>(v, (JsonSerializerOptions)null));
    }

    public DbSet<ClientsInfo> ClientsInfo { get; set; }
    public DbSet<Feedback> Feedback { get; set; }
    public DbSet<ChatBotLogs> ChatBotLog { get; set; }
    public DbSet<ErrorLogs> ErrorLog { get; set; }
}