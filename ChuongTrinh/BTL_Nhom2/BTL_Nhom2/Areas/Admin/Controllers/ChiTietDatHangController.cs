using BTL_Nhom2.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class ChiTietDatHangController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/ChiTietDatHang
        public ActionResult Index()
        {
            var chiTietDatHangs = db.ChiTietDatHangs.Include(c => c.DonDatHang).Include(c => c.KichCo).Include(c => c.SanPham);
            return View(chiTietDatHangs.ToList());
        }

        // GET: Admin/ChiTietDatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            if (chiTietDatHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDatHang);
        }

        // GET: Admin/ChiTietDatHang/Create
        public ActionResult Create()
        {
            ViewBag.DonDatHangId = new SelectList(db.DonDatHangs, "Id", "Id");
            ViewBag.SizeId = new SelectList(db.KichCoes, "Id", "TenKichCo");
            ViewBag.SanPhamId = new SelectList(db.SanPhams, "Id", "TenSP");
            return View();
        }

        // POST: Admin/ChiTietDatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DonDatHangId,SanPhamId,SizeId,SoLuong")] ChiTietDatHang chiTietDatHang)
        {
            
            if (ModelState.IsValid)
            {
                db.ChiTietDatHangs.Add(chiTietDatHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DonDatHangId = new SelectList(db.DonDatHangs, "Id", "Id", chiTietDatHang.DonDatHangId);
            ViewBag.SizeId = new SelectList(db.KichCoes, "Id", "TenKichCo", chiTietDatHang.SizeId);
            ViewBag.SanPhamId = new SelectList(db.SanPhams, "Id", "TenSP", chiTietDatHang.SanPhamId);
            return View(chiTietDatHang);
        }

        // GET: Admin/ChiTietDatHang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            if (chiTietDatHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.DonDatHangId = new SelectList(db.DonDatHangs, "Id", "Id", chiTietDatHang.DonDatHangId);
            ViewBag.SizeId = new SelectList(db.KichCoes, "Id", "TenKichCo", chiTietDatHang.SizeId);
            ViewBag.SanPhamId = new SelectList(db.SanPhams, "Id", "TenSP", chiTietDatHang.SanPhamId);
            return View(chiTietDatHang);
        }

        // POST: Admin/ChiTietDatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DonDatHangId,SanPhamId,SizeId,SoLuong")] ChiTietDatHang chiTietDatHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDatHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DonDatHangId = new SelectList(db.DonDatHangs, "Id", "Id", chiTietDatHang.DonDatHangId);
            ViewBag.SizeId = new SelectList(db.KichCoes, "Id", "TenKichCo", chiTietDatHang.SizeId);
            ViewBag.SanPhamId = new SelectList(db.SanPhams, "Id", "TenSP", chiTietDatHang.SanPhamId);
            return View(chiTietDatHang);
        }

        // GET: Admin/ChiTietDatHang/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            if (chiTietDatHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDatHang);
        }

        // POST: Admin/ChiTietDatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDatHang chiTietDatHang = db.ChiTietDatHangs.Find(id);
            db.ChiTietDatHangs.Remove(chiTietDatHang);
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
