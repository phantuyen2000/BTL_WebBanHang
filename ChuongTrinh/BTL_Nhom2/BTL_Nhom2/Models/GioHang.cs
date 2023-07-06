using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_Nhom2.Models
{
    public class GioHang
    {
        ShopQuanAo db = new ShopQuanAo();
        public int iSPId { get; set; }
        public string sTenSP { get; set; }
        public string sHinhMinhHoa { get; set; }
        public string sDonViTinh { get; set; }
        public int iKichCo { get; set; }
        public int iDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return double.Parse((iSoLuong * iDonGia).ToString()); }
        }
        public GioHang(int maSP)
        {
            iSPId = maSP;
            SanPham sp = db.SanPhams.SingleOrDefault(s => s.Id == maSP);
            sTenSP = sp.TenSP;
            sHinhMinhHoa = sp.HinhMinhHoa;
            sDonViTinh = sp.DonViTinh;
            iDonGia = int.Parse(sp.DonGia.ToString());
            iSoLuong = 1;
        }
    }
}