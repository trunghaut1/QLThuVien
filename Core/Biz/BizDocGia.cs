using Core.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
namespace Core.Biz
{
    public class BizDocGia
    {
        QLThuVienEntities _db = new QLThuVienEntities();
        //phantrang+List
        public List<DocGia> GetByPageLinq(int pageSize, int pageNum)
        {
            var temp = (from a in _db.DocGia orderby a.NgayCap select a).ToList();


            var list = temp.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
            return list;
        }
        public List<DocGia> _GetByPageLinq(String name,int pageSize, int pageNum)
        {
            var temp = from a in _db.DocGia   select a;
            temp = temp.Where(v => v.TenDocGia.Contains(name)).OrderBy(v => v.MaDocGia);      
            var list = temp.Skip(pageSize * (pageNum - 1)).Take(pageSize).ToList();
            return list;
        }
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
        //laydsDGtheoID
        public List<DocGia> GetListByID(int ma)
        {
            try
            {

                var query = from c in _db.DocGia
                            where c.MaDocGia == ma
                            select c;
                return query.ToList();
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
