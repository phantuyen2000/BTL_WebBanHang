using BTL_Nhom2.Models;
using PagedList;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class NhaSanXuatController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/NhaSanXuat
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

            var nsx = db.NhaSanXuats.Select(c => c);
            if (!String.IsNullOrEmpty(searchString))
            {
                nsx = nsx.Where(c => c.TenNSX.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    nsx = nsx.OrderByDescending(c => c.TenNSX);
                    break;
                default:
                    nsx = nsx.OrderBy(c => c.TenNSX);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(nsx.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Admin/NhaSanXuat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // GET: Admin/NhaSanXuat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaSanXuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenNSX,DiaChi,DienThoai")] NhaSanXuat nhaSanXuat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.NhaSanXuats.Add(nhaSanXuat);
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(nhaSanXuat);
            }

           
        }

        // GET: Admin/NhaSanXuat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenNSX,DiaChi,DienThoai")] NhaSanXuat nhaSanXuat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(nhaSanXuat).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(nhaSanXuat);
            }
            
        }

        // GET: Admin/NhaSanXuat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            if (nhaSanXuat == null)
            {
                return HttpNotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: Admin/NhaSanXuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhaSanXuat nhaSanXuat = db.NhaSanXuats.Find(id);
            try
            {
                db.NhaSanXuats.Remove(nhaSanXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xóa" + ex.Message;
                return View("Delete", nhaSanXuat);
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
