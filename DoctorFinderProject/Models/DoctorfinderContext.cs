using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DoctorFinderProject.Models
{
    public partial class DoctorfinderContext : DbContext
    {
        public DoctorfinderContext()
        {
        }

        public DoctorfinderContext(DbContextOptions<DoctorfinderContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admintbl> Admintbls { get; set; }
        public virtual DbSet<Citytbl> Citytbls { get; set; }
        public virtual DbSet<Doctortbl> Doctortbls { get; set; }
        public virtual DbSet<Hospitaltbl> Hospitaltbls { get; set; }
        public virtual DbSet<Pateinttbl> Pateinttbls { get; set; }
        public virtual DbSet<Statetbl> Statetbls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NR72NMI;Database=Doctorfinder;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admintbl>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("Admintbl");

                entity.Property(e => e.AdminId).HasColumnName("Admin_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Citytbl>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("Citytbl");

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("City_name");

                entity.Property(e => e.StateId).HasColumnName("State_id");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Citytbls)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Citytbl_Statetbl");
            });

            modelBuilder.Entity<Doctortbl>(entity =>
            {
                entity.HasKey(e => e.DoctorId);

                entity.ToTable("Doctortbl");

                entity.Property(e => e.DoctorId).HasColumnName("Doctor_id");

                entity.Property(e => e.Degree)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_name");

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_id");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last_name");

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Phone_no");

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Profile_image");

                entity.Property(e => e.Speciality)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Hospital)
                    .WithMany(p => p.Doctortbls)
                    .HasForeignKey(d => d.HospitalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctortbl_Hospitaltbl");
            });

            modelBuilder.Entity<Hospitaltbl>(entity =>
            {
                entity.HasKey(e => e.HospitalId);

                entity.ToTable("Hospitaltbl");

                entity.Property(e => e.HospitalId).HasColumnName("Hospital_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.ContactNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Contact_no");

                entity.Property(e => e.Description)
                    .HasMaxLength(5000)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HospitalName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Hospital_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Profile_image");

                entity.Property(e => e.StateId).HasColumnName("State_id");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Hospitaltbls)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hospitaltbl_Citytbl");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Hospitaltbls)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hospitaltbl_Statetbl");
            });

            modelBuilder.Entity<Pateinttbl>(entity =>
            {
                entity.HasKey(e => e.PateintId);

                entity.ToTable("Pateinttbl");

                entity.Property(e => e.PateintId).HasColumnName("Pateint_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("First_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Last_name");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Profile_image");

                entity.Property(e => e.StateId).HasColumnName("State_id");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Pateinttbls)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pateinttbl_Citytbl");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Pateinttbls)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pateinttbl_Statetbl");
            });

            modelBuilder.Entity<Statetbl>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.ToTable("Statetbl");

                entity.Property(e => e.StateId).HasColumnName("State_id");

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("State_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
