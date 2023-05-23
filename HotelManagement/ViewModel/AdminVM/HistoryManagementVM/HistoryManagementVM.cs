using System.Windows.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using HotelManagement.DTOs;
using System.Windows.Controls;
using HotelManagement.View.Admin.HistoryManagement;
using HotelManagement.View.CustomMessageBoxWindow;
using HotelManagement.Model.Services;
using System.Windows.Forms;
using ComboBox = System.Windows.Controls.ComboBox;

namespace HotelManagement.ViewModel.AdminVM.HistoryManagementVM
{
    public class HistoryManagementVM : BaseVM
    {
        private int SelectedView = 0;
        private ObservableCollection<ImportProductDTO> importList;
        public ObservableCollection<ImportProductDTO> ImportList
        {
            get { return importList; }
            set { importList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ExportBillDTO> billExportList;
        public ObservableCollection<ExportBillDTO> BillExportList
        {
            get { return billExportList; }
            set { billExportList = value; OnPropertyChanged(); }
        }
        private ComboBoxItem _selectedFilterImportItem;
        public ComboBoxItem SelectedFilterImportItem
        {
            get { return _selectedFilterImportItem; }
            set { _selectedFilterImportItem = value; OnPropertyChanged(); }
        }
        private int _selectedMonthImportItem;
        public int SelectedMonthImportItem
        {
            get { return _selectedMonthImportItem; }
            set { _selectedMonthImportItem = value; OnPropertyChanged(); }
        }
        private int _selectedMonthExportItem;
        public int SelectedMonthExportItem
        {
            get { return _selectedMonthExportItem; }
            set { _selectedMonthExportItem = value; OnPropertyChanged(); }
        }
        private int _filterImportList;
        public int FilterImportList
        {
            get { return _filterImportList; }
            set { _filterImportList = value; OnPropertyChanged(); }
        }
        public ICommand LoadImportPageCM { get; set; }
        public ICommand LoadExportPageCM { get; set; }
        public ICommand CheckImportItemFilterCM { get; set; }
        public ICommand SelectedImportMonthCM { get; set; }
        public ICommand ExportFileCM { get; set; }
        public ICommand FilterListImportCM { get; set; }

        public HistoryManagementVM()
        {
            //GetCurrentDate = DateTime.Today;
            //SelectedDate = GetCurrentDate;
            _selectedMonthExportItem = DateTime.Now.Month - 1;
            SelectedMonthImportItem = DateTime.Now.Month - 1;
            LoadImportPageCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                SelectedView = 0;
                await GetImportListSource();
                ImportManagementPage page = new ImportManagementPage();
                p.Content = page;
            });
            LoadExportPageCM = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                SelectedView = 1;
                BillExportList = new ObservableCollection<ExportBillDTO>();
                await GetExportListSource("date");
                ExportManagementPage page = new ExportManagementPage();
                p.Content = page;

            });
            CheckImportItemFilterCM = new RelayCommand<System.Windows.Controls.ComboBox>((p) => { return true; }, async (p) =>
            {
                switch (SelectedFilterImportItem.Tag.ToString())
                {
                    case "Toàn bộ":
                        {
                            await GetImportListSource("");
                            return;
                        }
                    case "Theo tháng":
                        {
                            await GetImportListSource("month");
                            return;
                        }
                }
            });
            SelectedImportMonthCM = new RelayCommand<System.Windows.Controls.ComboBox>((p) => { return true; }, async (p) =>
            {
                await GetListImportByMonth();
            });
            ExportFileCM = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ExportToFileFunc();
            });
            FilterListImportCM = new RelayCommand<ComboBox>(p => true, async p =>
            {
                switch (SelectedFilterImportItem.Tag.ToString())
                {
                    case "Toàn bộ":
                        {
                            ObservableCollection<ImportProductDTO> iplist = new ObservableCollection<ImportProductDTO>();
                            ObservableCollection<ImportProductDTO> importserviceList = new ObservableCollection<ImportProductDTO>(await ServiceHelper.Ins.GetListImportService());
                            ObservableCollection<ImportProductDTO> importFunitureList = new ObservableCollection<ImportProductDTO>(await FurnitureService.Ins.GetListImportFuniture());
                            foreach (var item in importserviceList)
                            {
                                iplist.Add(item);
                            }
                            foreach (var item in importFunitureList)
                            {
                                iplist.Add(item);
                            }
                            if (FilterImportList == 0)
                            {
                                ImportList = new ObservableCollection<ImportProductDTO>(iplist);
                            }
                            if(FilterImportList == 1)
                            {
                                ImportList = new ObservableCollection<ImportProductDTO>(iplist.Where(ip => ip.typeimport==0));
                            }
                            if(FilterImportList == 2)
                            {
                                ImportList = new ObservableCollection<ImportProductDTO>(iplist.Where(ip => ip.typeimport == 1));
                            }
                            return;
                        }
                    case "Theo tháng":
                        {
                            ObservableCollection<ImportProductDTO> iplist = new ObservableCollection<ImportProductDTO>();

                            ObservableCollection<ImportProductDTO> importserviceList = new ObservableCollection<ImportProductDTO>(await ServiceHelper.Ins.GetListImportService(SelectedMonthImportItem + 1));
                            ObservableCollection<ImportProductDTO> importFunitureList = new ObservableCollection<ImportProductDTO>(await FurnitureService.Ins.GetListImportFuniture(SelectedMonthImportItem + 1));
                            foreach (var item in importserviceList)
                            {
                                iplist.Add(item);
                            }
                            foreach (var item in importFunitureList)
                            {
                                iplist.Add(item);
                            }
                            if (FilterImportList == 0)
                            {
                                ImportList = new ObservableCollection<ImportProductDTO>(iplist);
                            }
                            if (FilterImportList == 1)
                            {
                                ImportList = new ObservableCollection<ImportProductDTO>(iplist.Where(ip => ip.typeimport == 0));
                            }
                            if (FilterImportList == 2)
                            {
                                ImportList = new ObservableCollection<ImportProductDTO>(iplist.Where(ip => ip.typeimport == 1));
                            }
                            return;
                        }
                }

            });
        }

        private async Task GetListImportByMonth()
        {
            ImportList = new ObservableCollection<ImportProductDTO>();
            try
            {
                ObservableCollection<ImportProductDTO> importserviceList = new ObservableCollection<ImportProductDTO>(await ServiceHelper.Ins.GetListImportService(SelectedMonthImportItem + 1));
                ObservableCollection<ImportProductDTO> importFunitureList = new ObservableCollection<ImportProductDTO>(await FurnitureService.Ins.GetListImportFuniture(SelectedMonthImportItem + 1));
                foreach (var item in importserviceList)
                {
                    ImportList.Add(item);
                }
                foreach (var item in importFunitureList)
                {
                    ImportList.Add(item);
                }
                return;
            }
            catch (System.Data.Entity.Core.EntityException e)
            {
                CustomMessageBox.ShowOk("Mất kết nối cơ sở dữ liệu", "Lỗi", "OK", CustomMessageBoxImage.Error);
                throw;
            }
            catch (Exception e)
            {
                CustomMessageBox.ShowOk("Lỗi hệ thống", "Lỗi", "OK", CustomMessageBoxImage.Error);
                throw;
            }
        }

        private async Task GetExportListSource(string v = "")
        {

        }

        private async Task GetImportListSource(string v = "")
        {
            ImportList = new ObservableCollection<ImportProductDTO>();
            switch (v)
            {
                case "":
                    {
                        try
                        {
                            ObservableCollection<ImportProductDTO> importserviceList = new ObservableCollection<ImportProductDTO>(await ServiceHelper.Ins.GetListImportService());
                            ObservableCollection<ImportProductDTO> importFunitureList = new ObservableCollection<ImportProductDTO>(await FurnitureService.Ins.GetListImportFuniture());
                            foreach (var item in importserviceList)
                            {
                                ImportList.Add(item);
                            }
                            foreach (var item in importFunitureList)
                            {
                                ImportList.Add(item);
                            }
                            return;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            CustomMessageBox.ShowOk("Mất kết nối cơ sở dữ liệu", "Lỗi", "OK", CustomMessageBoxImage.Error);
                            throw;
                        }
                        catch (Exception e)
                        {
                            CustomMessageBox.ShowOk("Lỗi hệ thống", "Lỗi", "OK", CustomMessageBoxImage.Error);
                            throw;
                        }

                    }
                case "month":
                    {
                       await GetListImportByMonth();
                        return;
                    }
            }
        }
        public void ExportToFileFunc()
        {
            switch (SelectedView)
            {
                case 0:
                    {
                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                        {
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                app.Visible = false;
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];


                                ws.Cells[1, 1] = "Mã đơn";
                                ws.Cells[1, 2] = "Tên đơn";
                                ws.Cells[1, 3] = "Số lượng";
                                ws.Cells[1, 4] = "Tổng giá";
                                ws.Cells[1, 5] = "Nhân viên";
                                ws.Cells[1, 6] = "Ngày nhập";

                                int i2 = 2;
                                foreach (var item in ImportList)
                                {
                                    ws.Cells[i2, 1] = item.ImportId;
                                    ws.Cells[i2, 2] = item.ProductName;
                                    ws.Cells[i2, 3] = item.ProductImportQuantity;
                                    ws.Cells[i2, 4] = item.ProductImportPrice;
                                    ws.Cells[i2, 5] = item.StaffName;
                                    ws.Cells[i2, 6] = item.CreatedDate;

                                    i2++;
                                }
                                ws.SaveAs(sfd.FileName);
                                wb.Close();
                                app.Quit();

                                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;

                                CustomMessageBox.ShowOk("Xuất file thành công", "Thông báo", "OK", CustomMessageBoxImage.Success);
                            }
                        }
                        break;
                    }
                case 1:
                    {

                        using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
                        {
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                                app.Visible = false;
                                Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
                                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];


                                ws.Cells[1, 1] = "Mã đơn";
                                ws.Cells[1, 2] = "Ngày xuất";
                                ws.Cells[1, 3] = "Khách hàng";
                                ws.Cells[1, 4] = "Số điện thoại";
                                ws.Cells[1, 5] = "Tổng giá";                                
                                ws.Cells[1, 6] = "Nhân viên xuất";

                                int i2 = 2;
                                foreach (var item in BillExportList)
                                {

                                    ws.Cells[i2, 1] = item.BillId;
                                    ws.Cells[i2, 2] = item.CreatedDate;
                                    ws.Cells[i2, 3] = item.CusName;
                                    ws.Cells[i2, 4] = "'" + item.PhoneNum;
                                    ws.Cells[i2, 5] = item.Price;
                                    ws.Cells[i2, 6] = item.StaffName;
                                    

                                    i2++;
                                }
                                ws.SaveAs(sfd.FileName);
                                wb.Close();
                                app.Quit();

                                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                                CustomMessageBox.ShowOk("Xuất file thành công", "Thông báo", "OK", CustomMessageBoxImage.Success);

                            }
                        }
                        break;
                    }
            }
        }
    }
}
