using BTL_Nhom2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class KichCoController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/KichCo
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

            var kichCo = db.KichCoes.Select(c => c);
            if (!String.IsNullOrEmpty(searchString))
            {
                kichCo = kichCo.Where(c => c.TenKichCo.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    kichCo = kichCo.OrderByDescending(c => c.TenKichCo);
                    break;
                default:
                    kichCo = kichCo.OrderBy(c => c.TenKichCo);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(kichCo.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/KichCo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KichCo kichCo = db.KichCoes.Find(id);
            if (kichCo == null)
            {
                return HttpNotFound();
            }
            return View(kichCo);
        }

        // GET: Admin/KichCo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KichCo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenKichCo")] KichCo kichCo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KichCoes.Add(kichCo);
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(kichCo);
            }

            
        }

        // GET: Admin/KichCo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KichCo kichCo = db.KichCoes.Find(id);
            if (kichCo == null)
            {
                return HttpNotFound();
            }
            return View(kichCo);
        }

        // POST: Admin/KichCo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenKichCo")] KichCo kichCo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(kichCo).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(kichCo);
            }
            
        }

        // GET: Admin/KichCo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KichCo kichCo = db.KichCoes.Find(id);
            if (kichCo == null)
            {
                return HttpNotFound();
            }
            return View(kichCo);
        }

        // POST: Admin/KichCo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KichCo kichCo = db.KichCoes.Find(id);
            try
            {
                db.KichCoes.Remove(kichCo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xóa" + ex.Message;
                return View("Delete", kichCo);

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
