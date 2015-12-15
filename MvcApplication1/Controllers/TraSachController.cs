using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Dal;
using Core.Biz;
using System.Data;

namespace MvcApplication1.Controllers
{
    public class TraSachController : Controller
    {
        //
        // GET: /PhieuTra/
        QLThuVienEntities _db = new QLThuVienEntities();
        BizDocGia db = new BizDocGia();
        BizCuonSach dbsach = new BizCuonSach();
        BizPhieuMuon dbpm = new BizPhieuMuon();
        BizCTPhieuMuon ctpm = new BizCTPhieuMuon();
        BizCTPhieuTra ctpt = new BizCTPhieuTra();
        BizPhieuTra pt = new BizPhieuTra();
        int chon;
        int maphieutra;
        public ActionResult Index(String id="1")
        {
            ViewBag.loai = "1";
            int sotrang = int.Parse(id);
            ViewBag.tongso = dbpm.GetAll().Count.ToString().Trim();

            ViewData["dspm"] = dbpm.getall_pagelist(5, sotrang).ToList();
            return View("index");
        }
        
        public ActionResult danhsachphieumuon(String id = "1")
        {
           
            ViewBag.loai = "1";
            int sotrang = int.Parse(id);
            ViewBag.tongso = dbpm.GetAll().Count.ToString().Trim();
            ViewData["dspm"] = dbpm.getall_pagelist(5, sotrang);

            return View("index");
        }
          
        public ActionResult Search_PM(FormCollection fc, String id = "1")
        {
            try
            {
                int ma = int.Parse(fc["search"].ToString());
                ViewData["dspm"] = dbpm.GetListByID(ma).ToList();
                ViewBag.loai = "0";
                ViewBag.tongso = "0";
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
        public ActionResult lapphieutra(String id)
        {
            
            int _id = int.Parse(id);
            var temp = ctpm.Search_id(_id).ToList();
            List<DataRow
         > list = new List<DataRow
         >();
            foreach (var item in temp)
            {
                System.Data.DataTable dt = dbsach.searchbyid(item.MaCuonSach);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
            }
            PhieuMuon pm = dbpm.GetByID(_id);     
            ViewData.Model = list;
            ViewBag.mapm = id;
            
            return View("phieutra");
        }
        public ActionResult xulytrasach(FormCollection fc,String mapm)
        {
            int maphieumuon=int.Parse(mapm);
            PhieuTra phieutra = new PhieuTra();
            phieutra.MaPhieuMuon = maphieumuon;
            phieutra.NgayTra = DateTime.Parse(fc["ngay"]);
           pt.Add(phieutra);
            DateTime ngaytra = DateTime.Parse(fc["ngay"]);
            var list_ = pt.Search(maphieumuon, ngaytra).ToList();
            foreach(var item in list_)
            {
                maphieutra = item.MaPhieuTra;
            }
            int _id = int.Parse(mapm);
            var temp = ctpm.Search_id(_id).ToList();
            List<DataRow
         > list = new List<DataRow
         >();
            foreach (var item in temp)
            {
                
                System.Data.DataTable dt = dbsach.searchbyid(item.MaCuonSach);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
            }
            foreach(var item in list)
            {
                if (fc[item["MaCuonSach"].ToString()] == "check")
                {
                    int ma=int.Parse(item["MaCuonSach"].ToString());
                    CTPhieuTra ct = new CTPhieuTra();
                    ct.MaCuonSach=int.Parse(item["MaCuonSach"].ToString());
                    ct.MaPhieuTra = maphieutra;
                    ctpt.Add(ct);
                    var query=from c in _db.CTPhieuMuon
                              where c.MaCuonSach.Equals(ma)&&c.DaTra==false
                              select c;
                    foreach(CTPhieuMuon _ct in query)
                    {
                        _ct.DaTra = true;
                    }
                    _db.SaveChanges();
                    CuonSach cs = new CuonSach();
                    cs = dbsach.GetByID(int.Parse(item["MaCuonSach"].ToString()));
                    cs.MaTinhTrang = 1;                
                    dbsach.Update(cs);
                }
            }
            return RedirectToAction("Index");
        }
    }
    }
