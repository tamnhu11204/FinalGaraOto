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
    
    public partial class CHITIETPHIEUNHAP
    {
        public int MaVatTuPhuTung { get; set; }
        public int MaNhapHang { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> GiaNhap { get; set; }
        public Nullable<decimal> ThanhTien { get; set; }
    
        public virtual PHIEUNHAP PHIEUNHAP { get; set; }
        public virtual VATTUPHUTUNG VATTUPHUTUNG { get; set; }
    }
}
