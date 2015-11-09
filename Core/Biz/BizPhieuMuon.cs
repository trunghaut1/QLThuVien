using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizPhieuMuon
    {
        // Lấy danh sách phiếu mượn
        public List<PhieuMuon> GetAll()
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    return db.PhieuMuon.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy phiếu mượn
        public PhieuMuon GetByID(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    PhieuMuon record = db.PhieuMuon.SingleOrDefault(v => v.MaPhieuMuon == id);
                    return record;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm phiếu mượn
        public bool Add(PhieuMuon value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    db.PhieuMuon.Add(value);
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
        // Cập nhật phiếu mượn
        public bool Update(PhieuMuon value)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    PhieuMuon record = db.PhieuMuon.SingleOrDefault(v => v.MaPhieuMuon == value.MaPhieuMuon);
                    record.MaDocGia = value.MaDocGia;
                    record.NgayMuon = value.NgayMuon;
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
        // Xóa phiếu mượn
        public bool Delete(int id)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    PhieuMuon record = db.PhieuMuon.SingleOrDefault(v => v.MaPhieuMuon == id);
                    db.PhieuMuon.Remove(record);
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
        // Tìm phiếu mượn
        public List<PhieuMuon> Search(int? docgia, DateTime? ngaymuon)
        {
            try
            {
                using (var db = new QLThuVienEntities())
                {
                    var record = from r in db.PhieuMuon select r;
                    if (docgia != null) record = record.Where(r => r.MaDocGia == docgia);
                    // Điều kiện ngày mượn >= ngaymuon
                    if (ngaymuon != null) record = record.Where(r => r.NgayMuon >= ngaymuon);
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
