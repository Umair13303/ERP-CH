using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IMSClothHouse.Models.DAL;

public partial class ERPClothHouseContext : DbContext
{
    public ERPClothHouseContext(DbContextOptions<ERPClothHouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cbrand> Cbrands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cbrand>(entity =>
        {
            entity.ToTable("CBrand");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.GuId).HasColumnName("GuID");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
