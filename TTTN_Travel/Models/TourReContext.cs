using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TTTN_Travel.Models
{
    public partial class TourReContext : DbContext
    {
        public TourReContext()
        {
        }

        public TourReContext(DbContextOptions<TourReContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Addmin> Admin { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Dattour> Dattour { get; set; }
        public virtual DbSet<Kh> Kh { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }

        public virtual DbSet<Tintuc> Tintuc { get; set; }
        public virtual DbSet<Tout> Tout { get; set; }
        public virtual DbSet<Statistical> Statistical { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-K3U1KN4\\SQLEXPRESS;Database=TourRe;Trusted_Connection=True;");
            }
        }
*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Addmin>(entity =>
            {
                entity.HasKey(e => e.Idad);

                entity.ToTable("ADDMIN");

                entity.Property(e => e.Idad).HasColumnName("IDAD");

                entity.Property(e => e.Image)
                    .HasColumnName("IMAGE")
                    .HasMaxLength(50);

                entity.Property(e => e.Passwork)
                    .HasColumnName("PASSWORK")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("USER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Image)
                    .HasColumnName("IMAGE")
                    .HasMaxLength(50);

                entity.Property(e => e.Pass)
                    .HasColumnName("PASS")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50);
                entity.Property(e => e.Cmt)
                    .HasColumnName("CMT")
                    .HasMaxLength(50);
                entity.Property(e => e.Phone)
                    .HasColumnName("PHONE")
                    .HasMaxLength(10);
                entity.Property(e => e.Adress)
                    .HasColumnName("ADRESS")
                    .HasMaxLength(50);
                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
                entity.Property(e => e.Gt)
                    .HasColumnName("GENDER");
            });

            modelBuilder.Entity<Statistical>(entity =>
            {
                entity.ToTable("Statistical");

                entity.Property(e => e.ID).HasColumnName("ID");
                entity.Property(e => e.Visit).HasColumnName("VISIT");

            });

            modelBuilder.Entity<Dattour>(entity =>
            {
                entity.ToTable("DATTOUR");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dc)
                    .IsRequired()
                    .HasColumnName("DC")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50);

                entity.Property(e => e.Ghichu)
                    .HasColumnName("GHICHU")
                    .HasMaxLength(50);

                entity.Property(e => e.Hoten)
                    .IsRequired()
                    .HasColumnName("HOTEN")
                    .HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .IsRequired()
                    .HasColumnName("SDT")
                    .HasMaxLength(15);

                entity.Property(e => e.Songuoi).HasColumnName("SONGUOI");
                entity.Property(e => e.Treem).HasColumnName("TREEM");

                entity.Property(e => e.Tentuor)
                    .IsRequired()
                    .HasColumnName("TENTUOR")
                    .HasMaxLength(50);

                entity.Property(e => e.Thanhtien)
                    .IsRequired()
                    .HasColumnName("THANHTIEN")
                    .HasMaxLength(50);
                entity.Property(e => e.Trangthai)
                   .IsRequired()
                   .HasColumnName("TRANGTHAI")
                   .HasMaxLength(50);
            });

            modelBuilder.Entity<Kh>(entity =>
            {
                entity.HasKey(e => e.Idkh);

                entity.ToTable("KH");

                entity.Property(e => e.Idkh).HasColumnName("IDKH");

                entity.Property(e => e.Dc)
                    .HasColumnName("DC")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50);

                entity.Property(e => e.Hoten)
                    .HasColumnName("HOTEN")
                    .HasMaxLength(50);

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasKey(e => e.Idtour);

                entity.ToTable("TOUR");

                entity.Property(e => e.Idtour).HasColumnName("IDTOUR");

                entity.Property(e => e.Danhgia).HasColumnName("DANHGIA");

                entity.Property(e => e.Gia)
                    .HasColumnName("GIA")
                    .HasMaxLength(50);

                entity.Property(e => e.Image)
                    .HasColumnName("IMAGE")
                    .HasMaxLength(50);

                entity.Property(e => e.Lichtrinh).HasColumnName("LICHTRINH");

                entity.Property(e => e.Mota).HasColumnName("MOTA");

                entity.Property(e => e.Ngaybd)
                    .HasColumnName("NGAYBD")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ngaykt)
                    .HasColumnName("NGAYKT")
                    .HasColumnType("datetime");

                entity.Property(e => e.Quocgia)
                    .HasColumnName("QUOCGIA")
                    .HasMaxLength(50);

                entity.Property(e => e.Tentuor)
                    .HasColumnName("TENTUOR")
                    .HasMaxLength(150);

                entity.Property(e => e.Giatre)
                    .HasColumnName("GIATRE")
                    .HasMaxLength(50);

                entity.Property(e => e.Trongnuoc).HasColumnName("TRONGNUOC");
            });

            modelBuilder.Entity<Tout>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TOUT");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Rate).HasColumnName("RATE");

                entity.Property(e => e.Giakm)
                    .HasColumnName("GIAKM")
                    .HasMaxLength(50);

                entity.Property(e => e.Image)
                    .HasColumnName("IMAGE")
                    .HasMaxLength(50);

                entity.Property(e => e.Lichtrinh).HasColumnName("LICHTRINH");

                entity.Property(e => e.Mota).HasColumnName("MOTA");

                entity.Property(e => e.Ngay)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Giatre)
                    .HasColumnName("GIATRE")
                    .HasMaxLength(50);

                entity.Property(e => e.Chitiet)
                    .HasColumnName("CHITIET")
                    .HasMaxLength(50);

                entity.Property(e => e.Ten)
                    .HasColumnName("TEN")
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Tintuc>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("TINTUC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Tomtat)
                    .HasColumnName("TOMTAT")
                    .HasMaxLength(50);

                entity.Property(e => e.Anhtintuc)
                    .HasColumnName("ANHTINTUC")
                    .HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnName("DATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.Chitiet)
                    .HasColumnName("CHITIET")
                    .HasMaxLength(50);

                entity.Property(e => e.Tentintuc)
                    .HasColumnName("TENTINTUC")
                    .HasMaxLength(150);

            });
        }
    }
}
