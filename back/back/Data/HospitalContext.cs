using System;
using System.Collections.Generic;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Data;

public partial class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<InternistDatum> InternistData { get; set; }

    public virtual DbSet<MedicalReferral> MedicalReferrals { get; set; }

    public virtual DbSet<MedicalReport> MedicalReports { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("appointment");

            entity.HasIndex(e => e.DoctorId, "doctor_app_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PatientId, "patient_app_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.InternistDataId).HasColumnName("internist_data_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Time)
                .HasMaxLength(45)
                .HasColumnName("time");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AppointmentDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_app");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Appointment)
                .HasForeignKey<Appointment>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("internist_app");

            entity.HasOne(d => d.Patient).WithMany(p => p.AppointmentPatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_app");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("blog");

            entity.HasIndex(e => e.DoctorId, "doctor_wrote_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(45)
                .HasColumnName("content");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .HasColumnName("title");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_wrote");
        });

        modelBuilder.Entity<InternistDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("internist_data");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PatientId, "patient_appointment_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BloodPressure)
                .HasMaxLength(45)
                .HasColumnName("blood_pressure");
            entity.Property(e => e.BloodSuggar).HasColumnName("blood_suggar");
            entity.Property(e => e.BodyFat).HasColumnName("body_fat");
            entity.Property(e => e.MeasuredDate)
                .HasMaxLength(45)
                .HasColumnName("measured_date");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Patient).WithMany(p => p.InternistData)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("patient_appointment");
        });

        modelBuilder.Entity<MedicalReferral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medical_referral");

            entity.HasIndex(e => e.DoctorId, "doctor_refe_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.PatientId, "patient_refe_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.IsUsed).HasColumnName("is_used");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Specialization)
                .HasMaxLength(45)
                .HasColumnName("specialization");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalReferralDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("doctor_refe");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalReferralPatients)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("patient_refe");
        });

        modelBuilder.Entity<MedicalReport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("medical_report");

            entity.HasIndex(e => e.AppointmentId, "appointment_report_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(45)
                .HasColumnName("diagnosis");

            entity.HasOne(d => d.Appointment).WithMany(p => p.MedicalReports)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("appointment_report");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification");

            entity.HasIndex(e => e.AdminId, "author_wrote_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Content)
                .HasMaxLength(45)
                .HasColumnName("content");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .HasColumnName("title");

            entity.HasOne(d => d.Admin).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("author_wrote");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.AssignedGeneralPraticeId, "general_practice_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedGeneralPraticeId).HasColumnName("assigned_general_pratice_id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.IsBlocked).HasColumnName("is_blocked");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.NumberOfPenalties).HasColumnName("number_of_penalties");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .HasColumnName("role");
            entity.Property(e => e.Specialization)
                .HasMaxLength(45)
                .HasColumnName("specialization");

            entity.HasOne(d => d.AssignedGeneralPratice).WithMany(p => p.InverseAssignedGeneralPratice)
                .HasForeignKey(d => d.AssignedGeneralPraticeId)
                .HasConstraintName("general_practice");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
