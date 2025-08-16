namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

//public class DataContext : DbContext
//{
//    protected readonly IConfiguration Configuration;

//    public DataContext(IConfiguration configuration)
//    {
//        Configuration = configuration;
//    }

//    protected override void OnConfiguring(DbContextOptionsBuilder options)
//    {
//        // in memory database used for simplicity.
//        options.UseInMemoryDatabase("UserDb");
//    }

//    public DbSet<User> Users { get; set; }
//}

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } // example entity
    public DbSet<Stations> Stations { get; set; } // example entity
    public DbSet<ChartInfo> ChartInfo { get; set; } // example entity
    public DbSet<FareChart> FareChart { get; set; } // example entity

}