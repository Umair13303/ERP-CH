using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrganisationSetup.Models.DAL;

public partial class ERPOrganisationSetupContext : DbContext
{
    public ERPOrganisationSetupContext(DbContextOptions<ERPOrganisationSetupContext> options)
        : base(options)
    {
    }

    public virtual DbSet<vRight> vRights { get; set; }

    public virtual DbSet<ACBranch> ACBranches { get; set; }

    public virtual DbSet<ACCompany> ACCompanies { get; set; }

    public virtual DbSet<ACUser> ACUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<vRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRight");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ACBranch>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACBranch");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACCompany>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACCompany");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ACUser");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
