using Core.Dal;

namespace WinForm.Resource
{
    public class TKDocGia : DocGia
    {
        public int? DangMuon { get; set; }
        public TKDocGia(DocGia value)
        {
            this.MaDocGia = value.MaDocGia;
            this.TenDocGia = value.TenDocGia;
            this.SDT = value.SDT;
            this.Email = value.Email;
            this.DiaChi = value.DiaChi;
            this.VienChuc = value.VienChuc;
            this.PhieuMuon = value.PhieuMuon;
            this.NamTotNgiep = value.NamTotNgiep;
            this.NgayCap = value.NgayCap;
            this.NgayHetHan = value.NgayHetHan;
        }
        public TKDocGia()
        {

        }
    }
}
