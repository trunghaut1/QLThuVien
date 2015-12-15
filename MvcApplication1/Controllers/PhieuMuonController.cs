using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Biz;
using Core.Dal;
using System.Data;


namespace MvcApplication1.Controllers
{
    public class PhieuMuonController : Controller
    {
        //
        // GET: /PhieuMuon/
        QLThuVienEntities _db = new QLThuVienEntities();
        BizDocGia db = new BizDocGia();
        BizCuonSach dbsach = new BizCuonSach();
        BizPhieuMuon dbpm = new BizPhieuMuon();
        BizCTPhieuMuon ctpm = new BizCTPhieuMuon();
        int chon;
        int maphieumuon;
        public ActionResult Index(String id="1")
        {
             deleteallrow();
            ViewBag.theloai ="0";
            int sotrang = int.Parse(id);
            ViewBag.tongso = db.GetAll().Count.ToString().Trim();
            
            ViewData["dsdg"] = db.GetByPageLinq(5, sotrang);
            return View("LapPhieuMuon");
        }
        public ActionResult lapphieumuon(String id,String soluongchon="1",String _sotrang="1")
        {

           
            int sotrang=int.Parse(_sotrang
                );
            int _id = int.Parse(id);
            var query = from c in _db.DocGia                      
                        where c.MaDocGia.Equals(_id)
                        select c;
            var query2 = from d in _db.Temp
                         where d.MaDocGia.Equals(_id)
                         select d;
            ViewData["dschon"] = query2.ToList();
                foreach(DocGia dg in query)
                {
                    if(dg.VienChuc==true)
                    {
                        ViewBag.vienchuc = "1";
                    }
                    else{
                        ViewBag.vienchuc="0";
                    }
                }
           if(kiemtra(int.Parse(id),ViewBag.vienchuc))
           {
               soluongchon = "1";
           }
           else
           {
               soluongchon = "0";
           }
            System.Data.DataTable dt = dbsach.GetByPageLinq(5, sotrang);    
            List<DataRow
            > list = new List<DataRow
            >();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr);
            }
            ViewData.Model = list;
            ViewBag.tongso = dbsach.GetAll().Count.ToString().Trim();
            ViewBag.madocgia = _id.ToString();
            ViewBag.theloai = "1";
            ViewBag.chon = soluongchon;
           
