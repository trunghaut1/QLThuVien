using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCTPhieuMuon
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy chi tiết phiếu mượn theo mã phiếu mượn
        // >> Dùng hàm Search
        public List<CTPhieuMuon> Search(int? maphieumuon, int? macuonsach)
        {
            try
            {
                var record = from r in _db.CTPhieuMuon select r;
                if (maphieumuon != null) record = record.Where(r => r.MaPhieuMuon == maphieumuon);
                if (macuonsach != null) record = record.Where(r => r.MaCuonSach == macuonsach);
                return record.ToList();
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
                _db.CTPhieuMuon.Add(value);
                _db.SaveChanges();
                return true;
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
                foreach (var record in value)
                {
                    _db.CTPhieuMuon.Add(record);
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
        // Cập nhật chi tiết phiếu mượn
        public bool Update(CTPhieuMuon value)
        {
            try
            {
                CTPhieuMuon record = _db.CTPhieuMuon.SingleOrDefault(v => v.ID == value.ID);
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
        // Xóa chi tiết phiếu mượn
        public bool Delete(int id)
        {
            try
            {
                CTPhieuMuon record = _db.CTPhieuMuon.SingleOrDefault(v => v.ID == id);
                _db.CTPhieuMuon.Remove(record);
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
