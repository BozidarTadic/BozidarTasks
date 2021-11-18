using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Tasks.Data.Data
{
    public partial class TasksEntities : DbContext
    {
        public TasksEntities()
        {
        }

        public TasksEntities(DbContextOptions<TasksEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrentStatus> CurrentStatuses { get; set; }
        public virtual DbSet<Priority> Priorities { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP;Database=Tasks;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CurrentStatus>(entity =>
            {
                entity.ToTable("CurrentStatus");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CurrentStatus1)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("CurrentStatus")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Priority>(entity =>
            {
                entity.ToTable("Priority");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompletionDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.CurrentStatus)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CurrentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_CurrentStatus");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Priority");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Status1)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Status")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Desription)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Priority");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Project");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tasks_Status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
