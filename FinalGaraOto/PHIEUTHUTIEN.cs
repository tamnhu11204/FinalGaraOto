//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalGaraOto
{
    using System;
    using System.Collections.Generic;
    
    public partial class PHIEUTHUTIEN
    {
        public int MaPhieuThuTien { get; set; }
        public int MaTiepNhan { get; set; }
        public System.DateTime NgayThuTien { get; set; }
        public decimal SoTienThu { get; set; }
    
        public virtual XE XE { get; set; }
    }
}
