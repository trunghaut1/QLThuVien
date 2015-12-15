using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizCuonSach
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        Tool tool = new Tool();
        //SeachTheoTen_List
        public int demsach_search(String Name)
        {
            var temp = (from a in _db.CuonSach
                        join b in _db.DauSach on a.MaDauSach equals b.MaDauSach
                        join c in _db.LoaiSach on b.MaLoai equals c.MaLoai
                        join d in _db.TrangThaiDauSach on b.MaTrangThai equals d.MaTrangThai
                        join e in _db.NXB on b.MaNXB equals e.MaNXB
                        join g in _db.TacGia on b.MaTacGia equals g.MaTacGia
                        join h in _db.TinhTrangCuonSach on a.MaTinhTrang equals h.MaTinhTrang
                        select new { a.MaCuonSach, b.TenDauSach, c.TenLoai, e.TenNXB, g.TenTacGia, h.TenTinhTrang }).ToList();
            temp = temp.Where(v => v.TenDauSach.Contains(Name)).ToList();
            return temp.Count;
        }
        //lay ds theo trang
        public System.Data.DataTable GetByPageLinq(int pageSize, int pageNum)
        {
            var temp = (from a in _db.CuonSach
                        join b in _db.DauSach on a.MaDauSach equals b.MaDauSach
                        join c in _db.LoaiSach on b.MaLoai equals c.MaLoai
                        join d in _db.TrangThaiDauSach on b.MaTrangThai equals d.MaTrangThai
                        join e in _db.NXB on b.MaNXB equals e.MaNXB
                        join g in _db.TacGia on b.MaTacGia equals g.MaTacGia
                        join h in _db.TinhTrangCuonSach on a.MaTinhTrang equals h.MaTinhTrang
                        select new {a.MaCuonSach,b.TenDauSach,c.TenLoai,e.TenNXB,g.TenTacGia ,h.TenTinhTrang}).ToList();
            var list = temp.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
            System.Data.DataTable data = tool.LinqToDataTable(list);          
            return data;
        }
        //Search_PhanTrang
        public System.Data.DataTable Search_GetByPageLinq(String name,int pageSize, int pageNum)
        {
            var temp = (from a in _db.CuonSach
                        join b in _db.DauSach on a.MaDauSach equals b.MaDauSach
                        join c in _db.LoaiSach on b.MaLoai equals c.MaLoai
                        join d in _db.TrangThaiDauSach on b.MaTrangThai equals d.MaTrangThai
                        join e in _db.NXB on b.MaNXB equals e.MaNXB
                        join g in _db.TacGia on b.MaTacGia equals g.MaTacGia
                        join h in _db.TinhTrangCuonSach on a.MaTinhTrang equals h.MaTinhTrang
                        select new { a.MaCuonSach, b.TenDauSach, c.TenLoai, e.TenNXB, g.TenTacGia, h.TenTinhTrang });
            temp = temp.Where(v => v.TenDauSach.Contains(name)).OrderBy(v => v.TenDauSach);
            var list = temp.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
            System.Data.DataTable data = tool.LinqToDataTable(list);
            return data;
        }
        //layallfullinf
        public System.Data.DataTable GetallBylinq()
        {
            var temp = (from a in _db.CuonSach
                        join b in _db.DauSach on a.MaDauSach equals b.MaDauSach
                        join c in _db.LoaiSach on b.MaLoai equals c.MaLoai
                        join d in _db.TrangThaiDauSach on b.MaTrangThai equals d.MaTrangThai
                        join e in _db.NXB on b.MaNXB equals e.MaNXB
                        join g in _db.TacGia on b.MaTacGia equals g.MaTacGia
                        select new { a.MaCuonSach, b.TenDauSach, c.TenLoai, e.TenNXB, g.TenTacGia, d.TenTrangThai }).ToList();

            System.Data.DataTable data = tool.LinqToDataTable(temp);
            return data;
        }
        //search byID
        public System.Data.DataTable searchbyid(int id)
        {
            var temp = (from a in _db.CuonSach
                        join b in _db.DauSach on a.MaDauSach equals b.MaDauSach
                        join c in _db.LoaiSach on b.MaLoai equals c.MaLoai
                        join d in _db.TrangThaiDauSach on b.MaTrangThai equals d.MaTrangThai
                        join e in _db.NXB on b.MaNXB equals e.MaNXB
                        join g in _db.TacGia on b.MaTacGia equals g.MaTacGia
                        join h in _db.TinhTrangCuonSach on a.MaTinhTrang equals h.MaTinhTrang
                    
                        where a.MaCuonSach.Equals(id)
                        select new { a.MaCuonSach, b.TenDauSach, c.TenLoai, e.TenNXB, g.TenTacGia, h.TenTinhTrang}).ToList();

            System.Data.DataTable data = tool.LinqToDataTable(temp);
            return data;
        }
        // Lấy danh sách cuốn sách
        public List<CuonSach> GetAll()
        {
            try
            {
                return _db.CuonSach.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy cuốn sách
        public CuonSach GetByID(int id)
        {
            try
            {
                CuonSach record = _db.CuonSach.SingleOrDefault(v => v.MaCuonSach == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm cuốn sách
        public bool Add(CuonSach value)
        {
            try
            {
                _db.CuonSach.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật cuốn sách
        public bool Update(CuonSach value)
        {
            try
            {
                CuonSach record = _db.CuonSach.SingleOrDefault(v => v.MaCuonSach == value.MaCuonSach);
                //record.MaDauSach = value.MaDauSach;
                record.MaTinhTrang = value.MaTinhTrang;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa cuốn sách
        public bool Delete(int id)
        {
            try
            {
                CuonSach record = _db.CuonSach.SingleOrDefault(v => v.MaCuonSach == id);
                _db.CuonSach.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm cuốn sách
        public List<CuonSach> Search(int? madausach, int? tinhtrang)
        {
            try
            {
                var record = from r in _db.CuonSach select r;
                if (madausach != null) record = record.Where(r => r.MaDauSach == madausach);
                if (tinhtrang != null) record = record.Where(r => r.MaTinhTrang == tinhtrang);
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
