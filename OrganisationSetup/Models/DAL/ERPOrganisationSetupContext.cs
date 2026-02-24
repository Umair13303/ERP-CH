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

    public virtual DbSet<ACBranch> ACBranch { get; set; }

    public virtual DbSet<ACCompany> ACCompany { get; set; }

    public virtual DbSet<ACDepartment> ACDepartment { get; set; }

    public virtual DbSet<ACUser> ACUser { get; set; }

    public virtual DbSet<AFChartOfAccount> AFChartOfAccount { get; set; }

    public virtual DbSet<vAccountCatagory> vAccountCatagory { get; set; }

    public virtual DbSet<vAccountType> vAccountType { get; set; }

    public virtual DbSet<vCity> vCity { get; set; }

    public virtual DbSet<vCountry> vCountry { get; set; }

    public virtual DbSet<vFinancialStatement> vFinancialStatement { get; set; }

    public virtual DbSet<vOrganisationType> vOrganisationType { get; set; }

    public virtual DbSet<vOrganizationType> vOrganizationType { get; set; }

    public virtual DbSet<vRight> vRight { get; set; }

    public virtual DbSet<vRole> vRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ACBranch>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACCompany>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACDepartment>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ACUser>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AFChartOfAccount>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<vAccountCatagory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAccountCatagory");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vAccountType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAccountType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
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

        modelBuilder.Entity<vFinancialStatement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vFinancialStatement");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vOrganisationType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrganisationType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vOrganizationType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrganizationType");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRight");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<vRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRole");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
