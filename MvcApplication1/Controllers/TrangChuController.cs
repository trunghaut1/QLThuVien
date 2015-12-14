using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Biz;
using Core.Dal;
using PagedList;

namespace MvcApplication1.Controllers
{
    public class TrangChuController : Controller
    {
        //
        // GET: /MuonSach/
        QLThuVienEntities _db = new QLThuVienEntities();
        BizDocGia db = new BizDocGia();
        public ActionResult Index(String id="1")
        {
            int sotrang = int.Parse(id);
            ViewBag.tongso = db.GetAll().Count.ToString().Trim();
            ViewData["dsdg"] = db.GetByPageLinq(5, sotrang);
         
            return View("index");
        }
        public ActionResult tracuu(String id)
        {
            int madg = int.Parse(id);
            ViewData["dsdg"] = db.GetListByID(madg);
            return View("tracuu");
        }
        
        

    }
}
