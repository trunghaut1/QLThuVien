using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTinhTrangCuonSach
    {
        // Lấy danh sách tình trạng cuốn sách
        public List<TinhTrangCuonSach> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.TinhTrangCuonSach.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy tình trạng cuốn sách
        public TinhTrangCuonSach GetByID(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TinhTrangCuonSach record = db.TinhTrangCuonSach.SingleOrDefault(v => v.MaTinhTrang == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm tình trạng cuốn sách
        public bool Add(TinhTrangCuonSach value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    db.TinhTrangCuonSach.Add(value);
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
        // Cập nhật tình trạng cuốn sách
        public bool Update(TinhTrangCuonSach value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TinhTrangCuonSach record = db.TinhTrangCuonSach.SingleOrDefault(v => v.MaTinhTrang == value.MaTinhTrang);
                    record.TenTinhTrang = value.TenTinhTrang;
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
        // Xóa tình trạng cuốn sách
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TinhTrangCuonSach record = db.TinhTrangCuonSach.SingleOrDefault(v => v.MaTinhTrang == id);
                    db.TinhTrangCuonSach.Remove(record);
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
        // Tìm tình trạng cuốn sách
        public List<TinhTrangCuonSach> Search(string ten)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.TinhTrangCuonSach select r;
                    if (ten != null) record = record.Where(r => r.TenTinhTrang.Contains(ten));
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
