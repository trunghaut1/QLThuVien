using Core.Dal;
using Core.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using FlatTheme.ControlStyle;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Win32;
using System.IO;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for TKTacGiaView.xaml
    /// </summary>
    public partial class TKTacGiaView : UserControl
    {
        BizTacGia _tacgia = new BizTacGia();
        public TKTacGiaView()
        {
            InitializeComponent();
            LoadTacGia();
        }
        public void LoadTacGia()
        {
            cbxTacGia.ItemsSource = _tacgia.GetAll();
        }

        private void cbxTacGia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTacGia.SelectedIndex != -1) txtMaTacGia.IsEnabled = false;
            else txtMaTacGia.IsEnabled = true;
        }

        private void txtMaTacGia_KeyUp(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(txtMaTacGia.Text)) cbxTacGia.IsEnabled = false;
            else cbxTacGia.IsEnabled = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtMaTacGia.Text = null;
            cbxTacGia.SelectedIndex = -1;
            txtMaTacGia.IsEnabled = true;
            cbxTacGia.IsEnabled = true;
        }

        private void txtMaTacGia_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || 
                key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
        }

        private void LoadDauSach(List<DauSach> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["dauSachViewSource"];
            myCollectionViewSource.Source = value;
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (txtMaTacGia.IsEnabled && cbxTacGia.IsEnabled)
            {
                MessageBox.Show("Vui lòng nhập mã tác giả hoặc chọn tác giả");
                return;
            }
            if (txtMaTacGia.IsEnabled && !cbxTacGia.IsEnabled)
            {
                var record = _tacgia.GetByID(int.Parse(txtMaTacGia.Text));
                if (record != null)
                {
                    txtMaTacGia1.Text = record.MaTacGia.ToString();
                    txtTenTacGia.Text = record.TenTacGia;
                    txtSL.Text = record.DauSach.Count.ToString();
                    txtNoiCongTac.Text = record.NoiCongTac;
                    LoadDauSach(record.DauSach.ToList());
                }
                else MessageBox.Show("Không tìm thấy tác giả, vui lòng nhập lại hoặc chọn tác giả");
            }
            else
            if (!txtMaTacGia.IsEnabled && cbxTacGia.IsEnabled)
            {
                var record = (TacGia)cbxTacGia.SelectedItem;
                if (record != null)
                {
                    txtMaTacGia1.Text = record.MaTacGia.ToString();
                    txtTenTacGia.Text = record.TenTacGia;
                    txtSL.Text = record.DauSach.Count.ToString();
                    txtNoiCongTac.Text = record.NoiCongTac;
                    LoadDauSach(record.DauSach.ToList());
                }
                else MessageBox.Show("Không tìm thấy tác giả, vui lòng nhập lại hoặc chọn tác giả");
            }
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var record = (DauSach)dauSachDataGrid.SelectedItem;
            if (record != null)
            {
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
            if (!String.IsNullOrEmpty(txtMaTacGia1.Text))
            {
                var record = _tacgia.GetByID(int.Parse(txtMaTacGia1.Text));
                if (!WriteExcel(record))
                    MessageBox.Show("Lỗi xuất báo cáo");
            }
            else MessageBox.Show("Vui lòng nhập hoặc chọn đầu sách");
        }
        private bool WriteExcel(TacGia record)
        {
            try
            {
                COMExcel.Application exApp = new COMExcel.Application();
                string workbookPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resource\BCTacGia.xlsx");
                COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath,
                        0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
                exSheet.Activate();
                exSheet.Cells[3, 3] = record.MaTacGia.ToString();
                exSheet.Cells[4, 3] = record.NoiCongTac;
                exSheet.Cells[3, 5] = record.TenTacGia;
                exSheet.Cells[4, 5] = record.DauSach.Count;
                // Xuất danh sách
                int i = 1;
                foreach (DauSach item in record.DauSach)
                {
                    exSheet.Cells[7 + i, 1] = i;
                    exSheet.Cells[7 + i, 2] = item.MaDauSach;
                    exSheet.Cells[7 + i, 3] = item.TenDauSach;
                    exSheet.Cells[7 + i, 4] = item.LoaiSach.TenLoai;
                    exSheet.Cells[7 + i, 5] = item.NXB.TenNXB;
                    exSheet.Cells[7 + i, 6] = item.TrangThaiDauSach.TenTrangThai;
                    i++;
                }
                //
                i = i + 6;
                COMExcel.Range r = (COMExcel.Range)exSheet.get_Range("A8", "F" + i);
                r.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                r.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                // Lưu file
                SaveFileDialog dialog = new SaveFileDialog { FileName = "BCTacGia.xls", Filter = "Excel files|*.xls", DefaultExt = "xls", Title = "Chọn nơi lưu tệp báo cáo" };
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
                return false;
            }
        }
    }
}
