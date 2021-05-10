using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Outsourced.Models
{
    public partial class OutsourcedContext : DbContext
    {
        public OutsourcedContext()
        {
        }

        public OutsourcedContext(DbContextOptions<OutsourcedContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Community> Communities { get; set; }
        public virtual DbSet<ProjectNews> ProjectNews { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<TypeCommunity> TypeCommunities { get; set; }
        public virtual DbSet<TypeRequest> TypeRequests { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-QOAEE45\\SQLEXPRESS; Database=Outsourced; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.UserName);

                entity.ToTable("Administrator");

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Community>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Community");

                entity.Property(e => e.Administrator)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DateWebinar).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Header)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AdministratorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Administrator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Community_Administrator");
            });

            modelBuilder.Entity<ProjectNews>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Administrator)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.BackGround).IsRequired();

                entity.Property(e => e.DateRelease).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Header).IsRequired();

                entity.Property(e => e.SubHeader).IsRequired();
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.Administrator).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(50);

                entity.Property(e => e.UserRequest)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AdministratorNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.Administrator)
                    .HasConstraintName("FK_Request_Administrator");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_TypeRequest");

                entity.HasOne(d => d.UserRequestNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.UserRequest)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Request_User");
            });

            modelBuilder.Entity<TypeCommunity>(entity =>
            {
                entity.HasKey(e => e.NameType)
                    .HasName("PK_TypeCommunity_1");

                entity.ToTable("TypeCommunity");

                entity.Property(e => e.NameType).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeRequest>(entity =>
            {
                entity.HasKey(e => e.NameTypeRequest);

                entity.ToTable("TypeRequest");

                entity.Property(e => e.NameTypeRequest).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
