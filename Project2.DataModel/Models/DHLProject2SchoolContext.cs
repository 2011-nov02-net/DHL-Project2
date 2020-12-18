using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project2.DataModel
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
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseAssistant> CourseAssistants { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Waitlist> Waitlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.Name).HasMaxLength(120);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "UQ__Category__737584F6949B65A5")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.HasIndex(e => e.Name, "UQ__Course__737584F68849356B")
                    .IsUnique();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Category)
                    .HasConstraintName("FK_CourseCategory_CategoryId");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_CourseDepartmentId_DepartmentId");

                entity.HasOne(d => d.SessionNavigation)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.Session)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CourseSession_SessionId");
            });

            modelBuilder.Entity<CourseAssistant>(entity =>
            {
                entity.HasKey(e => new { e.AssistantId, e.CourseId })
                    .HasName("PK__CourseAs__5BC4202A324E6E65");

                entity.ToTable("CourseAssistant");

                entity.Property(e => e.Role).HasMaxLength(15);

                entity.HasOne(d => d.Assistant)
                    .WithMany(p => p.CourseAssistants)
                    .HasForeignKey(d => d.AssistantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseAssistantId_UserId");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseAssistants)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseAssistantCourseId_CourseId");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.HasIndex(e => e.Name, "UQ__Departme__737584F609D60217")
                    .IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Dean)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DeanId)
                    .HasConstraintName("FK_DepartmentDeanId_UserId");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.User, e.Course })
                    .HasName("PK__Enrollme__5C7484700467CB6D");

                entity.ToTable("Enrollment");

                entity.HasOne(d => d.CourseNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.Course)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentCourseId_CourseId");

                entity.HasOne(d => d.GradeNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.Grade)
                    .HasConstraintName("FK_EnrollmentGrade_GradeId");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EnrollmentUserId_UserId");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.Letter).HasMaxLength(2);
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(e => new { e.InstructorId, e.CourseId })
                    .HasName("PK__Instruct__F193DD81DEBF8655");

                entity.ToTable("Instructor");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_InstructorCourseId_CourseId");

                entity.HasOne(d => d.InstructorNavigation)
                    .WithMany(p => p.Instructors)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_InstructorId_UserId");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__Permissi__A25C5AA65EF10E46");

                entity.ToTable("Permission");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => new { e.Room, e.Start })
                    .HasName("PK__Reservat__21BD274AE4EC1F0A");

                entity.ToTable("Reservation");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_ReservationCourse_CourseId");

                entity.HasOne(d => d.RoomNavigation)
                    .WithMany(p => p.Reservations)
                    .HasPrincipalKey(p => p.Id)
                    .HasForeignKey(d => d.Room)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservationRoom_RoomId");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.BuildingId })
                    .HasName("PK__Room__2DE79D40825A4DF2");

                entity.ToTable("Room");

                entity.HasIndex(e => e.Id, "UQ__Room__3214EC06A2320245")
                    .IsUnique();

                entity.Property(e => e.Number).HasColumnType("decimal(5, 0)");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK_RoomBuildingId_BuildingId");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("Session");

                entity.Property(e => e.End).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Start).HasColumnType("date");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ__User__A9D10534982F282C")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(120)
                    .HasColumnName("Full_Name");

                entity.HasOne(d => d.PermissionNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Permission)
                    .HasConstraintName("FK_UserPermission_Permission");
            });

            modelBuilder.Entity<Waitlist>(entity =>
            {
                entity.HasKey(e => new { e.User, e.CourseId })
                    .HasName("PK__Waitlist__D1B211EAFF0AFA86");

                entity.ToTable("Waitlist");

                entity.Property(e => e.Added).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Waitlists)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WaitlistCourseId_CourseId");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Waitlists)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WaitlistUserId_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