            return View("phieumuon");
        }
        public ActionResult Search(FormCollection fc,String id="1")
        {
            try
            {
                ViewBag.theloai ="1";
                ViewBag.keyword = fc["search"];
                int ma=int.Parse(fc["search"].ToString());
               ViewData["dsdg"]=db.GetListByID(ma);
               ViewBag.tongso = "0";
            }
            catch(Exception ex)
            {
                ViewBag.theloai ="1";
                ViewBag.keyword = fc["search"];
                ViewData["dsdg"] = db._GetByPageLinq(fc["search"], 5, int.Parse(id));
               
                ViewBag.tongso = db.Search(fc["search"], "", "", "", null, null, null, null).Count.ToString().Trim();
            }
              return View("LapPhieuMuon");
                  
        }
       public ActionResult _Search(String name="a",String id="1")
        {
            try
            {
                ViewBag.theloai ="1";
                ViewBag.keyword = name;
                int ma=int.Parse(name.ToString());
               ViewData["dsdg"]=db.GetListByID(ma);
               ViewBag.tongso = "0";
            }
            catch(Exception ex)
            {
                ViewBag.theloai ="1";
                ViewBag.keyword = name;
                ViewData["dsdg"] = db._GetByPageLinq(name, 5, int.Parse(id));
               
                ViewBag.tongso = db.Search(name, "", "", "", null, null, null, null).Count.ToString().Trim();
            }
              return View("LapPhieuMuon");
                  
        }
       public ActionResult Search_Sach(FormCollection fc,String madocgia,String vienchuc,String chon, String id = "1")
       {
           try
           {
               int ma = int.Parse(fc["search"]);
               ViewBag.chon = chon;
               ViewBag.theloai = "0";
               ViewBag.tongso = "0";
               ViewBag.vienchuc = vienchuc;
               ViewBag.madocgia = madocgia;
               System.Data.DataTable dt = dbsach.searchbyid(ma);
               List<DataRow
               > list = new List<DataRow
               >();
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(dr);
               }
               ViewData.Model = list;
           }
            catch(Exception ex)
           {
               ViewBag.theloai = "0";
               ViewBag.chon = chon;
               ViewBag.tongso = dbsach.demsach_search(fc["search"]).ToString();
               ViewBag.keyword = fc["search"];
               ViewBag.vienchuc = vienchuc;
               ViewBag.madocgia = madocgia;
               System.Data.DataTable dt = dbsach.Search_GetByPageLinq(fc["search"], 5, int.Parse(id));
               List<DataRow
               > list = new List<DataRow
               >();
               foreach (DataRow dr in dt.Rows)
               {
                   list.Add(dr);
               }
               ViewData.Model = list;
           }
           return View("phieumuon");
       }
       public ActionResult _Search_sachbyname(String name, String madocgia,String chon, String vienchuc,String id = "1")
        {
            try
            {
                ViewBag.chon = chon;
                ViewBag.theloai = "0";
                ViewBag.keyword = name;
                ViewBag.madocgia = madocgia;
                ViewBag.vienchuc = vienchuc;
                ViewBag.tongso = dbsach.demsach_search(name).ToString();
                System.Data.DataTable dt = dbsach.Search_GetByPageLinq(name, 5, int.Parse(id));
                List<DataRow
                > list = new List<DataRow
                >();
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
                ViewData.Model = list;
                
            }
            catch(Exception ex)
            {
                
            }
            return View("phieumuon");
        }
        public bool kiemtra(int madocgia,String vienchuc)
       {          
           var query1 = from c in _db.Temp
                        where c.MaDocGia.Equals(madocgia)
                        select c;
           var query2 = from a in _db.PhieuMuon 
                        join b in _db.CTPhieuMuon on a.MaPhieuMuon equals b.MaPhieuMuon
                        where b.DaTra==false
                        select a;
           int count = query2.Where(v => v.MaDocGia == madocgia).ToList().Count;              
           int soluongchon = query1.ToList().Count;
           int sl_chophep = soluongchon + count;
           if(vienchuc=="0")
           {
               if (sl_chophep >= 4) return false;
           }
           if (vienchuc == "1")
           {
               if (sl_chophep >= 5) return false;
           }
           return true;
       }
       
      public ActionResult xulychonsach(String madocgia,String masach,String vienchuc)
       {
        
           try
           {                       
               Temp temp = new Temp();
               temp.MaDocGia = int.Parse(madocgia);
               temp.MaCuonSach = int.Parse(masach);
               _db.Temp.Add(temp);
               _db.SaveChanges();
              
           }
              catch(Exception ex)
           {

           }
           return RedirectToAction("lapphieumuon", "PhieuMuon", new { id = madocgia, _sotrang = "1" });
       }     
        public ActionResult nexttep(String madocgia)
      {
          DocGia dg = db.GetByID(int.Parse(madocgia));
          int ma = int.Parse(madocgia);
          ViewBag.madocgia = dg.MaDocGia;
          ViewBag.tendocgia = dg.TenDocGia;
          var query = from c in _db.Temp   
                     
                      select c;
          List<DataRow
          > list = new List<DataRow
          >();
          var temp = query.Where(v => v.MaDocGia == ma).ToList();
            foreach(var item in temp)
            {
                System.Data.DataTable dt = dbsach.searchbyid(item.MaCuonSach);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
            }
        
          ViewData.Model = list;
          return View("FormPhieuMuon");
      }
        public ActionResult xoaitem(String _madocgia,String id)
        {
            int _id = int.Parse(id);
            var query = from c in _db.Temp
                        where c.MaCuonSach.Equals(_id)
                        select c;
            foreach(Temp temp in query)
            {
                _db.Temp.Remove(temp);
            }
            _db.SaveChanges();
            return RedirectToAction("nexttep", "PhieuMuon", new { madocgia = _madocgia });
        }
        public ActionResult submit(String _madocgia,FormCollection fc)
        {
            PhieuMuon phieumuon = new PhieuMuon();
          
            phieumuon.MaDocGia = int.Parse(_madocgia);
            phieumuon.NgayMuon = DateTime.Parse(fc["ngay"]);
            dbpm.Add(phieumuon);
            int madocgia=int.Parse(_madocgia);
            DateTime ngaymuon=DateTime.Parse(fc["ngay"]);
            var list = dbpm.Search(madocgia, ngaymuon);
            foreach(var item in list)
            {
                maphieumuon = item.MaPhieuMuon;
            }
            var query=from c in _db.Temp
                      select c;
            var temp = query.Where(v => v.MaDocGia == madocgia).ToList();
            foreach(var item in temp)
            {
                CTPhieuMuon ct = new CTPhieuMuon();
                ct.MaPhieuMuon = maphieumuon;
                ct.MaCuonSach = item.MaCuonSach;
                ct.DaTra = false;
                _db.CTPhieuMuon.Add(ct);
                CuonSach cs=new CuonSach();
                cs = dbsach.GetByID(item.MaCuonSach);
                cs.MaTinhTrang = 2;    
                dbsach.Update(cs);
            }
            deleteallrow();
            return RedirectToAction("Index");
        }
        public void deleteallrow()
        {
            var row = from c in _db.Temp
                      select c;
            _db.Temp.RemoveRange(row);
            _db.SaveChanges();
        }
        public ActionResult quanlyphieumuon(String id="1")
        {
            deleteallrow();
            ViewBag.loai = "1";
            int sotrang = int.Parse(id);
            ViewBag.tongso = dbpm.GetAll().Count.ToString().Trim();
            ViewData["dspm"] = dbpm.getall_pagelist(5, sotrang);
            return View("QuanLyPhieuMuon");        
        }
        public ActionResult PM_xemchitiet(String id)
        {
            int _id = int.Parse(id);
           var temp= ctpm._Search_id(_id).ToList();
            List<DataRow
         > list = new List<DataRow
         >();
            foreach(var item in temp)
            {
                System.Data.DataTable dt= dbsach.searchbyid(item.MaCuonSach);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(dr);
                }
            }
            PhieuMuon pm = dbpm.GetByID(_id);
            ViewBag.madocgia = pm.MaDocGia;
            ViewBag.tendocgia = pm.DocGia.TenDocGia;
            ViewBag.ngaymuon = pm.NgayMuon;
            ViewData.Model = list;
            ViewBag.mapm = id;
            return View("chitietphieumuon");
        }
        public ActionResult Search_PM(FormCollection fc,String id="1")
        {
            try
            {
                int ma = int.Parse(fc["search"].ToString());
                ViewData["dspm"] = dbpm.GetListByID(ma).ToList();
                ViewBag.loai = "0";
                ViewBag.tongso = "0";
            }            
               catch(Exception ex)
            {
                String keyword = fc["search"];
                int sotrang = int.Parse(id);
                ViewData["dspm"] = dbpm.getlistbyname(keyword,5,sotrang);
                   ViewBag.loai = "2";
                   ViewBag.tongso = dbpm._getlistbyname(keyword).Count.ToString().Trim() ;
                   ViewBag.keyword = fc["search"]; 
            }
            return View("QuanLyPhieuMuon");
        }
       public ActionResult Search_PMbyname(String name,String id="1")
        {
            String keyword = name;
            int sotrang = int.Parse(id);
            ViewData["dspm"] = dbpm.getlistbyname(keyword, 5, sotrang);
            ViewBag.loai = "2";
            ViewBag.tongso = dbpm._getlistbyname(keyword).Count.ToString().Trim();
            ViewBag.keyword = name;
            return View("QuanLyPhieuMuon");

        }
    }
}
