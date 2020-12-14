using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project2.Api
{
    public partial class DHLProject2SchoolContext : DbContext
    {
        public DHLProject2SchoolContext()
        {
        }

        public DHLProject2SchoolContext(DbContextOptions<DHLProject2SchoolContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Transcript> Transcripts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.BuildingName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.CourseDescription)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CourseName)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.RoomNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_ClassBuildingId_BuildingId");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_ClassDepartmentId_DepartmentID");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_ClassInstructorId_InstructorId");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Dean)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DeanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentDean_Instructor");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.StudentId })
                    .HasName("PK__Enrollme__4A01231E14A8F096");

                entity.ToTable("Enrollment");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_EnrollmentCourseId_CourseId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_EnrollmentStudentId_PersonId");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.Property(e => e.InstructorId).ValueGeneratedNever();

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorDepartmentId_Department");

                entity.HasOne(d => d.InstructorNavigation)
                    .WithOne(p => p.Instructor)
                    .HasForeignKey<Instructor>(d => d.InstructorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InstructorId_PersonID");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasIndex(e => e.Email, "UQ__Person__A9D10534A8B4190A")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Transcript>(entity =>
            {
                entity.HasKey(e => new { e.PersonId, e.CourseId })
                    .HasName("PK__Transcri__C6BD2CFFB0CA8AA2");

                entity.ToTable("Transcript");

                entity.Property(e => e.Grade)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Transcripts)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_TranscriptCourseId_CourseId");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Transcripts)
                    .HasForeignKey(d => d.PersonId)
                    .HasConstraintName("FK_TranscriptPersonId_PersonId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
