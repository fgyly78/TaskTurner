﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskTurner.Models;

namespace TaskTurner;

public partial class BaseDBContext : DbContext
{
    public BaseDBContext()
    {
    }

    public BaseDBContext(DbContextOptions<BaseDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MyTask> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=task_manager;Username=postgres;Password=Allstars1051");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MyTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tasks_pkey");

            entity.Property(e => e.Title).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
