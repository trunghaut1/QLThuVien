using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCTPhieuTra
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách chi tiết phiếu trả theo mã phiếu trả
        // >> Dùng hàm Search
        //
        public List<CTPhieuTra> Search(int? maphieutra, int? macuonsach)
        {
            try
            {
                var record = from r in _db.CTPhieuTra select r;
                if (maphieutra != null) record = record.Where(r => r.MaPhieuTra == maphieutra);
                if (macuonsach != null) record = record.Where(r => r.MaCuonSach == macuonsach);
                return record.ToList();
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
                _db.CTPhieuTra.Add(value);
                _db.SaveChanges();
                return true;
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
                foreach (var record in value)
                {
                    _db.CTPhieuTra.Add(record);
                }
                _db.SaveChanges();
                return true;
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
                CTPhieuTra record = _db.CTPhieuTra.SingleOrDefault(v => v.ID == value.ID);
                record.MaCuonSach = value.MaCuonSach;
                _db.SaveChanges();
                return true;
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
                CTPhieuTra record = _db.CTPhieuTra.SingleOrDefault(v => v.ID == id);
                _db.CTPhieuTra.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
