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
    
    public partial class CHUXE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUXE()
        {
            this.XEs = new HashSet<XE>();
        }
    
        public int MaChuXe { get; set; }
        public string TenChuXe { get; set; }
        public string DiaChiChuXe { get; set; }
        public string SDTChuXe { get; set; }
        public string EmailChuXe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<XE> XEs { get; set; }
    }
}
