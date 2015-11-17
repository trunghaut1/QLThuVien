using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTinhTrangCuonSach
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách tình trạng cuốn sách
        public List<TinhTrangCuonSach> GetAll()
        {
            try
            {
                return _db.TinhTrangCuonSach.ToList();
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
                TinhTrangCuonSach record = _db.TinhTrangCuonSach.SingleOrDefault(v => v.MaTinhTrang == id);
                return record;
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
                _db.TinhTrangCuonSach.Add(value);
                _db.SaveChanges();
                return true;
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
                TinhTrangCuonSach record = _db.TinhTrangCuonSach.SingleOrDefault(v => v.MaTinhTrang == value.MaTinhTrang);
                record.TenTinhTrang = value.TenTinhTrang;
                _db.SaveChanges();
                return true;
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
                TinhTrangCuonSach record = _db.TinhTrangCuonSach.SingleOrDefault(v => v.MaTinhTrang == id);
                _db.TinhTrangCuonSach.Remove(record);
                _db.SaveChanges();
                return true;
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
                var record = from r in _db.TinhTrangCuonSach select r;
                if (ten != null) record = record.Where(r => r.TenTinhTrang.Contains(ten));
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
