using AskonTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AskonTest.Database;

public class AskonTestContext : DbContext
 {
 	public AskonTestContext(DbContextOptions<AskonTestContext> options) : base(options)
 	{
 	}
    
    public virtual DbSet<Applications> Applications { get; set; }
    public virtual DbSet<Settings> Settings { get; set; }
    public virtual DbSet<Users> Users { get; set; }
    public virtual DbSet<SettingsStatuses> SettingsStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    modelBuilder.Entity<SettingsStatuses>(entity => entity.HasNoKey());
	    base.OnModelCreating(modelBuilder);
    }
 }