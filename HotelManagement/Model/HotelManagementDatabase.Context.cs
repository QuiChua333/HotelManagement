﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelManagement.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HotelManagementEntities : DbContext
    {
        public HotelManagementEntities()
            : base("name=HotelManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Furniture> Furniture { get; set; }
        public virtual DbSet<FurnitureReceipt> FurnitureReceipt { get; set; }
        public virtual DbSet<FurnitureStorage> FurnitureStorage { get; set; }
        public virtual DbSet<GoodsStorage> GoodsStorage { get; set; }
        public virtual DbSet<RentalContract> RentalContract { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomCustomer> RoomCustomer { get; set; }
        public virtual DbSet<RoomFurnituresDetail> RoomFurnituresDetail { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceUsing> ServiceUsing { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Trouble> Trouble { get; set; }
        public virtual DbSet<TroubleByCustomer> TroubleByCustomer { get; set; }
    }
}