using BTL_Nhom2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class DoiTuongController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/DoiTuong
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var doiTuong = db.DoiTuongs.Select(c => c);
            if (!String.IsNullOrEmpty(searchString))
            {
                doiTuong = doiTuong.Where(c => c.TenDoiTuong.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    doiTuong = doiTuong.OrderByDescending(c => c.TenDoiTuong);
                    break;
                default:
                    doiTuong = doiTuong.OrderBy(c => c.TenDoiTuong);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(doiTuong.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Admin/DoiTuong/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiTuong doiTuong = db.DoiTuongs.Find(id);
            if (doiTuong == null)
            {
                return HttpNotFound();
            }
            return View(doiTuong);
        }

        // GET: Admin/DoiTuong/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/DoiTuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenDoiTuong")] DoiTuong doiTuong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DoiTuongs.Add(doiTuong);
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(doiTuong);
            }

            
        }

        // GET: Admin/DoiTuong/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiTuong doiTuong = db.DoiTuongs.Find(id);
            if (doiTuong == null)
            {
                return HttpNotFound();
            }
            return View(doiTuong);
        }

        // POST: Admin/DoiTuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenDoiTuong")] DoiTuong doiTuong)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(doiTuong).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(doiTuong);
            }
            
        }

        // GET: Admin/DoiTuong/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoiTuong doiTuong = db.DoiTuongs.Find(id);
            if (doiTuong == null)
            {
                return HttpNotFound();
            }
            return View(doiTuong);
        }

        // POST: Admin/DoiTuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoiTuong doiTuong = db.DoiTuongs.Find(id);
            try
            {
                db.DoiTuongs.Remove(doiTuong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xóa" + ex.Message;
                return View("Delete", doiTuong);
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
