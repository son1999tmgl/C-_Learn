using Microsoft.EntityFrameworkCore;
using WebApi.Data;

namespace WebApi.Controllers.Data
{
    public class DTMyDbContext : DbContext

    {
        public DTMyDbContext(DbContextOptions options): base(options) { }


        #region DBSet

        public DbSet<DTRefreshToken> RefreshTokens { get; set; }
        public DbSet<DTNguoiDung> NguoiDungs { get; set; } 
        public DbSet<DTHangHoa> HangHoas { get; set; }
       
        public DbSet<DTLoai> Loais { get; set; }

        public DbSet<DTDonHang> DonHangs{ get; set; }


        public DbSet<DTDonHangChiTiet> DonHangChiTiets{ get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DTDonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.NgayDat).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<DTDonHangChiTiet>(entity =>
            {
                entity.ToTable("ChiTietDonHang");
                entity.HasKey(e => new { e.MaDh, e.MaHh });

                entity.HasOne(e => e.DonHang)
                    .WithMany(e => e.DonHangChiTiets)
                    .HasForeignKey(e => e.MaDh)
                    .HasConstraintName("FK_DonHangCT_DonHang");

                 entity.HasOne(e => e.HangHoa)
                    .WithMany(e => e.DonHangChiTiets)
                    .HasForeignKey(e => e.MaHh)
                    .HasConstraintName("FK_DonHangCT_HangHoa");

            });

            modelBuilder.Entity<DTNguoiDung>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                
            });
        }


    }
}
