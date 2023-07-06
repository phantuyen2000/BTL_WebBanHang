using BTL_Nhom2.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class DonDatHangController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/DonDatHang
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            if(searchString != null)
            {
                page = 1;

            }
            else
            {
                searchString = currentFilter;
            }ViewBag.CurrentFilter = searchString;

            var donDat = db.DonDatHangs.Select(d => d);
                         
            switch (sortOrder)
            {
                case "ten_desc":
                    donDat = donDat.OrderByDescending(s => s.KhachHang.HoTen);
                    break;
                default:
                    donDat = donDat.OrderBy(s => s.KhachHang.HoTen);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var donDatHangs = db.DonDatHangs.Include(d => d.KhachHang);
            return View(donDat.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DonDatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            var chiTiet = from c in db.ChiTietDatHangs
                            where c.DonDatHangId == id
                            select c;
            return View(chiTiet.ToList());
        }

        // GET: Admin/DonDatHang/Create
        public ActionResult Create()
        {
            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "Id", "HoTen");
            return View();
        }

        // POST: Admin/DonDatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KhachHangId,NgayDatHang")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.DonDatHangs.Add(donDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "Id", "HoTen", donDatHang.KhachHangId);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "Id", "HoTen", donDatHang.KhachHangId);
            return View(donDatHang);
        }

        // POST: Admin/DonDatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KhachHangId,NgayDatHang")] DonDatHang donDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KhachHangId = new SelectList(db.KhachHangs, "Id", "HoTen", donDatHang.KhachHangId);
            return View(donDatHang);
        }

        // GET: Admin/DonDatHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            return View(donDatHang);
        }

        // POST: Admin/DonDatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            db.DonDatHangs.Remove(donDatHang);
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
