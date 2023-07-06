using BTL_Nhom2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom2.Controllers
{
    public class GioHangController : Controller
    {
        ShopQuanAo db = new ShopQuanAo();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> dsHang = Session["GioHang"] as List<GioHang>;
            if (dsHang == null)
            {
                dsHang = new List<GioHang>();
                Session["GioHang"] = dsHang;
            }
            return dsHang;
        }
        public ActionResult ThemGioHang(int maSP, string strURL)
        {
            List<GioHang> dsHang = LayGioHang();
            GioHang sp = dsHang.Find(n => n.iSPId == maSP);
            if (sp == null)
            {
                sp = new GioHang(maSP);
                dsHang.Add(sp);
                return Redirect(strURL);
            }
            else
            {
                sp.iSoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSL()
        {
            int iTongSL = 0;
            List<GioHang> dsHang = Session["GioHang"] as List<GioHang>;
            if (dsHang != null)
            {
                iTongSL = dsHang.Sum(s => s.iSoLuong);
            }
            return iTongSL;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> dsHang = Session["GioHang"] as List<GioHang>;
            if (dsHang != null)
            {
                dTongTien = dsHang.Sum(s => s.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> dsHang = LayGioHang();
            if (dsHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return View(dsHang);
        }
        public ActionResult XoaGioHang(int maSP)
        {
            List<GioHang> dsHang = LayGioHang();
            GioHang sp = dsHang.SingleOrDefault(s => s.iSPId == maSP);
            if (sp != null)
            {
                dsHang.RemoveAll(n => n.iSPId == maSP);
                return RedirectToAction("GioHang");
            }
            if (dsHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int maSP, FormCollection f)
        {
            List<GioHang> dsHang = LayGioHang();
            GioHang sp = dsHang.SingleOrDefault(s => s.iSPId == maSP);
            if (sp != null)
            {
                SanPham s = db.SanPhams.SingleOrDefault(x => x.Id == maSP);
                int slMua = int.Parse(f["txtSoLuong"].ToString());
                if (slMua > s.SoLuong)
                {
                    sp.iSoLuong = (int)s.SoLuong;
                }
                else
                {
                    sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult ClearGioHang()
        {
            List<GioHang> dsHang = LayGioHang();
            dsHang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> dsHang = LayGioHang();
            ViewBag.TongSL = TongSL();
            ViewBag.TongTien = TongTien();
            return View(dsHang);
        }
        public ActionResult DatHang(FormCollection f)
        {
            DonDatHang ddh = new DonDatHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            List<GioHang> dsHang = LayGioHang();
            ddh.KhachHangId = kh.Id;
            ddh.NgayDatHang = DateTime.Now;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            foreach (var item in dsHang)
            {
                ChiTietDatHang ct = new ChiTietDatHang();
                ct.DonDatHangId = ddh.Id;
                ct.SanPhamId = item.iSPId;
                ct.SoLuong = item.iSoLuong;
                ct.SizeId = 1;
                db.ChiTietDatHangs.Add(ct);
                db.SaveChanges();
                SanPham sp = db.SanPhams.SingleOrDefault(s => s.Id == item.iSPId);
                sp.SoLuong -= item.iSoLuong;
                db.SaveChanges();
            }
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}