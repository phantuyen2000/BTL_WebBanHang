namespace BTL_Nhom2.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ChatLieu")]
    public partial class ChatLieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChatLieu()
        {
            SanPhams = new HashSet<SanPham>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Tên chất liệu không được để trống!")]
        [StringLength(100)]
        public string TenChatLieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
