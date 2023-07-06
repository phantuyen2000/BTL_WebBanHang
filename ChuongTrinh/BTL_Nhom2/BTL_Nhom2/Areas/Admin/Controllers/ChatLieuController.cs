using BTL_Nhom2.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class ChatLieuController : Controller
    {
        private ShopQuanAo db = new ShopQuanAo();

        // GET: Admin/ChatLieu
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
            }
            ViewBag.CurrentFilter = searchString;

            var chatLieu = db.ChatLieux.Select(c => c);
            if (!String.IsNullOrEmpty(searchString))
            {
                chatLieu = chatLieu.Where(c => c.TenChatLieu.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    chatLieu = chatLieu.OrderByDescending(c => c.TenChatLieu);
                    break;
                default:
                    chatLieu = chatLieu.OrderBy(c => c.TenChatLieu);
                    break;
            }
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            
            return View(chatLieu.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/ChatLieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // GET: Admin/ChatLieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChatLieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenChatLieu")] ChatLieu chatLieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ChatLieux.Add(chatLieu);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(chatLieu);
            }
        }

        // GET: Admin/ChatLieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: Admin/ChatLieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenChatLieu")] ChatLieu chatLieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(chatLieu).State = EntityState.Modified;
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu" + ex.Message;
                return View(chatLieu);
            }
        }

        // GET: Admin/ChatLieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            if (chatLieu == null)
            {
                return HttpNotFound();
            }
            return View(chatLieu);
        }

        // POST: Admin/ChatLieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChatLieu chatLieu = db.ChatLieux.Find(id);
            try
            {
                db.ChatLieux.Remove(chatLieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không thể xóa" + ex.Message;
                return View("Delete",chatLieu);
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
