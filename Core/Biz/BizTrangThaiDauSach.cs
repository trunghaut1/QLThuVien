using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTrangThaiDauSach
    {
        // Lấy danh sách trạng thái đầu sách
        public List<TrangThaiDauSach> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.TrangThaiDauSach.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy trạng thái đầu sách
        public TrangThaiDauSach GetByID(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TrangThaiDauSach record = db.TrangThaiDauSach.SingleOrDefault(v => v.MaTrangThai == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm trạng thái đầu sách
        public bool Add(TrangThaiDauSach value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    db.TrangThaiDauSach.Add(value);
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
        // Cập nhật trạng thái đầu sách
        public bool Update(TrangThaiDauSach value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TrangThaiDauSach record = db.TrangThaiDauSach.SingleOrDefault(v => v.MaTrangThai == value.MaTrangThai);
                    record.TenTrangThai = value.TenTrangThai;
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
        // Xóa trạng thái đầu sách
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    TrangThaiDauSach record = db.TrangThaiDauSach.SingleOrDefault(v => v.MaTrangThai == id);
                    db.TrangThaiDauSach.Remove(record);
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
        // Tìm trạng thái đầu sách
        public List<TrangThaiDauSach> Search(string ten)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.TrangThaiDauSach select r;
                    if (ten != null) record = record.Where(r => r.TenTrangThai.Contains(ten));
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
