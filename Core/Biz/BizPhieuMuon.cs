using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizPhieuMuon
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách phiếu mượn theo mã phiếu mượn
        public List<PhieuMuon> GetListByID(int id)
        {
            try
            {
                var query = from c in _db.PhieuMuon
                            where c.MaPhieuMuon.Equals(id)
                            select c;
                return query.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        //Lấy DS theo Mã Độc giả
        public List<PhieuMuon> GetListByIDDocGia(int id)
        {
            try
            {
                var query = from c in _db.PhieuMuon
                            where c.DocGia.MaDocGia.Equals(id)
                            select c;
                return query.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        public List<PhieuMuon> getlistbyname(String name, int pageSize, int pageNum)
        {
            var query = from c in _db.PhieuMuon
                        select c;
            var temp = query.Where(v => v.DocGia.TenDocGia.Contains(name)).OrderBy(v=>v.MaPhieuMuon).ToList();
            var list = temp.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
            return list;
                   
        }
        public List<PhieuMuon> _getlistbyname(String name)
        {
            var query = from c in _db.PhieuMuon
                        select c;
            var temp = query.Where(v => v.DocGia.TenDocGia.Contains(name)).OrderBy(v => v.MaPhieuMuon).ToList();

            return temp;
        }
        public List<PhieuMuon> getall_pagelist(int pageSize, int pageNum)
        {
            var query = (from c in _db.PhieuMuon orderby c.MaPhieuMuon select c).ToList();
            var list = query.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
            return list;
        }
        public List<PhieuMuon> GetAll()
        {
            try
            {
                return _db.PhieuMuon.OrderBy(e => e.NgayMuon).ToList();
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
        //timphieumuon
        public PhieuMuon _Search(int? docgia, DateTime? ngaymuon)
        {
            try
            {
                PhieuMuon pm = new PhieuMuon();
                var query = from c in _db.PhieuMuon
                            where c.MaDocGia.Equals(docgia)
                            select c;
                var temp = query.Where(v => v.NgayMuon >= ngaymuon).ToList();
                foreach(var item in temp)
                {
                    pm.MaPhieuMuon = item.MaPhieuMuon;
                    pm.NgayMuon = item.NgayMuon;
                    pm.MaDocGia = item.MaDocGia;
                }
                return pm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
