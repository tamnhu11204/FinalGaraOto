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
    
    public partial class CTBAOCAODOANHTHU
    {
        public int MaBaoCaoDoanhThu { get; set; }
        public int MaHieuXe { get; set; }
        public Nullable<int> SoLuotSua { get; set; }
        public Nullable<decimal> ThanhTien { get; set; }
        public Nullable<float> TiLe { get; set; }
    
        public virtual BAOCAODOANHTHU BAOCAODOANHTHU { get; set; }
        public virtual HIEUXE HIEUXE { get; set; }
    }
}
