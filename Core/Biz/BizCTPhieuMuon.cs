using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCTPhieuMuon
    {
        // Lấy chi tiết phiếu mượn theo mã phiếu mượn
        // >> Dùng hàm Search
        //
        public List<CTPhieuMuon> Search(int? maphieumuon, int? macuonsach)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.CTPhieuMuon select r;
                    if (maphieumuon != null) record = record.Where(r => r.MaPhieuMuon == maphieumuon);
                    if (macuonsach != null) record = record.Where(r => r.MaCuonSach == macuonsach);
                    return record.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm chi tiết phiếu mượn
        public bool Add(CTPhieuMuon value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    db.CTPhieuMuon.Add(value);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Thêm List chi tiết phiếu mượn
        public bool AddList(List<CTPhieuMuon> value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    foreach(var record in value)
                    {
                        db.CTPhieuMuon.Add(record);
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật chi tiết phiếu mượn
        public bool Update(CTPhieuMuon value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    CTPhieuMuon record = db.CTPhieuMuon.SingleOrDefault(v => v.ID == value.ID);
                    record.MaCuonSach = value.MaCuonSach;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa chi tiết phiếu mượn
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    CTPhieuMuon record = db.CTPhieuMuon.SingleOrDefault(v => v.ID == id);
                    db.CTPhieuMuon.Remove(record);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
