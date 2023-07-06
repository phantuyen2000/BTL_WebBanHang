using BTL_Nhom2.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
using System;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/KhachHang
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SapTheoNS = sortOrder== "tuoi" ? "tuoi_desc" : "tuoi";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var khachHang = db.KhachHangs.Select(k => k);
            if (!String.IsNullOrEmpty(searchString))
            {
                khachHang = khachHang.Where(k => k.HoTen.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ten_desc":
                    khachHang = khachHang.OrderByDescending(c => c.HoTen);
                    break;
                case "tuoi_desc":
                    khachHang = khachHang.OrderByDescending(c => c.NgaySinh);
                    break;
                case "tuoi":
                    khachHang = khachHang.OrderBy(c => c.NgaySinh);
                    break;
                default:
                    khachHang = khachHang.OrderBy(c => c.HoTen);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var khachHangs = db.KhachHangs.Include(k => k.TaiKhoan);

            return View(khachHang.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KhachHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Create
        public ActionResult Create()
        {
            ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName");
            return View();
        }

        // POST: Admin/KhachHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HoTen,DiaChi,DienThoai,AccID,Avatar,NgaySinh,GioiTinh,Email")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName", khachHang.AccID);
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName", khachHang.AccID);
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HoTen,DiaChi,DienThoai,AccID,Avatar,NgaySinh,GioiTinh,Email")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccID = new SelectList(db.TaiKhoans, "Id", "UserName", khachHang.AccID);
            return View(khachHang);
        }

        // GET: Admin/KhachHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
            db.SaveChanges();
            return RedirectToAction("Index");
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
