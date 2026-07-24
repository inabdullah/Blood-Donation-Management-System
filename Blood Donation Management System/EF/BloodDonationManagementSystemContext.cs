using System;
using System.Collections.Generic;
using Blood_Donation_Management_System.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace Blood_Donation_Management_System.EF;

public partial class BloodDonationManagementSystemContext : DbContext
{
    public BloodDonationManagementSystemContext()
    {
    }

    public BloodDonationManagementSystemContext(DbContextOptions<BloodDonationManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donation> Donations { get; set; }

    public virtual DbSet<Donor> Donors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConn");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PK_Donation_1");

            entity.ToTable("Donation");

            entity.Property(e => e.CampName).HasMaxLength(50);

            entity.HasOne(d => d.Donor).WithMany(p => p.Donations)
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Donation_Donor");
        });

        modelBuilder.Entity<Donor>(entity =>
        {
            entity.ToTable("Donor");

            entity.Property(e => e.BloodGroup).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.ContactNo).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
