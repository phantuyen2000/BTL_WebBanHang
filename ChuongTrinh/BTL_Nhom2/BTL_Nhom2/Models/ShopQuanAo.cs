using System.Data.Entity;

namespace BTL_Nhom2.Models
{
    public partial class ShopQuanAo : DbContext
    {
        public ShopQuanAo()
            : base("name=ShopQuanAo")
        {
        }

        public virtual DbSet<ChatLieu> ChatLieux { get; set; }
        public virtual DbSet<ChiTietDatHang> ChiTietDatHangs { get; set; }
        public virtual DbSet<DoiTuong> DoiTuongs { get; set; }
        public virtual DbSet<DonDatHang> DonDatHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<KichCo> KichCoes { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TheLoai> TheLoais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatLieu>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.ChatLieu)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DoiTuong>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.DoiTuong)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonDatHangs)
                .WithOptional(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KichCo>()
                .HasMany(e => e.ChiTietDatHangs)
                .WithOptional(e => e.KichCo)
                .HasForeignKey(e => e.SizeId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhaSanXuat>()
                .Property(e => e.DienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<NhaSanXuat>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.NhaSanXuat)
                .HasForeignKey(e => e.NSXId)
                .WillCascadeOnDelete();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.HinhMinhHoa)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.KhachHangs)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.AccID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.NhanViens)
                .WithOptional(e => e.TaiKhoan)
                .HasForeignKey(e => e.AccID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TheLoai>()
                .HasMany(e => e.SanPhams)
                .WithOptional(e => e.TheLoai)
                .WillCascadeOnDelete();
        }
    }
}
