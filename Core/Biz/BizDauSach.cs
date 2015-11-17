using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizDauSach
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách đầu sách
        public List<DauSach> GetAll()
        {
            try
            {
                return _db.DauSach.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy đầu sách
        public DauSach GetByID(int id)
        {
            try
            {
                DauSach record = _db.DauSach.SingleOrDefault(v => v.MaDauSach == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm đầu sách
        public bool Add(DauSach value)
        {
            try
            {
                _db.DauSach.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật đầu sách
        public bool Update(DauSach value)
        {
            try
            {
                DauSach record = _db.DauSach.SingleOrDefault(v => v.MaDauSach == value.MaDauSach);
                record.TenDauSach = value.TenDauSach;
                record.MaLoai = value.MaLoai;
                record.MaNXB = value.MaNXB;
                record.MaTrangThai = value.MaTrangThai;
                record.TomTat = value.TomTat;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa đầu sách
        public bool Delete(int id)
        {
            try
            {
                DauSach record = _db.DauSach.SingleOrDefault(v => v.MaDauSach == id);
                _db.DauSach.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm đầu sách
        public List<DauSach> Search(string ten, int? loai, int? nxb, int? trangthai)
        {
            try
            {
                var record = from r in _db.DauSach select r;
                if (ten != null) record = record.Where(r => r.TenDauSach.Contains(ten));
                if (loai != null) record = record.Where(r => r.MaLoai == loai);
                if (nxb != null) record = record.Where(r => r.MaNXB == nxb);
                if (trangthai != null) record = record.Where(r => r.MaTrangThai == trangthai);
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
