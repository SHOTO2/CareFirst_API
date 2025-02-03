using System;
using System.Collections.Generic;
using CareFirst.Model;
using Microsoft.EntityFrameworkCore;

namespace CareFirst.Context;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<AppSetting> AppSettings { get; set; }

    public virtual DbSet<AppointmentDoctor> AppointmentDoctors { get; set; }

    public virtual DbSet<AppointmentNurse> AppointmentNurses { get; set; }

    public virtual DbSet<AwarenessVideo> AwarenessVideos { get; set; }

    public virtual DbSet<BookDoctor> BookDoctors { get; set; }

    public virtual DbSet<BookNurse> BookNurses { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<DoctorDepartment> DoctorDepartments { get; set; }

    public virtual DbSet<DoctorMedicalCenter> DoctorMedicalCenters { get; set; }

    public virtual DbSet<DoctorsAvailableTime> DoctorsAvailableTimes { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<NurseDepartment> NurseDepartments { get; set; }

    public virtual DbSet<NurseMedicalCenter> NurseMedicalCenters { get; set; }

    public virtual DbSet<ReviewDoctor> ReviewDoctors { get; set; }

    public virtual DbSet<SupperUser> SupperUsers { get; set; }

    public virtual DbSet<Symptom> Symptoms { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppointmentDoctor>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA204777518");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AppointmentDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__3429BB53");

            entity.HasOne(d => d.User).WithMany(p => p.AppointmentDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__User___351DDF8C");
        });

        modelBuilder.Entity<AppointmentNurse>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA27E9FECBB");

            entity.HasOne(d => d.Nurse).WithMany(p => p.AppointmentNurses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Nurse__4A18FC72");

            entity.HasOne(d => d.User).WithMany(p => p.AppointmentNurses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__User___4B0D20AB");
        });

        modelBuilder.Entity<AwarenessVideo>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PK__Awarenes__BAE5124A2A2CB6BC");
        });

        modelBuilder.Entity<BookDoctor>(entity =>
        {
            entity.HasKey(e => e.BookDoctorId).HasName("PK__BookDoct__5C665E5E2BCC2D74");

            entity.HasOne(d => d.Doctor).WithMany(p => p.BookDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookDocto__Docto__3AD6B8E2");

            entity.HasOne(d => d.User).WithMany(p => p.BookDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookDocto__User___3BCADD1B");
        });

        modelBuilder.Entity<BookNurse>(entity =>
        {
            entity.HasKey(e => e.BookNurseId).HasName("PK__BookNurs__86DACD43FB357395");

            entity.HasOne(d => d.Nurse).WithMany(p => p.BookNurses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookNurse__Nurse__4DE98D56");

            entity.HasOne(d => d.User).WithMany(p => p.BookNurses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookNurse__User___4EDDB18F");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PK__Disease__69B533A9009E97BA");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__2DC00EDFA5567581");

            entity.HasOne(d => d.Department).WithMany(p => p.Doctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor__Departme__30592A6F");

            entity.HasOne(d => d.MedicalCenter).WithMany(p => p.Doctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctor__MedicalC__314D4EA8");
        });

        modelBuilder.Entity<DoctorDepartment>(entity =>
        {
            entity.HasKey(e => e.DoctorAppointmentId).HasName("PK__DoctorDe__0C56E767129044D9");
        });

        modelBuilder.Entity<DoctorMedicalCenter>(entity =>
        {
            entity.HasKey(e => e.DoctorMedicalCenterId).HasName("PK__DoctorMe__B2E93E85AEC84BCF");
        });

        modelBuilder.Entity<DoctorsAvailableTime>(entity =>
        {
            entity.HasKey(e => e.AvailableTimeId).HasName("PK__DoctorsA__6EC40C2486A70DF9");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DoctorsAvailableTimes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DoctorsAv__Docto__37FA4C37");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.NurseId).HasName("PK__Nurse__4384786950687BBD");

            entity.HasOne(d => d.Department).WithMany(p => p.Nurses).HasConstraintName("FK__Nurse__Departmen__46486B8E");

            entity.HasOne(d => d.MedicalCenter).WithMany(p => p.Nurses).HasConstraintName("FK__Nurse__MedicalCe__473C8FC7");
        });

        modelBuilder.Entity<NurseDepartment>(entity =>
        {
            entity.HasKey(e => e.NurseAppointmentId).HasName("PK__NurseDep__2E2C437A97183C8C");
        });

        modelBuilder.Entity<NurseMedicalCenter>(entity =>
        {
            entity.HasKey(e => e.NurseMedicalCenterId).HasName("PK__NurseMed__056ABD0DDFA31F2F");
        });

        modelBuilder.Entity<ReviewDoctor>(entity =>
        {
            entity.HasKey(e => e.ReviewDoctorId).HasName("PK__ReviewDo__7E37B3A3D916E83B");

            entity.HasOne(d => d.AppointmentDoctor).WithMany(p => p.ReviewDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewDoc__Appoi__3F9B6DFF");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ReviewDoctors)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ReviewDoc__Docto__3EA749C6");
        });

        modelBuilder.Entity<SupperUser>(entity =>
        {
            entity.HasKey(e => e.SupperUserId).HasName("PK__SupperUs__670B66F848B8786E");
        });

        modelBuilder.Entity<Symptom>(entity =>
        {
            entity.HasKey(e => e.SymptomId).HasName("PK__Symptom__D26ED8B612615737");

            entity.HasOne(d => d.Disease).WithMany(p => p.Symptoms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Symptom__Disease__53A266AC");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserTabl__1788CCAC7D15E634");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
