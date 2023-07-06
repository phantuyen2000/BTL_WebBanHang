using BTL_Nhom2.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Razor;
using PagedList;
namespace BTL_Nhom2.Controllers
{
    public class HomeController : Controller
    {
        ShopQuanAo db = new ShopQuanAo();
        int pageSize = 8;
        public ActionResult Index()
        {
            ViewBag.BeTrai = db.SanPhams.Where(x => x.DoiTuongId == 1 && x.SoLuong > 0).OrderBy(x => x.Id).Take(8).ToList();
            ViewBag.BeGai = db.SanPhams.Where(x => x.DoiTuongId == 2 &&x.SoLuong>0).OrderBy(x => x.Id).Take(8).ToList();
            ViewBag.TreSoSinh = db.SanPhams.Where(x => x.DoiTuongId == 3 && x.SoLuong > 0).OrderBy(x => x.Id).Take(8).ToList();
            ViewBag.PhuKien = db.SanPhams.Where(x => x.TheLoaiId == 8 && x.SoLuong > 0).OrderBy(x => x.Id).Take(4).ToList();
            ViewBag.SanPhamMoi = db.SanPhams.OrderByDescending(x => x.NgayCapNhat).OrderBy(x => x.Id).Take(8).ToList();
            
            return View();
        }

        public ActionResult BeTrai(int? page)
        {
            var beTrai = db.SanPhams.Where(x => x.DoiTuongId == 1 && x.SoLuong > 0).OrderBy(x => x.Id);
            int pageNumber = (page ?? 1);
            return View(beTrai.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult BeGai(int? page)
        {
            var beGai = db.SanPhams.Where(x => x.DoiTuongId == 2 && x.SoLuong > 0).OrderBy(x => x.Id);
            int pageNumber = (page ?? 1);
            return View(beGai.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult TreSoSinh(int? page)
        {
            var treSoSinh = db.SanPhams.Where(x => x.DoiTuongId == 3 && x.SoLuong > 0).OrderBy(x => x.Id);
            int pageNumber = (page ?? 1);
            return View(treSoSinh.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult PhuKien(int? page)
        {
            var phuKien = db.SanPhams.Where(x => x.TheLoaiId == 8 && x.SoLuong > 0).OrderBy(x => x.Id);
            int myPage = 4;
            int pageNumber = (page ?? 1);
            return View(phuKien.ToPagedList(pageNumber, myPage));
        }
        public ActionResult Details(int id)
        {

            SanPham sp = db.SanPhams.SingleOrDefault(x => x.Id == id);
            //Nếu không tìm thấy
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Search(string search, int? page)
        {
            int pageNumber = (page ?? 1);
            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                var sp = db.SanPhams.Where(s => s.TenSP.Contains(search)).OrderBy(s=>s.Id);
                if (sp.ToList().Count == 0)
                {
                    ViewBag.Result = "Không tìm thấy sản phẩm nào.";
                }
                return View(sp.ToPagedList(pageNumber,pageSize));
            }
            return View("Index");
        }
    }
}