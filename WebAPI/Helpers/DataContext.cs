namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } // example entity
    public DbSet<Stations> Stations { get; set; } // example entity
    public DbSet<ChartInfo> ChartInfo { get; set; } // example entity
    public DbSet<FareChart> FareChart { get; set; } // example entity

}