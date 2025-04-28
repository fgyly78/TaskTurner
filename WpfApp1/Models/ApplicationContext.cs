using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTurner;
using TaskTurner.Models;


public class ApplicationContext : BaseDBContext
{
    public DbSet<MyTask> Tasks { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=task_manager;Username=postgres;Password=Allstars1051");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyTask>()
            .Property(t => t.TaskState)
            .HasConversion<string>();

        modelBuilder.Entity<MyTask>()
            .Property(t => t.TaskImportance)
            .HasConversion<string>();
    }

}