using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizTacGia
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách tác giả
        public List<TacGia> GetAll()
        {
            try
            {
                return _db.TacGia.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy tác giả
        public TacGia GetByID(int id)
        {
            try
            {
                TacGia record = _db.TacGia.SingleOrDefault(v => v.MaTacGia == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm tác giả
        public bool Add(TacGia value)
        {
            try
            {
                _db.TacGia.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật tác giả
        public bool Update(TacGia value)
        {
            try
            {
                TacGia record = _db.TacGia.SingleOrDefault(v => v.MaTacGia == value.MaTacGia);
                record.TenTacGia = value.TenTacGia;
                record.NoiCongTac = value.NoiCongTac;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa tác giả
        public bool Delete(int id)
        {
            try
            {
                TacGia record = _db.TacGia.SingleOrDefault(v => v.MaTacGia == id);
                _db.TacGia.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm tác giả
        public List<TacGia> Search(string ten, string congtac)
        {
            try
            {
                var record = from r in _db.TacGia select r;
                if (ten != null) record = record.Where(r => r.TenTacGia.Contains(ten));
                if (congtac != null) record = record.Where(r => r.NoiCongTac.Contains(congtac));
                return record.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Kiểm tra có tồn tại đầu sách mang tác giả này hay không
        public bool CheckFK(int id)
        {
            try
            {
                int record = (from r in _db.TacGia_Sach where r.MaTacGia == id select r).Count();
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
