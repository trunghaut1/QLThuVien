using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTrangThaiDauSach
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách trạng thái đầu sách
        public List<TrangThaiDauSach> GetAll()
        {
            try
            {
                return _db.TrangThaiDauSach.ToList();
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
                TrangThaiDauSach record = _db.TrangThaiDauSach.SingleOrDefault(v => v.MaTrangThai == id);
                return record;
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
                _db.TrangThaiDauSach.Add(value);
                _db.SaveChanges();
                return true;
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
                TrangThaiDauSach record = _db.TrangThaiDauSach.SingleOrDefault(v => v.MaTrangThai == value.MaTrangThai);
                record.TenTrangThai = value.TenTrangThai;
                _db.SaveChanges();
                return true;
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
                TrangThaiDauSach record = _db.TrangThaiDauSach.SingleOrDefault(v => v.MaTrangThai == id);
                _db.TrangThaiDauSach.Remove(record);
                _db.SaveChanges();
                return true;
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
                var record = from r in _db.TrangThaiDauSach select r;
                if (ten != null) record = record.Where(r => r.TenTrangThai.Contains(ten));
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
