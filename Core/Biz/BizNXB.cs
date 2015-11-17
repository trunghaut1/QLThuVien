using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizNXB
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách NXB
        public List<NXB> GetAll()
        {
            try
            {
                return _db.NXB.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy NXB
        public NXB GetByID(int id)
        {
            try
            {
                NXB record = _db.NXB.SingleOrDefault(v => v.MaNXB == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm NXB
        public bool Add(NXB value)
        {
            try
            {
                _db.NXB.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật NXB
        public bool Update(NXB value)
        {
            try
            {
                NXB record = _db.NXB.SingleOrDefault(v => v.MaNXB == value.MaNXB);
                record.TenNXB = value.TenNXB;
                record.SDT = value.SDT;
                record.DiaChi = value.DiaChi;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa NXB
        public bool Delete(int id)
        {
            try
            {
                NXB record = _db.NXB.SingleOrDefault(v => v.MaNXB == id);
                _db.NXB.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm NXB theo nhiều điều kiện
        public List<NXB> Search(string ten, string sdt, string diachi)
        {
            try
            {
                var record = from r in _db.NXB select r;
                if (ten != null) record = record.Where(r => r.TenNXB.Contains(ten));
                if (sdt != null) record = record.Where(r => r.SDT.Contains(sdt));
                if (diachi != null) record = record.Where(r => r.DiaChi.Contains(diachi));
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại đầu sách mang NXB này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record = (from r in _db.DauSach where r.MaNXB == id select r).Count();
                if (record > 0) return false;
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
