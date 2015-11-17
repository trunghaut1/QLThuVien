using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Biz
{
    public class BizDocGia
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        // Lấy danh sách độc giả
        public List<DocGia> GetAll()
        {
            try
            {
                return _db.DocGia.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Lấy độc giả
        public DocGia GetByID(int id)
        {
            try
            {
                DocGia record = _db.DocGia.SingleOrDefault(v => v.MaDocGia == id);
                return record;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        // Thêm độc giả
        public bool Add(DocGia value)
        {
            try
            {
                _db.DocGia.Add(value);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Cập nhật độc giả
        public bool Update(DocGia value)
        {
            try
            {
                DocGia record = _db.DocGia.SingleOrDefault(v => v.MaDocGia == value.MaDocGia);
                record.TenDocGia = value.TenDocGia;
                record.SDT = value.SDT;
                record.DiaChi = value.DiaChi;
                record.Email = value.Email;
                record.NamTotNgiep = value.NamTotNgiep;
                record.NgayCap = value.NgayCap;
                record.NgayHetHan = value.NgayHetHan;
                record.VienChuc = value.VienChuc;
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Xóa độc giả
        public bool Delete(int id)
        {
            try
            {
                DocGia record = _db.DocGia.SingleOrDefault(v => v.MaDocGia == id);
                _db.DocGia.Remove(record);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        // Tìm độc giả
        public List<DocGia> Search(string ten, string sdt, string diachi, string email,
            int? nam, bool? vienchuc, DateTime? ngaycap, DateTime? hethan)
        {
            try
            {
                var record = from r in _db.DocGia select r;
                if (ten != null) record = record.Where(r => r.TenDocGia.Contains(ten));
                if (sdt != null) record = record.Where(r => r.SDT.Contains(sdt));
                if (diachi != null) record = record.Where(r => r.DiaChi.Contains(diachi));
                if (email != null) record = record.Where(r => r.Email.Contains(email));
                if (nam != null) record = record.Where(r => r.NamTotNgiep == nam);
                if (vienchuc != null) record = record.Where(r => r.VienChuc.Value == vienchuc.Value);
                // Điều kiện ngày cấp >= ngaycap
                if (ngaycap != null) record = record.Where(r => r.NgayCap >= ngaycap);
                // Điều kiện ngày hết hạn <= henhan
                if (hethan != null) record = record.Where(r => r.NgayHetHan <= hethan);
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
