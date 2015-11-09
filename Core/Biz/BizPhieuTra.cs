using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizPhieuTra
    {
        // Lấy danh sách phiếu trả
        public List<PhieuTra> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.PhieuTra.ToList();
                }
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
                using (var db = new QLThuVienEntities())
                {
                    PhieuTra record = db.PhieuTra.SingleOrDefault(v => v.MaPhieuTra == id);
                    return record;
                }
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
                using (var db = new QLThuVienEntities())
                {
                    db.PhieuTra.Add(value);
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
        // Cập nhật phiếu trả
        public bool Update(PhieuTra value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    PhieuTra record = db.PhieuTra.SingleOrDefault(v => v.MaPhieuTra == value.MaPhieuTra);
                    record.MaPhieuMuon = value.MaPhieuMuon;
                    record.NgayTra = value.NgayTra;
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
        // Xóa phiếu trả
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    PhieuTra record = db.PhieuTra.SingleOrDefault(v => v.MaPhieuTra == id);
                    db.PhieuTra.Remove(record);
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
        // Tìm phiếu trả
        public List<PhieuTra> Search(int? phieumuon, DateTime? ngaytra)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.PhieuTra select r;
                    if (phieumuon != null) record = record.Where(r => r.MaPhieuMuon == phieumuon);
                    // Điều kiện ngày mượn <= ngaymuon
                    if (ngaytra != null) record = record.Where(r => r.NgayTra <= ngaytra);
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
