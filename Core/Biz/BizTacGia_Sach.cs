using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTacGia_Sach
    {
        // Lấy danh sách tacgia_sach
        public List<TacGia_Sach> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.TacGia_Sach.ToList();
                }
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
                using (var db = new QLThuVienEntities())
                {
                    db.TacGia_Sach.Add(value);
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
        // Cập nhật tacgia_sach
        public bool Update(TacGia_Sach value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TacGia_Sach record = db.TacGia_Sach.SingleOrDefault(v => v.ID == value.ID);
                    record.MaDauSach = value.MaDauSach;
                    record.MaTacGia = value.MaTacGia;
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
        // Xóa tacgia_sach
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TacGia_Sach record = db.TacGia_Sach.SingleOrDefault(v => v.ID == id);
                    db.TacGia_Sach.Remove(record);
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
        // Tìm tacgia_sach
        public List<TacGia_Sach> Search(int? dausach, int? tacgia)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.TacGia_Sach select r;
                    if (dausach != null) record = record.Where(r => r.MaDauSach == dausach);
                    if (tacgia != null) record = record.Where(r => r.MaTacGia == tacgia);
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
