﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.DTOs
{
    public class BillDTO
    {
        public string BillId { get; set; }
        public string RentalContractId { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomName
        {
            get
            {
                return "Phòng " + RoomNumber;
            }
        }
        public Nullable<double> RoomPrice { get; set; }
        public Nullable<int> NumberOfRentalDays { get; set; }
        public Nullable<double> ServicePrice { get; set; }
        public Nullable<double> TroublePrice { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<double> DiscountPrice { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.DateTime> CheckOutDate { get; set; }
        public int DayNumber
        {
            get
            {
                if (CheckOutDate == null || StartDate == null)
                {
                    return 0;
                }
                
                TimeSpan t = (TimeSpan)(CheckOutDate - StartDate);
                int res = (int)t.TotalDays;
                return res;
            }
        }
        public IList<ServiceUsingDTO> ListListServicePayment { get; set; }
        public IList<TroubleByCustomerDTO> ListTroubleByCustomer { get; set; }
        public double ServicePriceTemp
        {
            get
            {
                double t = 0;
                foreach (var item in ListListServicePayment)
                {
                    t += item.TotalMoney;
                }
                return t;
            }
        }
        public double TroublePriceTemp
        {
            get
            {
                double t = 0;
                foreach(var item in ListTroubleByCustomer)
                {
                    t += (double)item.PredictedPrice;
                }
                return t;
            }
        }
        public double TotalPriceTemp
        {
            get
            {
                return ServicePriceTemp + TroublePriceTemp;
            }
        }

    }
}
