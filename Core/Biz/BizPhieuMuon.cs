using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizPhieuMuon
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách phiếu mượn
        public List<PhieuMuon> GetAll()
        {
            try
            {
                return _db.PhieuMuon.ToList();
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
                PhieuMuon record = _db.PhieuMuon.SingleOrDefault(v => v.MaPhieuMuon == id);
                return record;
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
                _db.PhieuMuon.Add(value);
                _db.SaveChanges();
                return true;
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
                PhieuMuon record = _db.PhieuMuon.SingleOrDefault(v => v.MaPhieuMuon == value.MaPhieuMuon);
                record.MaDocGia = value.MaDocGia;
                record.NgayMuon = value.NgayMuon;
                _db.SaveChanges();
                return true;
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
                PhieuMuon record = _db.PhieuMuon.SingleOrDefault(v => v.MaPhieuMuon == id);
                _db.PhieuMuon.Remove(record);
                _db.SaveChanges();
                return true;
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
                var record = from r in _db.PhieuMuon select r;
                if (docgia != null) record = record.Where(r => r.MaDocGia == docgia);
                // Điều kiện ngày mượn >= ngaymuon
                if (ngaymuon != null) record = record.Where(r => r.NgayMuon >= ngaymuon);
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
