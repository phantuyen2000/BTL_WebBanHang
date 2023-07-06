using BTL_Nhom2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;
namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/SanPham
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.DV = string.IsNullOrEmpty(sortOrder) ? "dv_desc" : "dv";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var sanPham = db.SanPhams.Select(c => c);
            if (!String.IsNullOrEmpty(searchString))
            {
                sanPham = sanPham.Where(c => c.TenSP.Contains(searchString)|| c.DonViTinh.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    sanPham = sanPham.OrderByDescending(c => c.TenSP);
                    break;
                case "":
                    sanPham = sanPham.OrderBy(c => c.TenSP);
                    break;
                case "dv_desc":
                    sanPham = sanPham.OrderByDescending(c => c.DonViTinh);
                    break;
                default:
                    sanPham = sanPham.OrderBy(c => c.DonViTinh);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            var sanPhams = db.SanPhams.Include(s => s.ChatLieu).Include(s => s.DoiTuong).Include(s => s.NhaSanXuat).Include(s => s.TheLoai);
            return View(sanPham.ToPagedList(pageNumber, pageSize));
            
            
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.ChatLieuId = new SelectList(db.ChatLieux, "Id", "TenChatLieu");
            ViewBag.DoiTuongId = new SelectList(db.DoiTuongs, "Id", "TenDoiTuong");
            ViewBag.NSXId = new SelectList(db.NhaSanXuats, "Id", "TenNSX");
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "Id", "TenTheLoai");
            return View();
        }

        // POST: Admin/SanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenSP,DoiTuongId,TheLoaiId,ChatLieuId,NSXId,DonViTinh,SoLuong,DonGia,MoTa,NgayCapNhat,HinhMinhHoa")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sanPham.HinhMinhHoa = "";
                    var f = Request.Files["imgFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/Content/images/" + fileName);
                        f.SaveAs(uploadPath);
                        sanPham.HinhMinhHoa = fileName;
                    }
                    db.SanPhams.Add(sanPham);
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                ViewBag.ChatLieuId = new SelectList(db.ChatLieux, "Id", "TenChatLieu", sanPham.ChatLieuId);
            ViewBag.DoiTuongId = new SelectList(db.DoiTuongs, "Id", "TenDoiTuong", sanPham.DoiTuongId);
            ViewBag.NSXId = new SelectList(db.NhaSanXuats, "Id", "TenNSX", sanPham.NSXId);
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "Id", "TenTheLoai", sanPham.TheLoaiId);
            return View(sanPham);
            }
        }

        // GET: Admin/SanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChatLieuId = new SelectList(db.ChatLieux, "Id", "TenChatLieu", sanPham.ChatLieuId);
            ViewBag.DoiTuongId = new SelectList(db.DoiTuongs, "Id", "TenDoiTuong", sanPham.DoiTuongId);
            ViewBag.NSXId = new SelectList(db.NhaSanXuats, "Id", "TenNSX", sanPham.NSXId);
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "Id", "TenTheLoai", sanPham.TheLoaiId);
            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenSP,DoiTuongId,TheLoaiId,ChatLieuId,NSXId,DonViTinh,SoLuong,DonGia,MoTa,NgayCapNhat,HinhMinhHoa")] SanPham sanPham)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sanPham.HinhMinhHoa = "";
                    var f = Request.Files["imgFile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/Content/images/" + fileName);
                        f.SaveAs(uploadPath);
                        sanPham.HinhMinhHoa = fileName;
                    }
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                ViewBag.ChatLieuId = new SelectList(db.ChatLieux, "Id", "TenChatLieu", sanPham.ChatLieuId);
            ViewBag.DoiTuongId = new SelectList(db.DoiTuongs, "Id", "TenDoiTuong", sanPham.DoiTuongId);
            ViewBag.NSXId = new SelectList(db.NhaSanXuats, "Id", "TenNSX", sanPham.NSXId);
            ViewBag.TheLoaiId = new SelectList(db.TheLoais, "Id", "TenTheLoai", sanPham.TheLoaiId);
            return View(sanPham);
            }
            
        }

        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            try
            {
                db.SanPhams.Remove(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không thể xóa" + ex.Message;
                return View("Delete", sanPham);

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
