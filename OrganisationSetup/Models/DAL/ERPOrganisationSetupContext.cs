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

    public virtual DbSet<ACBranch> ACBranches { get; set; }

    public virtual DbSet<ACCompany> ACCompanies { get; set; }

    public virtual DbSet<ACUser> ACUsers { get; set; }

    public virtual DbSet<AFChartOfAccount> AFChartOfAccounts { get; set; }

    public virtual DbSet<vCity> vCities { get; set; }

    public virtual DbSet<vCountry> vCountries { get; set; }

    public virtual DbSet<vRight> vRights { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        modelBuilder.Entity<AFChartOfAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AFChartOfAccount");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<vCity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCity");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCountry");
        });

        modelBuilder.Entity<vRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRight");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
