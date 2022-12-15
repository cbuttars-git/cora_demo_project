// 
//    Project:  rest.instantassistant
//    Created:  07-2022-11
// 
//    COPYRIGHT © 2022 - 2030 Navient Solutions, LLC. All rights reserved
// 
//    COPYRIGHT NOTICE
//    The entire contents of this file and the files that make up the assembly that this file resides in are the express property of Navient Solutions, LLC. All Rights Reserved.
//    None of the contents of this file or assembly may be copied or duplicated in whole or part by any means without express prior agreement in writing or unless
//    specifically noted within this file or copyright notice of this file, assembly, or API.
// 
//    Some of the content contained within this file, assembly or API may be the copyrighted property of others; acknowledgement of those copyrights is hereby given.
//    All such material is used with the proper license or permission of the owner.
// 

namespace Navient.Presentation.Service.Rest.Helpers;

/// <summary>
/// </summary>
public class DataContext : DbContext
{
  private readonly IConfiguration _configuration;

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  public DataContext(IConfiguration configuration)
  {
    _configuration = configuration; // It is bad mojo to pass IConfiguration around
  }

  /// <summary>
  /// </summary>
  public DbSet<ChatBotLog> ChatBotLog { get; set; }

  /// <summary>
  /// </summary>
  public DbSet<ClientsInfo> ClientsInfo { get; set; }

  /// <summary>
  /// </summary>

  public DbSet<ErrorLogs> ErrorLog { get; set; }

  /// <summary>
  /// </summary>
  public DbSet<Feedback> Feedback { get; set; }

  /// <summary>
  /// </summary>
  /// <param name="options"></param>
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    options.UseSqlServer(_configuration.GetConnectionString("InstantAssistant"));
  }

  /// <summary>
  /// </summary>
  /// <param name="modelBuilder"></param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<ClientsInfo>()
        .Property(e => e.IAConfigData)
        .HasConversion(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<IAConfigData>(v, (JsonSerializerOptions)null!));
  }
}