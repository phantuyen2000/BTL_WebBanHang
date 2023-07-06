using BTL_Nhom2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL_Nhom2.Controllers
{
    public class NguoiDungController : Controller
    {
        ShopQuanAo db = new ShopQuanAo();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, TaiKhoan tk, KhachHang kh)
        {
            kh.Avatar = "";
            var hoten = collection["HoTenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var matkhauagain = collection["MatKhauNhapLai"];
            var diachi = collection["DiaChi"];
            var dienthoai = collection["DienThoai"];
            var avatar = Request.Files["imgFile"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);
            var gioitinh = collection["GioTinh"];
            var email = collection["Email"];
            if (avatar != null)
            {
                string fileName = System.IO.Path.GetFileName(avatar.FileName);
                string uploadPath = Server.MapPath("~/Content/images/" + fileName);
                avatar.SaveAs(uploadPath);
                kh.Avatar = fileName;
            }
            else
            {
                ViewData["Loi7"] = "Chưa chọn ảnh đại diện.";
            }
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống.";
            }
            else if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Vui lòng nhập tên đăng nhập.";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Vui lòng nhập mật khẩu.";
            }
            else if (string.IsNullOrEmpty(matkhauagain))
            {
                ViewData["Loi4"] = "Vui lòng nhập lại mật khẩu.";
            }
            else if (matkhau != matkhauagain)
            {
                ViewData["Loi4"] = "Mật khẩu không khớp.";
            }
            else if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi5"] = "Vui lòng nhập địa chỉ.";
            }
            else if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Vui lòng nhập số điện thoại.";
            }
            else if (string.IsNullOrEmpty(ngaysinh))
            {
                ViewData["Loi8"] = "Vui lòng nhập ngày sinh.";
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi9"] = "Vui lòng nhập email.";
            }
            else
            {
                tk.UserName = tendn;
                tk.PassWord = matkhau;
                tk.Allowed = 0;
                db.TaiKhoans.Add(tk);
                db.SaveChanges();
                TaiKhoan t = db.TaiKhoans.SingleOrDefault(x => x.UserName == tendn);
                kh.HoTen = hoten;
                kh.DiaChi = diachi;
                kh.DienThoai = dienthoai;
                kh.AccID = t.Id;
                kh.NgaySinh = DateTime.Parse(ngaysinh);
                if (gioitinh == "Nam")
                {
                    kh.GioiTinh = true;
                }
                else
                {
                    kh.GioiTinh = false;
                }
                kh.Email = email;
                db.KhachHangs.Add(kh);
                db.SaveChanges();
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                TaiKhoan tk = db.TaiKhoans.SingleOrDefault(t => t.UserName == tendn && t.PassWord == matkhau);
                if (tk != null)
                {
                    if (tk.Allowed == 1)
                    {
                        NhanVien nv = db.NhanViens.SingleOrDefault(n => n.AccID == tk.Id);
                        Session["NhanVien"] = nv;
                        Session["UserName"] = nv.HoTen;
                        Session["Avatar"] = nv.Avatar;
                        return RedirectToAction("Index", "Admin/HomeAd");
                    }
                    else
                    {
                        KhachHang kh = db.KhachHangs.SingleOrDefault(k => k.AccID == tk.Id);
                        Session["TaiKhoan"] = kh;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng.";
                }
            }

            return View();
        }
    }
}