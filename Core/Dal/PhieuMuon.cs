//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuMuon
    {
        public PhieuMuon()
        {
            this.CTPhieuMuon = new HashSet<CTPhieuMuon>();
            this.PhieuTra = new HashSet<PhieuTra>();
        }
    
        public int MaPhieuMuon { get; set; }
        public Nullable<int> MaDocGia { get; set; }
        public Nullable<System.DateTime> NgayMuon { get; set; }
        public Nullable<bool> tinhtrang { get; set; }
    
        public virtual ICollection<CTPhieuMuon> CTPhieuMuon { get; set; }
        public virtual DocGia DocGia { get; set; }
        public virtual ICollection<PhieuTra> PhieuTra { get; set; }
    }
}
