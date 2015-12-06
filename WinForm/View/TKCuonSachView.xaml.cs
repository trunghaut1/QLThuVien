using Core.Dal;
using Core.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Text.RegularExpressions;
using FlatTheme.ControlStyle;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Reflection;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for TKCuonSachView.xaml
    /// </summary>
    public partial class TKCuonSachView : UserControl
    {
        BizDauSach _dausach = new BizDauSach();
        BizCuonSach _cuonsach = new BizCuonSach();
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
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void txtMaDauSach_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
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
                try
                {
                    COMExcel.Application exApp = new COMExcel.Application();
                    string workbookPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resource\BCCuonSach.xlsx");
                    COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath,
                            0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                            true, false, 0, true, false, false);
                    COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
                    exSheet.Activate();
                    exSheet.Cells[3, 2] = record.MaDauSach.ToString();
                    exSheet.Cells[4, 2] = record.TenDauSach;
                    exSheet.Cells[5, 2] = record.LoaiSach.TenLoai;
                    exSheet.Cells[6, 2] = record.NXB.TenNXB;
                    exSheet.Cells[7, 2] = record.TacGia.TenTacGia;
                    exSheet.Cells[8, 2] = record.CuonSach.Count;
                    exBook.Save();
                    exApp.Quit();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xuất báo cáo" + ex); }
            }
            else MessageBox.Show("Vui lòng nhập hoặc chọn đầu sách");
        }
    }
}
