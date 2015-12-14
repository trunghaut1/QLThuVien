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
        public ActionResult Index(String id="1")
        {
            ViewBag.theloai ="0";
            int sotrang = int.Parse(id);
            ViewBag.tongso = db.GetAll().Count.ToString().Trim();
            
            ViewData["dsdg"] = db.GetByPageLinq(5, sotrang);
            return View("LapPhieuMuon");
        }
        public ActionResult lapphieumuon(String id,String _sotrang="1")
        {
            int sotrang=int.Parse(_sotrang
                );
            int _id = int.Parse(id);
            var query = from c in _db.DocGia
                        where c.MaDocGia.Equals(_id)
                        select c;
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
       public ActionResult Search_Sach(FormCollection fc,String madocgia,String vienchuc, String id = "1")
       {
           try
           {
               int ma = int.Parse(fc["search"]);
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
       public ActionResult _Search_sachbyname(String name, String madocgia, String vienchuc,String id = "1")
        {
            try
            {
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
      
       

    }
}
