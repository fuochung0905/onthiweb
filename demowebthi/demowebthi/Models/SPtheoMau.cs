//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace demowebthi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SPtheoMau
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SPtheoMau()
        {
            this.AnhChiTietSP = new HashSet<AnhChiTietSP>();
            this.ChiTietSPBan = new HashSet<ChiTietSPBan>();
        }
    
        public string MaSPTheoMau { get; set; }
        public string MaSanPham { get; set; }
        public string MaMau { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnhChiTietSP> AnhChiTietSP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietSPBan> ChiTietSPBan { get; set; }
        public virtual MauSac MauSac { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
