namespace BTL_Nhom2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            DonDatHangs = new HashSet<DonDatHang>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên khách hàng không được để trống!")]
        [StringLength(50)]
        public string HoTen { get; set; }


        [StringLength(100)]
        public string DiaChi { get; set; }


        [StringLength(20)]
        public string DienThoai { get; set; }


        public int? AccID { get; set; }


        [Required(ErrorMessage = "Ảnh không được để trống!")]
        [StringLength(100)]
        public string Avatar { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonDatHang> DonDatHangs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
