//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Furniture
    {
        public string FurnitureId { get; set; }
        public string FurnitureName { get; set; }
        public string FurnitureType { get; set; }
        public byte[] FurnitureAvatar { get; set; }
    
        public virtual FurnitureStorage FurnitureStorage { get; set; }
    }
}
