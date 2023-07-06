namespace BTL_Nhom2.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NhanVien")]
    public partial class NhanVien
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống!")]
        [StringLength(50)]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(20)]
        public string DienThoai { get; set; }

        [StringLength(100)]
        public string DiaChi { get; set; }

        [StringLength(100)]
        public string Avatar { get; set; }

        public int? AccID { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
