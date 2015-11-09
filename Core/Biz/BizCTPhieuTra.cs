using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCTPhieuTra
    {
        // Lấy danh sách chi tiết phiếu trả theo mã phiếu trả
        // >> Dùng hàm Search
        public List<CTPhieuTra> Search(int? maphieutra, int? macuonsach)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.CTPhieuTra select r;
                    if (maphieutra != null) record = record.Where(r => r.MaPhieuTra == maphieutra);
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
        // Thêm chi tiết phiếu trả
        public bool Add(CTPhieuTra value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    db.CTPhieuTra.Add(value);
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
        // Thêm List chi tiết phiếu trả
        public bool AddList(List<CTPhieuTra> value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    foreach (var record in value)
                    {
                        db.CTPhieuTra.Add(record);
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
        // Cập nhật chi tiết phiếu trả
        public bool Update(CTPhieuTra value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    CTPhieuTra record = db.CTPhieuTra.SingleOrDefault(v => v.ID == value.ID);
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
        // Xóa chi tiết phiếu trả
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    CTPhieuTra record = db.CTPhieuTra.SingleOrDefault(v => v.ID == id);
                    db.CTPhieuTra.Remove(record);
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
