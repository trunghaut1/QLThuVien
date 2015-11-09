using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCuonSach
    {
        // Lấy danh sách cuốn sách
        public List<CuonSach> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.CuonSach.ToList();
                }
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
                using (var db = new QLThuVienEntities())
                {
                    CuonSach record = db.CuonSach.SingleOrDefault(v => v.MaCuonSach == id);
                    return record;
                }
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
                using (var db = new QLThuVienEntities())
                {
                    db.CuonSach.Add(value);
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
        // Cập nhật cuốn sách
        public bool Update(CuonSach value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    CuonSach record = db.CuonSach.SingleOrDefault(v => v.MaCuonSach == value.MaCuonSach);
                    record.MaDauSach = value.MaDauSach;
                    record.MaTinhTrang = value.MaTinhTrang;
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
        // Xóa cuốn sách
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    CuonSach record = db.CuonSach.SingleOrDefault(v => v.MaCuonSach == id);
                    db.CuonSach.Remove(record);
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
        // Tìm cuốn sách
        public List<CuonSach> Search(int? madausach, int? tinhtrang)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.CuonSach select r;
                    if (madausach != null) record = record.Where(r => r.MaDauSach == madausach);
                    if (tinhtrang != null) record = record.Where(r => r.MaTinhTrang == tinhtrang);
                    return record.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
