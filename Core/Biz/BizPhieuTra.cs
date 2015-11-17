using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizPhieuTra
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách phiếu trả
        public List<PhieuTra> GetAll()
        {
            try
            {
                return _db.PhieuTra.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy phiếu trả
        public PhieuTra GetByID(int id)
        {
            try
            {
                PhieuTra record = _db.PhieuTra.SingleOrDefault(v => v.MaPhieuTra == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm phiếu trả
        public bool Add(PhieuTra value)
        {
            try
            {
                _db.PhieuTra.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật phiếu trả
        public bool Update(PhieuTra value)
        {
            try
            {
                PhieuTra record = _db.PhieuTra.SingleOrDefault(v => v.MaPhieuTra == value.MaPhieuTra);
                record.MaPhieuMuon = value.MaPhieuMuon;
                record.NgayTra = value.NgayTra;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa phiếu trả
        public bool Delete(int id)
        {
            try
            {
                PhieuTra record = _db.PhieuTra.SingleOrDefault(v => v.MaPhieuTra == id);
                _db.PhieuTra.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm phiếu trả
        public List<PhieuTra> Search(int? phieumuon, DateTime? ngaytra)
        {
            try
            {
                var record = from r in _db.PhieuTra select r;
                if (phieumuon != null) record = record.Where(r => r.MaPhieuMuon == phieumuon);
                // Điều kiện ngày mượn <= ngaymuon
                if (ngaytra != null) record = record.Where(r => r.NgayTra <= ngaytra);
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
