using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizNXB
    {
        // Lấy danh sách NXB
        public List<NXB> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.NXB.ToList();
                }
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
                using (var db = new QLThuVienEntities())
                {
                    NXB record = db.NXB.SingleOrDefault(v => v.MaNXB == id);
                    return record;
                }
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
                using (var db = new QLThuVienEntities())
                {
                    db.NXB.Add(value);
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
        // Cập nhật NXB
        public bool Update(NXB value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    NXB record = db.NXB.SingleOrDefault(v => v.MaNXB == value.MaNXB);
                    record.TenNXB = value.TenNXB;
                    record.SDT = value.SDT;
                    record.DiaChi = value.DiaChi;
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
        // Xóa NXB
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    NXB record = db.NXB.SingleOrDefault(v => v.MaNXB == id);
                    db.NXB.Remove(record);
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
        // Tìm NXB theo nhiều điều kiện
        public List<NXB> Search(string ten, string sdt, string diachi)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.NXB select r;
                    if (ten != null) record = record.Where(r => r.TenNXB.Contains(ten));
                    if (sdt != null) record = record.Where(r => r.SDT.Contains(sdt));
                    if (diachi != null) record = record.Where(r => r.DiaChi.Contains(diachi));
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
