using HotelManagement.DTOs;
using HotelManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Model.Services
{
    public partial class OverviewStatisticService
    {
        private OverviewStatisticService() { }
        private static OverviewStatisticService _ins;
        public static OverviewStatisticService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new OverviewStatisticService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<int> GetBillQuantity(int year, int month = 0)
        {
            try
            {
                using (var context = new HotelManagementEntities())
                {
                    if (month == 0)
                    {
                        return await context.Bills.Where(b => b.CreateDate.Value.Year == year).CountAsync();
                    }
                    else
                    {
                        return await context.Bills.Where(b => b.CreateDate.Value.Year == year && b.CreateDate.Value.Month == month).CountAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<(List<double>, double ServiceRevenue, double RentalRevenue, string RentalRateStr)> GetRevenueByYear(int year)
        {
            List<double> MonthlyRevenueList = new List<double>(new double[12]);

            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var billList = context.Bills
                    .Where(b => b.CreateDate.Value.Year == year);

                    (double ServiceRevenue, double RentalRevenue) = await GetFullRevenue(context, year);

                    var MonthlyRevenue = billList
                             .GroupBy(b => b.CreateDate.Value.Month)
                             .Select(gr => new
                             {
                                 Month = gr.Key,
                                 Income = gr.Sum(b => (double?)b.TotalPrice) ?? 0
                             }).ToList();

                    foreach (var re in MonthlyRevenue)
                    {
                        MonthlyRevenueList[re.Month - 1] = (float)re.Income;
                    }

                    (double lastProdReve, double lastTicketReve) = await GetFullRevenue(context, year - 1);
                    double lastRevenueTotal = lastProdReve + lastTicketReve;
                    string RevenueRateStr;
                    if (lastRevenueTotal == 0)
                    {
                        RevenueRateStr = "-2";
                    }
                    else
                    {
                        RevenueRateStr = Helper.ConvertDoubleToPercentageStr((double)((ServiceRevenue + RentalRevenue) / lastRevenueTotal) - 1);
                    }

                    return (MonthlyRevenueList, (double)ServiceRevenue, (double)RentalRevenue, RevenueRateStr);
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<(double, double)> GetFullRevenue(HotelManagementEntities context, int year, int month = 0)
        {
            try
            {

                if (month != 0)
                {
                    year = DateTime.Now.Year;
                    double ServiceRevenue = await context.Bills.Where(x => x.CreateDate.Value.Year == year && x.CreateDate.Value.Month == month).SumAsync(x => x.ServicePrice) ?? 0;
                    double TroublePrice = await context.Bills.Where(x => x.CreateDate.Value.Year == year && x.CreateDate.Value.Month == month).SumAsync(x => x.TroublePrice) ?? 0;
                    double TotalPrice = await context.Bills.Where(x => x.CreateDate.Value.Year == year && x.CreateDate.Value.Month == month).SumAsync(x => x.TotalPrice) ?? 0;
                    double RentalRevenue = TotalPrice - TroublePrice - ServiceRevenue;
                    return (ServiceRevenue, RentalRevenue);
                }
                else
                {
                    double ServiceRevenue = await context.Bills.Where(x => x.CreateDate.Value.Year == year).SumAsync(x => x.ServicePrice) ?? 0;
                    double TroublePrice = await context.Bills.Where(x => x.CreateDate.Value.Year == year).SumAsync(x => x.TroublePrice) ?? 0;
                    double TotalPrice = await context.Bills.Where(x => x.CreateDate.Value.Year == year).SumAsync(x=> x.TotalPrice) ?? 0;
                    double RentalRevenue = TotalPrice - TroublePrice - ServiceRevenue;
                    return ( ServiceRevenue,RentalRevenue);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<(List<double> MonthlyExpense, double ServiceExpense, double RepairCost, string ExpenseRate)> GetExpenseByYear(int year)
        {
            List<double> MonthlyExpense = new List<double>(new double[12]);
            double ServiceExpenseTotal = 0;
            double RepairCostTotal = 0;

            try
            {
                using (var context = new HotelManagementEntities())
                {
                    var MonthlyServiceExpense = await context.GoodsReceipts
                     .Where(b => b.CreateAt.Value.Year == year)
                     .GroupBy(b => b.CreateAt.Value.Month)
                     .Select(gr => new
                     {
                         Month = gr.Key,
                         Outcome = gr.Sum(b => (double)b.ImportPrice * b.Quantity) ?? 0
                     }).ToListAsync();


                    var MonthRepairCostByCustomer = await context.TroubleByCustomers
                         .Where(t => t.Trouble.FinishDate != null && t.Trouble.FinishDate.Value.Year == year)
                         .GroupBy(t => t.Trouble.FinishDate.Value.Month)
                         .Select(gr =>
                         new
                         {
                             Month = gr.Key,
                             Outcome = gr.Sum(t => t.PredictedPrice) ?? 0
                         }).ToListAsync();

                    var MonthlyRepairCost = await context.Troubles
                         .Where(t => t.FinishDate != null && t.FinishDate.Value.Year == year)
                         .GroupBy(t => t.FinishDate.Value.Month)
                         .Select(gr =>
                         new
                         {
                             Month = gr.Key,
                             Outcome = (gr.Sum(t => (double)t.Price)) - MonthRepairCostByCustomer.Where(x => x.Month == gr.Key).Select(x => x.Outcome),
                         }).ToListAsync();

                    

                    //Accumulate
                    foreach (var ex in MonthlyServiceExpense)
                    {
                        MonthlyExpense[ex.Month - 1] += (double)ex.Outcome;
                        ServiceExpenseTotal += ex.Outcome;
                    }

                    foreach (var ex in MonthlyRepairCost)
                    {
                        MonthlyExpense[ex.Month - 1] += (double)ex.Outcome;
                        RepairCostTotal += ex.Outcome;
                    }

                    double lastProductExpenseTotal = await GetFullExpenseLastTime(context, year - 1);

                    string ExpenseRateStr;
                    //check mẫu  = 0
                    if (lastProductExpenseTotal == 0)
                    {
                        ExpenseRateStr = "-2";
                    }
                    else
                    {
                        ExpenseRateStr = Helper.ConvertDoubleToPercentageStr(((double)(ServiceExpenseTotal / lastProductExpenseTotal) - 1));
                    }


                    return (MonthlyExpense, (double)ServiceExpenseTotal, (double)RepairCostTotal, ExpenseRateStr);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<double> GetFullExpenseLastTime(HotelManagementEntities context, int year, int month = 0)
        {
            try
            {
                if (month == 0)
                {
                    //Service Receipt
                    double LastYearServiceExpense = await context.GoodsReceipts
                             .Where(pr => pr.CreateAt.Value.Year == year)
                             .SumAsync(pr => (double)(pr.ImportPrice * pr.Quantity));

                    //Repair Cost
                    var LastYearRepairCost = await context.Troubles
                             .Where(tr => tr.FinishDate != null && tr.FinishDate.Value.Year == year)
                             .SumAsync(tr => (double)tr.Price);

                    var LastYearRepairCostByCustomer = await context.TroubleByCustomers
                             .Where(tr => tr.Trouble.FinishDate != null && tr.Trouble.FinishDate.Value.Year == year)
                             .SumAsync(tr => (double)tr.PredictedPrice);

                    return (LastYearServiceExpense + LastYearRepairCost - LastYearRepairCostByCustomer);
                }
                else
                {
                    //Service Receipt
                    double LastMonthServiceExpense = await context.GoodsReceipts
                             .Where(pr => pr.CreateAt.Value.Year == year && pr.CreateAt.Value.Month == month)
                             .SumAsync(pr => (double)(pr.ImportPrice * pr.Quantity));

                    //Repair Cost
                    var LastMonthRepairCost = await context.Troubles
                             .Where(tr => tr.FinishDate != null && tr.FinishDate.Value.Year == year && tr.FinishDate.Value.Month == month)
                             .SumAsync(tr => (double)tr.Price);

                    var LastMonthRepairCostByCustomer = await context.TroubleByCustomers
                             .Where(tr => tr.Trouble.FinishDate != null && tr.Trouble.FinishDate.Value.Year == year && tr.Trouble.FinishDate.Value.Month == month)
                             .SumAsync(tr => (double)tr.PredictedPrice);


                    return (LastMonthServiceExpense + LastMonthRepairCost - LastMonthRepairCostByCustomer);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<(List<double>, double ServiceRevenue, double RentalRevenue, string RevenueRate)> GetRevenueByMonth(int year, int month)
        {
            int days = DateTime.DaysInMonth(year, month);
            List<double> DailyReveList = new List<double>(new double[days]);

            try
            {

                using (var context = new HotelManagementEntities())
                {
                    var billList = context.Bills
                     .Where(b => b.CreateDate.Value.Year == year && b.CreateDate.Value.Month == month);

                    (double ServiceRevenue, double RentalRevenue) = await GetFullRevenue(context, year, month);

                    var dailyRevenue = await billList
                                .GroupBy(b => b.CreateDate.Value.Day)
                                 .Select(gr => new
                                 {
                                     Day = gr.Key,
                                     Income = gr.Sum(b => b.TotalPrice),
                                     DiscountPrice = gr.Sum(b => (double?)b.DiscountPrice) ?? 0,
                                 }).ToListAsync();

                    foreach (var re in dailyRevenue)
                    {
                        DailyReveList[re.Day - 1] = (double)re.Income;
                    }

                    if (month == 1)
                    {
                        year--;
                        month = 13;
                    }
                    (double lastProdReve, double lastTicketReve) = await GetFullRevenue(context, year, month - 1);
                    double lastRevenueTotal = lastProdReve + lastTicketReve;
                    string RevenueRateStr;
                    if (lastRevenueTotal == 0)
                    {
                        RevenueRateStr = "-2";
                    }
                    else
                    {
                        RevenueRateStr = Helper.ConvertDoubleToPercentageStr((double)((ServiceRevenue + RentalRevenue) / lastRevenueTotal) - 1);
                    }

                    return (DailyReveList, (double)ServiceRevenue, (double)RentalRevenue, RevenueRateStr);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<(List<double> DailyExpense, double ServiceExpense, double RepairCost, string RepairRateStr)> GetExpenseByMonth(int year, int month)
        {

            int days = DateTime.DaysInMonth(year, month);
            List<double> DailyExpense = new List<double>(new double[days]);
            double ServiceExpenseTotal = 0;
            double RepairCostTotal = 0;

            try
            {

                using (var context = new HotelManagementEntities())
                {
                    //Service Receipt
                    var MonthlyServiceExpense = await context.GoodsReceipts
                         .Where(b => b.CreateAt.Value.Year == year && b.CreateAt.Value.Month == month)
                         .GroupBy(b => b.CreateAt.Value.Day)
                         .Select(gr => new
                         {
                             Day = gr.Key,
                             Outcome = gr.Sum(b => (double?)b.ImportPrice * b.Quantity) ?? 0
                         }).ToListAsync();
                    //Repair Cost


                    var MonthRepairCostByCustomer = await context.TroubleByCustomers
                         .Where(t => t.Trouble.FinishDate != null && t.Trouble.FinishDate.Value.Year == year)
                         .GroupBy(t => t.Trouble.FinishDate.Value.Day)
                         .Select(gr =>
                         new
                         {
                             Day = gr.Key,
                             Outcome = gr.Sum(t => t.PredictedPrice) ?? 0
                         }).ToListAsync();

                    var MonthlyRepairCost = await context.Troubles
                         .Where(t => t.FinishDate != null && t.FinishDate.Value.Year == year)
                         .GroupBy(t => t.FinishDate.Value.Day)
                         .Select(gr =>
                         new
                         {
                             Day = gr.Key,
                             Outcome = (gr.Sum(t => (double)t.Price)) - Double.Parse(MonthRepairCostByCustomer.Where(x => x.Day == gr.Key).Select(x => x.Outcome).ToString())
                         }).ToListAsync();


                   
                    //context.
                    //Accumulate
                    foreach (var ex in MonthlyServiceExpense)
                    {
                        DailyExpense[ex.Day - 1] += (double)ex.Outcome;
                        ServiceExpenseTotal += ex.Outcome;
                    }

                    foreach (var ex in MonthlyRepairCost)
                    {
                        DailyExpense[ex.Day - 1] += (double)ex.Outcome;
                        RepairCostTotal += ex.Outcome;
                    }
                    if (month == 1)
                    {
                        year--;
                        month = 13;
                    }


                    double lastProductExpenseTotal = await GetFullExpenseLastTime(context, year, month - 1);
                    string ExpenseRateStr;
                    //check mẫu  = 0
                    if (lastProductExpenseTotal == 0)
                    {
                        ExpenseRateStr = "-2";
                    }
                    else
                    {
                        ExpenseRateStr = Helper.ConvertDoubleToPercentageStr(((double)(ServiceExpenseTotal / lastProductExpenseTotal) - 1));
                    }

                    return (DailyExpense, ServiceExpenseTotal, RepairCostTotal, ExpenseRateStr);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<RoomTypeDTO>> GetListRoomTypeRevenue(string period, string value)
        {
            try
            {

                using (var context = new HotelManagementEntities())
                {
                    
                    if (period == "Theo năm") 
                    {
                        int year = int.Parse(value);
                        var list1 = await context.Bills.Where(x => x.CreateDate.Value.Year == year).ToListAsync();
                        var list2 = list1.GroupBy(b => b.RentalContract.Room.RoomType.RoomTypeName)
                            .Select(gr => new RoomTypeDTO
                            {
                                RoomTypeName = gr.First().RentalContract.Room.RoomType.RoomTypeName,
                                Revenue = (double)gr.Sum(x=> x.TotalPrice - x.ServicePrice - x.TroublePrice)
                            }).ToList();
                        return list2;
                    }
                    else
                    {
                        int month = int.Parse(value);
                        var list1 = await context.Bills.Where(x => x.CreateDate.Value.Year == DateTime.Now.Year && x.CreateDate.Value.Month == month).ToListAsync();
                        var list2 = list1.GroupBy(b => b.RentalContract.Room.RoomType.RoomTypeName)
                            .Select(gr => new RoomTypeDTO
                            {
                                RoomTypeName = gr.First().RentalContract.Room.RoomType.RoomTypeName,
                                Revenue = (double)gr.Sum(x => x.TotalPrice - x.ServicePrice - x.TroublePrice)
                            }).ToList();
                        for (int i = 0; i < list2.Count; i++)
                        {
                            list2[i].STT = i + 1;
                        }
                        return list2;
                    }
                       
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<ServiceTypeDTO>> GetListServiceTypeRevenue(string period, string value)
        {
            try
            {

                using (var context = new HotelManagementEntities())
                {

                    if (period == "Theo năm")
                    {
                        int year = int.Parse(value);
                        var listRentalContract = await context.Bills.Where(x => x.CreateDate.Value.Year == year).Select(x => x.RentalContractId).ToListAsync();

                        var list1 = await context.ServiceUsings.Where(x=> listRentalContract.Contains(x.RentalContractId)).ToListAsync();
                        var list2 = list1.GroupBy(b => b.Service.ServiceType)
                            .Select(gr => new ServiceTypeDTO
                            { 


                                ServiceType = gr.First().Service.ServiceType,
                                Revenue = (double)gr.Sum(x=> x.Quantity * x.UnitPrice)
                              

                            }).ToList();
                        for (int i = 0; i< list2.Count; i++)
                        {
                            list2[i].STT = i + 1;
                        }
                        return list2;
                    }
                    else
                    {
                        int month = int.Parse(value);
                        var listRentalContract = await context.Bills.Where(x => x.CreateDate.Value.Year == DateTime.Now.Year && x.CreateDate.Value.Year == month).Select(x => x.RentalContractId).ToListAsync();

                        var list1 = await context.ServiceUsings.Where(x => listRentalContract.Contains(x.RentalContractId)).ToListAsync();
                        var list2 = list1.GroupBy(b => b.Service.ServiceType)
                            .Select(gr => new ServiceTypeDTO
                            {


                                ServiceType = gr.First().Service.ServiceType,
                                Revenue = (double)gr.Sum(x => x.Quantity * x.UnitPrice)
                                

                            }).ToList();
                        for (int i = 0; i < list2.Count; i++)
                        {
                            list2[i].STT = i + 1;
                        }
                        return list2;
                    }

                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
