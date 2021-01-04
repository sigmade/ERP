using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DBModels
{
    public partial class MasterDBContext : DbContext
    {
        public MasterDBContext()
        {
        }

        public MasterDBContext(DbContextOptions<MasterDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Local> Locals { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Worktime> Worktimes { get; set; }
        public virtual DbSet<File> Files { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MasterDB;Trusted_Connection=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Local>(entity =>
            {
                entity.ToTable("Local");

                entity.Property(e => e.LocalId)
                    .ValueGeneratedNever()
                    .HasColumnName("local_id");

                entity.Property(e => e.LocalName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("local_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.DateBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("full_name");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.RegionId)
                    .HasMaxLength(50)
                    .HasColumnName("region_id");

                entity.Property(e => e.SocNum)
                    .HasMaxLength(50)
                    .HasColumnName("soc_num");

                entity.Property(e => e.TaxNum).HasColumnName("tax_num");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.PositionId).HasColumnName("position_id");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("position_name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_RoleId");
            });

            modelBuilder.Entity<Worktime>(entity =>
            {
                entity.ToTable("Worktime");

                entity.Property(e => e.WorktimeId).HasColumnName("worktime_id");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.LocalId).HasColumnName("local_id");

                entity.Property(e => e.PersonId).HasColumnName("person_id");

                entity.Property(e => e.PositionId).HasColumnName("position_id");

                entity.HasOne(d => d.Local)
                    .WithMany(p => p.Worktimes)
                    .HasForeignKey(d => d.LocalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Worktime_Local");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Worktimes)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Worktime_Personid");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Worktimes)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Worktime_Position");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Path);

                entity.Property(e => e.PersonId);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("entity.Property(e => e.Name);");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
