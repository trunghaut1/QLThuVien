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
        BizCuonSach dbsach = new BizCuonSach();
        BizPhieuMuon dbpm = new BizPhieuMuon();
        BizCTPhieuMuon ctpm = new BizCTPhieuMuon();
        public ActionResult Index(String id="1")
        {
            int sotrang = int.Parse(id);
            ViewBag.tongso = db.GetAll().Count.ToString().Trim();
            ViewData["dsdg"] = db.GetByPageLinq(5, sotrang);
            ViewBag.loai = "1";
            return View("index");
        }
        public ActionResult tracuu(String id)
        {
            ViewBag.loai = "0";
            int madg = int.Parse(id);
            ViewData["dsdg"] = db.GetListByID(madg);
            ViewData["dspm"] = dbpm.GetListByIDDocGia(madg);
            return View("tracuu");
        }

        public ActionResult Search_PM(FormCollection fc, String id)
        {
            try
            {
                int ma = int.Parse(fc["search"].ToString());
                ViewData["dspm"] = dbpm.GetListByID(ma).ToList();
                int madg = int.Parse(id);
                ViewData["dsdg"] = db.GetListByID(madg);
                ViewBag.loai = "0";
                ViewBag.tongso = "0";
            }
            catch (Exception ex)
            {
                
            }
            return View("tracuu");
        }
            

    }
}
