using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Biz;
using Core.Dal;

namespace MvcApplication1.Controllers
{
    public class DangNhapController : Controller
    {
        //
        // GET: /DangNhap/
        BizDocGia db = new BizDocGia();
        QLThuVienEntities _db = new QLThuVienEntities();
        public ActionResult Index()
        {
            Session["admin"] = "admin";
            return View("login");
        }
        public ActionResult Xulydangnhap()
        {
           
            return RedirectToAction("Index", "TrangChu");
        }
    }
}
