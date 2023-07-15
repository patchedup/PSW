using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using back.Models;

namespace back.Data;

public partial class HospitalContext : DbContext
{
    public HospitalContext(DbContextOptions<HospitalContext> options)
        : base(options)
    {
    }

    public HospitalContext()
        : base()
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<InternistData> InternistData { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Referral> Referrals { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

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

            entity.HasIndex(e => e.DoctorId, "FK1l62fhlqqc08wmgrvn7hjtfom");

            entity.HasIndex(e => e.ReportId, "FK3uhdl6e2tryg7ujxdb37g5726");

            entity.HasIndex(e => e.MeasuredInternistDataId, "FK5wcjq3m0ibc3rt67sjpu1fo8g");

            entity.HasIndex(e => e.PatientId, "FKg90ck1kd0p4rbamwc22jd2oql");

            entity.HasIndex(e => e.ReferralId, "FKpji5lnouj2myh2u714v6ms11t");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.MeasuredInternistDataId).HasColumnName("measured_internist_data_id");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.ReferralId).HasColumnName("referral_id");
            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.Time)
                .HasMaxLength(255)
                .HasColumnName("time");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AppointmentDoctors)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK1l62fhlqqc08wmgrvn7hjtfom");

            entity.HasOne(d => d.MeasuredInternistData).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.MeasuredInternistDataId)
                .HasConstraintName("FK5wcjq3m0ibc3rt67sjpu1fo8g");

            entity.HasOne(d => d.Patient).WithMany(p => p.AppointmentPatients)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FKg90ck1kd0p4rbamwc22jd2oql");

            entity.HasOne(d => d.Referral).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ReferralId)
                .HasConstraintName("FKpji5lnouj2myh2u714v6ms11t");

            entity.HasOne(d => d.Report).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ReportId)
                .HasConstraintName("FK3uhdl6e2tryg7ujxdb37g5726");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("blog");

            entity.HasIndex(e => e.UserId, "FKpxk2jtysqn41oop7lvxcp6dqq");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKpxk2jtysqn41oop7lvxcp6dqq");
        });

        modelBuilder.Entity<InternistData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("internist_data");

            entity.HasIndex(e => e.UserId, "FKbf1sri1r4gfqre7m90x7blucl");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BloodPressure)
                .HasMaxLength(255)
                .HasColumnName("blood_pressure");
            entity.Property(e => e.BloodSugar).HasColumnName("blood_sugar");
            entity.Property(e => e.BodyFat).HasColumnName("body_fat");
            entity.Property(e => e.Date)
                .HasMaxLength(255)
                .HasColumnName("date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.User).WithMany(p => p.InternistData)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKbf1sri1r4gfqre7m90x7blucl");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification");

            entity.HasIndex(e => e.AdminId, "FKsobdu7xwna27ygsaju5ab30h5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminId).HasColumnName("admin_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");

            entity.HasOne(d => d.Admin).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FKsobdu7xwna27ygsaju5ab30h5");
        });

        modelBuilder.Entity<Referral>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("referral");

            entity.HasIndex(e => e.ForDoctorId, "FKb7srwnq0jesxnyckmejpkebio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ForDoctorId).HasColumnName("for_doctor_id");
            entity.Property(e => e.IsUsed)
                .HasColumnType("bit(1)")
                .HasColumnName("is_used");

            entity.HasOne(d => d.ForDoctor).WithMany(p => p.Referrals)
                .HasForeignKey(d => d.ForDoctorId)
                .HasConstraintName("FKb7srwnq0jesxnyckmejpkebio");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("report");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .HasColumnName("diagnosis");
            entity.Property(e => e.Treatment)
                .HasMaxLength(255)
                .HasColumnName("treatment");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.AssignedGeneralPracticeDoctorId, "FKoxg7636eoyfmx5v1w5iq81q58");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedGeneralPracticeDoctorId).HasColumnName("assigned_general_practice_doctor_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.IsBlocked)
                .HasColumnType("bit(1)")
                .HasColumnName("is_blocked");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.NumberOfPenalties).HasColumnName("number_of_penalties");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasColumnName("role");
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .HasColumnName("specialization");

            entity.HasOne(d => d.AssignedGeneralPracticeDoctor).WithMany(p => p.InverseAssignedGeneralPracticeDoctor)
                .HasForeignKey(d => d.AssignedGeneralPracticeDoctorId)
                .HasConstraintName("FKoxg7636eoyfmx5v1w5iq81q58");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
