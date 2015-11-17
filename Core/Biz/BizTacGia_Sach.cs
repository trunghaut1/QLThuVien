using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTacGia_Sach
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách tacgia_sach
        public List<TacGia_Sach> GetAll()
        {
            try
            {
                return _db.TacGia_Sach.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm tacgia_sach
        public bool Add(TacGia_Sach value)
        {
            try
            {
                _db.TacGia_Sach.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật tacgia_sach
        public bool Update(TacGia_Sach value)
        {
            try
            {
                TacGia_Sach record = _db.TacGia_Sach.SingleOrDefault(v => v.ID == value.ID);
                record.MaDauSach = value.MaDauSach;
                record.MaTacGia = value.MaTacGia;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa tacgia_sach
        public bool Delete(int id)
        {
            try
            {
                TacGia_Sach record = _db.TacGia_Sach.SingleOrDefault(v => v.ID == id);
                _db.TacGia_Sach.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm tacgia_sach
        public List<TacGia_Sach> Search(int? dausach, int? tacgia)
        {
            try
            {
                var record = from r in _db.TacGia_Sach select r;
                if (dausach != null) record = record.Where(r => r.MaDauSach == dausach);
                if (tacgia != null) record = record.Where(r => r.MaTacGia == tacgia);
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
