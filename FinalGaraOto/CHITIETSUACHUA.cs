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
    
    public partial class CHITIETSUACHUA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHITIETSUACHUA()
        {
            this.CT_SUDUNGVTPT = new HashSet<CT_SUDUNGVTPT>();
        }
    
        public int MaChiTietSuaChua { get; set; }
        public int MaSuaChua { get; set; }
        public int MaTienCong { get; set; }
        public string NoiDung { get; set; }
        public Nullable<decimal> TongTienVTPT { get; set; }
        public Nullable<decimal> TongCong { get; set; }
    
        public virtual PHIEUSUACHUA PHIEUSUACHUA { get; set; }
        public virtual TIENCONG TIENCONG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_SUDUNGVTPT> CT_SUDUNGVTPT { get; set; }
    }
}
