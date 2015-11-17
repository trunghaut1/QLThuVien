using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCuonSach
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách cuốn sách
        public List<CuonSach> GetAll()
        {
            try
            {
                return _db.CuonSach.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy cuốn sách
        public CuonSach GetByID(int id)
        {
            try
            {
                CuonSach record = _db.CuonSach.SingleOrDefault(v => v.MaCuonSach == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm cuốn sách
        public bool Add(CuonSach value)
        {
            try
            {
                _db.CuonSach.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật cuốn sách
        public bool Update(CuonSach value)
        {
            try
            {
                CuonSach record = _db.CuonSach.SingleOrDefault(v => v.MaCuonSach == value.MaCuonSach);
                record.MaDauSach = value.MaDauSach;
                record.MaTinhTrang = value.MaTinhTrang;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa cuốn sách
        public bool Delete(int id)
        {
            try
            {
                CuonSach record = _db.CuonSach.SingleOrDefault(v => v.MaCuonSach == id);
                _db.CuonSach.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm cuốn sách
        public List<CuonSach> Search(int? madausach, int? tinhtrang)
        {
            try
            {
                var record = from r in _db.CuonSach select r;
                if (madausach != null) record = record.Where(r => r.MaDauSach == madausach);
                if (tinhtrang != null) record = record.Where(r => r.MaTinhTrang == tinhtrang);
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
