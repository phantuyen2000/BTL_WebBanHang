using BTL_Nhom2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class NhanVienController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/NhanVien
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SapTheoNS = sortOrder == "tuoi" ? "tuoi_desc" : "tuoi";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var nhanVien = db.NhanViens.Select(n => n);
            if (!String.IsNullOrEmpty(searchString))
            {
                nhanVien = nhanVien.Where(k => k.HoTen.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ten_desc":
                    nhanVien = nhanVien.OrderByDescending(c => c.HoTen);
                    break;
                case "tuoi_desc":
                    nhanVien = nhanVien.OrderByDescending(c => c.NgaySinh);
                    break;
                case "tuoi":
                    nhanVien = nhanVien.OrderBy(c => c.NgaySinh);
                    break;
                default:
                    nhanVien = nhanVien.OrderBy(c => c.HoTen);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var khachHangs = db.KhachHangs.Include(k => k.TaiKhoan);

            return View(nhanVien.ToPagedList(pageNumber, pageSize));

            var nhanViens = db.NhanViens.Include(n => n.TaiKhoan);
            return View(nhanViens.ToList());
        }

        // GET: Admin/NhanVien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName");
            return View();
        }

        // POST: Admin/NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HoTen,GioiTinh,NgaySinh,DienThoai,DiaChi,Avatar,AccID")] NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.NhanViens.Add(nhanVien);
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName", nhanVien.AccID);
                return View(nhanVien);
            }

            
        }

        // GET: Admin/NhanVien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName", nhanVien.AccID);
            return View(nhanVien);
        }

        // POST: Admin/NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HoTen,GioiTinh,NgaySinh,DienThoai,DiaChi,Avatar,AccID")] NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nhanVien).State = EntityState.Modified;
                    db.SaveChanges();
                   
                } return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                 ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName", nhanVien.AccID);
            return View(nhanVien);
            }
            
        }

        // GET: Admin/NhanVien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhanVien nhanVien = db.NhanViens.Find(id);
            try
            {
                db.NhanViens.Remove(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xóa" + ex.Message;
                return View("Delete", nhanVien);

            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
