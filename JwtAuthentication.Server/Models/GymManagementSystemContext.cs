using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JwtAuthentication.Server.Models;

namespace JwtAuthentication.Server.Models;

public partial class GymManagementSystemContext : DbContext
{
    public GymManagementSystemContext()
    {
    }

    public GymManagementSystemContext(DbContextOptions<GymManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-TLNS7U0; Database=GymManagementSystem; trusted_connection=true; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69263CADA8C872");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId)
                .ValueGeneratedNever()
                .HasColumnName("AttendanceID");
            entity.Property(e => e.AttendanceDate).HasColumnType("date");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Class).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Attendanc__Class__4222D4EF");

            entity.HasOne(d => d.Member).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Attendanc__Membe__412EB0B6");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A0226A8EC9");

            entity.Property(e => e.ClassId)
                .ValueGeneratedNever()
                .HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Schedule).HasMaxLength(200);
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.Classes)
                .HasForeignKey(d => d.TrainerId)
                .HasConstraintName("FK__Classes__Trainer__3E52440B");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B383C0DE27B");

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("MemberID");
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.JoinDate).HasColumnType("date");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58682EE2B4");

            entity.Property(e => e.PaymentId)
                .ValueGeneratedNever()
                .HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Member).WithMany(p => p.Payments)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Payments__Member__44FF419A");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B24BD847EB6C6");

            entity.Property(e => e.SubscriptionId)
                .ValueGeneratedNever()
                .HasColumnName("SubscriptionID");
            entity.Property(e => e.AmountPaid).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.SubscriptionType).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK__Subscript__Membe__398D8EEE");
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.TrainerId).HasName("PK__Trainers__366A1B9CE04BC813");

            entity.Property(e => e.TrainerId)
                .ValueGeneratedNever()
                .HasColumnName("TrainerID");
            entity.Property(e => e.ContactNumber).HasMaxLength(20);
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.HireDate).HasColumnType("date");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<JwtAuthentication.Server.Models.User>? User { get; set; }

    public DbSet<JwtAuthentication.Server.Models.LoginModel>? LoginModel { get; set; }
}
