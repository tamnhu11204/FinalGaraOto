﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLGARAOTOEntities : DbContext
    {
        public QLGARAOTOEntities()
            : base("name=QLGARAOTOEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BAOCAODOANHTHU> BAOCAODOANHTHUs { get; set; }
        public virtual DbSet<BAOCAOTON> BAOCAOTONs { get; set; }
        public virtual DbSet<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
        public virtual DbSet<CHITIETSUACHUA> CHITIETSUACHUAs { get; set; }
        public virtual DbSet<CHUXE> CHUXEs { get; set; }
        public virtual DbSet<CT_SUDUNGVTPT> CT_SUDUNGVTPT { get; set; }
        public virtual DbSet<CTBAOCAODOANHTHU> CTBAOCAODOANHTHUs { get; set; }
        public virtual DbSet<DONVITINH> DONVITINHs { get; set; }
        public virtual DbSet<HIEUXE> HIEUXEs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHACUNGCAP> NHACUNGCAPs { get; set; }
        public virtual DbSet<NHOMNGUOIDUNG> NHOMNGUOIDUNGs { get; set; }
        public virtual DbSet<PHIEUNHAP> PHIEUNHAPs { get; set; }
        public virtual DbSet<PHIEUSUACHUA> PHIEUSUACHUAs { get; set; }
        public virtual DbSet<PHIEUTHUTIEN> PHIEUTHUTIENs { get; set; }
        public virtual DbSet<THAMSO> THAMSOes { get; set; }
        public virtual DbSet<TIENCONG> TIENCONGs { get; set; }
        public virtual DbSet<VATTUPHUTUNG> VATTUPHUTUNGs { get; set; }
        public virtual DbSet<XE> XEs { get; set; }
    }
}
