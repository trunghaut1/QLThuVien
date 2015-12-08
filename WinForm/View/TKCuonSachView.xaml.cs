using Core.Dal;
using Core.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FlatTheme.ControlStyle;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for TKCuonSachView.xaml
    /// </summary>
    public partial class TKCuonSachView : UserControl
    {
        BizDauSach _dausach = new BizDauSach();
        public TKCuonSachView()
        {
            InitializeComponent();
            LoadDauSach();
        }
        public void LoadDauSach()
        {
            cbxDauSach.ItemsSource = _dausach.GetAll();
        }

        private void cbxDauSach_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbxDauSach.SelectedIndex != -1) txtMaDauSach.IsEnabled = false;
            else txtMaDauSach.IsEnabled = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtMaDauSach.Text = null;
            cbxDauSach.SelectedIndex = -1;
            txtMaDauSach.IsEnabled = true;
            cbxDauSach.IsEnabled = true;
        }

        private void txtMaDauSach_KeyUp(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMaDauSach.Text)) cbxDauSach.IsEnabled = false;
            else cbxDauSach.IsEnabled = true;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if(txtMaDauSach.IsEnabled && cbxDauSach.IsEnabled)
            {
                MessageBox.Show("Vui lòng nhập mã đầu sách hoặc chọn đầu sách");
                return;
            }
            if(txtMaDauSach.IsEnabled && !cbxDauSach.IsEnabled)
            {
                var record = _dausach.GetByID(int.Parse(txtMaDauSach.Text));
                if (record != null)
                {
                    txtMaDauSach1.Text = record.MaDauSach.ToString();
                    txtTenDauSach.Text = record.TenDauSach;
                    txtNXB.Text = record.NXB.TenNXB;
                    txtTacGia.Text = record.TacGia.TenTacGia;
                    txtMaLoai.Text = record.LoaiSach.TenLoai;
                    txtSL.Text = record.CuonSach.Count.ToString();
                    LoadCuonSach(record.CuonSach.ToList());
                }
                else MessageBox.Show("Không tìm thấy đầu sách, vui lòng nhập lại hoặc chọn đầu sách");
            }
            else
            if(!txtMaDauSach.IsEnabled && cbxDauSach.IsEnabled)
            {
                var record = (DauSach)cbxDauSach.SelectedItem;
                if (record != null)
                {
                    txtMaDauSach1.Text = record.MaDauSach.ToString();
                    txtTenDauSach.Text = record.TenDauSach;
                    txtNXB.Text = record.NXB.TenNXB;
                    txtMaLoai.Text = record.LoaiSach.TenLoai;
                    txtTacGia.Text = record.TacGia.TenTacGia;
                    txtSL.Text = record.CuonSach.Count.ToString();
                    LoadCuonSach(record.CuonSach.ToList());
                }
                else MessageBox.Show("Không tìm thấy đầu sách, vui lòng nhập lại hoặc chọn đầu sách");
            }
        }

        private void LoadCuonSach(List<CuonSach> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["cuonSachViewSource"];
            myCollectionViewSource.Source = value;
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(txtMaDauSach1.Text))
            {
                var record = _dausach.GetByID(int.Parse(txtMaDauSach1.Text));
                FlatWindow window = new FlatWindow()
                {
                    Title = "CHI TIẾT ĐẦU SÁCH",
                    Name = "CTDauSach",
                    Content = new CTDauSachView(record),
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    FontSize = 16,
                    MinHeight = 500,
                    MinWidth = 1000
                };
                window.Style = Application.Current.FindResource("FlatWindow") as Style;
                window.ShowDialog();
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(txtMaDauSach1.Text))
            {
                var record = _dausach.GetByID(int.Parse(txtMaDauSach1.Text));
                if(!WriteExcel(record))
                    MessageBox.Show("Lỗi xuất báo cáo");
            }
            else MessageBox.Show("Vui lòng nhập hoặc chọn đầu sách");
        }
        private bool WriteExcel(DauSach record)
        {
            try
            {
                COMExcel.Application exApp = new COMExcel.Application();
                string workbookPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resource\BCCuonSach.xlsx");
                COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath,
                        0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
                ((Microsoft.Office.Interop.Excel._Worksheet)exSheet).Activate();
                exSheet.Cells[3, 3] = record.MaDauSach.ToString();
                exSheet.Cells[4, 3] = record.TenDauSach;
                exSheet.Cells[5, 3] = record.LoaiSach.TenLoai;
                exSheet.Cells[6, 3] = record.NXB.TenNXB;
                exSheet.Cells[7, 3] = record.TacGia.TenTacGia;
                exSheet.Cells[8, 3] = record.CuonSach.Count;
                // Xuất danh sách
                int i = 1;
                foreach(CuonSach item in record.CuonSach)
                {
                    exSheet.Cells[11 + i, 1] = i;
                    exSheet.Cells[11 + i, 2] = item.MaCuonSach;
                    exSheet.Cells[11 + i, 3] = item.TinhTrangCuonSach.TenTinhTrang;
                    i++;
                }
                //
                i = i + 10;
                COMExcel.Range r = (COMExcel.Range)exSheet.get_Range("A12", "C" + i);
                r.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                r.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                // Lưu file
                SaveFileDialog dialog = new SaveFileDialog { FileName = "BCCuonSach.xls", Filter = "Excel files|*.xls", DefaultExt = "xls", Title = "Chọn nơi lưu tệp báo cáo" };
                if (dialog.ShowDialog() == true)
                {
                    exBook.SaveAs(dialog.FileName, COMExcel.XlFileFormat.xlWorkbookNormal,
                                    null, null, false, false,
                                    COMExcel.XlSaveAsAccessMode.xlExclusive,
                                    false, false, false, false, false);
                    MessageBox.Show("Xuất báo cáo thành công");
                }
                //
                exBook.Close(false, false, false);
                exApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(exApp);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void txtMaDauSach_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 ||
                key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
        }
    }
}
