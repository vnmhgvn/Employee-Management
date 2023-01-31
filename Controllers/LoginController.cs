using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalExam.Models;
namespace FinalExam.Controllers
{
    public class LoginController : Controller
    {
        private ModelDB db = new ModelDB();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string username , string password)
        {
            var user = db.tblUser.Where(s => s.username == username && s.password == password).FirstOrDefault();
            if(user == null)
            {
                ViewBag.notif = "Sai tên đăng nhập hoặc mật khẩu";
                return View("DangNhap");
            }
            else
            {
                Session["username"] = username;
                return RedirectToAction("Index", "Nhanvien");
            }
        }
        public ActionResult DangXuat()
        {
            Session["username"] = null;
            return RedirectToAction("DangNhap", "Login");
        }
    }
}