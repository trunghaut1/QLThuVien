﻿using System;
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
        public ActionResult SearchDocGia(FormCollection fc,String id="1")
        {
            try
            {
                ViewBag.theloai = "2";
                ViewBag.keyword = fc["search"];
                int ma = int.Parse(fc["search"].ToString());
                ViewData["dsdg"] = db.GetListByID(ma);
                ViewBag.tongso = "0";
            }
            catch (Exception ex)
            {
                ViewBag.theloai = "2";
                ViewBag.keyword = fc["search"];
                ViewData["dsdg"] = db._GetByPageLinq(fc["search"], 5, int.Parse(id));

                ViewBag.tongso = db.Search(fc["search"], "", "", "", null, null, null, null).Count.ToString().Trim();
            }
            return View("index");
        }
        public ActionResult _Search(String name = "a", String id = "1")
        {
            try
            {
                ViewBag.theloai = "2";
                ViewBag.keyword = name;
                int ma = int.Parse(name.ToString());
                ViewData["dsdg"] = db.GetListByID(ma);
                ViewBag.tongso = "0";
            }
            catch (Exception ex)
            {
                ViewBag.theloai = "2";
                ViewBag.keyword = name;
                ViewData["dsdg"] = db._GetByPageLinq(name, 5, int.Parse(id));

                ViewBag.tongso = db.Search(name, "", "", "", null, null, null, null).Count.ToString().Trim();
            }
            return View("index");

        }
    }
}
