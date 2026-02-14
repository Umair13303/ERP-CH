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

    public virtual DbSet<CCategory> CCategories { get; set; }

    public virtual DbSet<CDepartment> CDepartments { get; set; }

    public virtual DbSet<CMaterial> CMaterials { get; set; }

    public virtual DbSet<CSaleUnit> CSaleUnits { get; set; }

    public virtual DbSet<CSection> CSections { get; set; }

    public virtual DbSet<CSubCategory> CSubCategories { get; set; }

    public virtual DbSet<CType> CTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CProduct__3214EC07E1361943");

            entity.ToTable("CCategory");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CDepartment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CDepartment");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CMateria__3214EC07685D309B");

            entity.ToTable("CMaterial");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CSaleUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CSaleUni__3214EC0760DA5D56");

            entity.ToTable("CSaleUnit");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Packing).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PackingQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CSection>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CSection");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CSubCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CSubCategory");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CType");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
