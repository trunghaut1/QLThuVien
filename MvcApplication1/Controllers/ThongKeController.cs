using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Dal;
using Core.Biz;

namespace MvcApplication1.Controllers
{
    public class ThongKeController : Controller
    {
        //
        // GET: /ThongKe/
        QLThuVienEntities _db = new QLThuVienEntities();
        BizDocGia db = new BizDocGia();
        BizCuonSach dbsach = new BizCuonSach();
        BizPhieuMuon dbpm = new BizPhieuMuon();
        BizCTPhieuMuon ctpm = new BizCTPhieuMuon();
        BizCTPhieuTra ctpt = new BizCTPhieuTra();
        BizPhieuTra pt = new BizPhieuTra();
        public ActionResult Index(String id="1")
        {
            ViewBag.loai = "1";
            int sotrang = int.Parse(id);
            ViewBag.tongso = dbpm.GetAll().Count.ToString().Trim();

            ViewData["dspm"] = dbpm.getall_pagelist(5, sotrang).ToList();
            return View("index");
          
        }
        public ActionResult thongkephieumuon(String id="1")
        {
            ViewBag.loai = "1";
            int sotrang = int.Parse(id);
            ViewBag.tongso = dbpm.GetAll().Count.ToString().Trim();

            ViewData["dspm"] = dbpm.getall_pagelist(5, sotrang).ToList();
            return View("index");
          
        }
        public ActionResult Search_PM(FormCollection fc, String id = "1")
        {
            try
            {
                int ma = int.Parse(fc["search"].ToString());
                ViewData["dspm"] = dbpm.GetListByIDDocGia(ma).ToList();
                ViewBag.loai = "0";
                ViewBag.tongso = dbpm.GetListByIDDocGia(ma).Count.ToString().Trim();
            }
            catch (Exception ex)
            {
                String keyword = fc["search"];
                int sotrang = int.Parse(id);
                ViewData["dspm"] = dbpm.getlistbyname(keyword, 5, sotrang);
                ViewBag.loai = "2";
                ViewBag.tongso = dbpm._getlistbyname(keyword).Count.ToString().Trim();
                ViewBag.keyword = fc["search"];
            }
            return View("index");
        }
        public ActionResult Search_PMbyname(String name, String id = "1")
        {
            String keyword = name;
            int sotrang = int.Parse(id);
            ViewData["dspm"] = dbpm.getlistbyname(keyword, 5, sotrang);
            ViewBag.loai = "2";
            ViewBag.tongso = dbpm._getlistbyname(keyword).Count.ToString().Trim();
            ViewBag.keyword = name;
            return View("index");

        }
    }
}
