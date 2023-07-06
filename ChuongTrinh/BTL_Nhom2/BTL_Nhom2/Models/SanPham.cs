namespace BTL_Nhom2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(50)]
        public string TenSP { get; set; }

        public int? DoiTuongId { get; set; }

        public int? TheLoaiId { get; set; }

        public int? ChatLieuId { get; set; }

        public int? NSXId { get; set; }

        [StringLength(50)]
        public string DonViTinh { get; set; }

        public int? SoLuong { get; set; }

        public int? DonGia { get; set; }

        [Column(TypeName = "ntext")]
        public string MoTa { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? NgayCapNhat { get; set; }

        [StringLength(50)]
        public string HinhMinhHoa { get; set; }

        public virtual ChatLieu ChatLieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }

        public virtual DoiTuong DoiTuong { get; set; }

        public virtual NhaSanXuat NhaSanXuat { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
