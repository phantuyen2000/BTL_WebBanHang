using BTL_Nhom2.Models;
using System.Linq;
using System.Web.Mvc;

namespace BTL_Nhom2.Areas.Admin.Controllers
{
    public class HomeAdController : Controller
    {
        // GET: Admin/HomeAd
        ShopQuanAo db = new ShopQuanAo();

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password, string avatar)
        {
            var img = db.NhanViens.SingleOrDefault(x => x.Avatar == avatar);
            var user = db.TaiKhoans.SingleOrDefault(x => x.UserName == username && x.PassWord == password && x.Allowed == 1);
            if (user != null)
            {
                Session["Id"] = user.Id;
                Session["UserName"] = user.UserName;
                Session["Avarar"] = img.Avatar;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }
    }
}