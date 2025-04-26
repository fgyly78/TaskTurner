using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TaskTurner;
using TaskTurner.Models;


public class ApplicationContext : DbContext
{
    public DbSet<Task> Tasks { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=task_manager;Username=postgres;Password=Allstars1051");
    }
}