using Core.Biz;
using WinForm.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Core.Dal;
using COMExcel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using Microsoft.Win32;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for TKDocGiaView.xaml
    /// </summary>
    public partial class TKDocGiaView : UserControl
    {
        BizDocGia db = new BizDocGia();
        public TKDocGiaView()
        {
            InitializeComponent();
        }

        private void cbxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                switch (cbxType.SelectedIndex)
                {
                    case 0:
                        {
                            txtKey.IsEnabled = false;
                            break;
                        }
                    case 1:
                        {
                            txtKey.IsEnabled = true;
                            break;
                        }
                    case 2:
                        {
                            txtKey.IsEnabled = true;
                            break;
                        }
                }
                txtKey.Text = null;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cbxType.SelectedIndex = 0;
            txtKey.Text = null;
            chkHetHan.IsChecked = false;
            chkVienChuc.IsChecked = false;
            docGiaDataGrid.ItemsSource = null;
        }

        private void txtKey_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(cbxType.SelectedIndex == 1)
            {
                int key = (int)e.Key;
                e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 ||
                    key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            var record = db.GetAll();
            if(cbxType.SelectedIndex == 1)
            {
                if(string.IsNullOrEmpty(txtKey.Text))
                {
                    MessageBox.Show("Vui lòng nhập mã độc giả");
                    return;
                }
                record = record.Where(r => r.MaDocGia == int.Parse(txtKey.Text)).ToList();
            }
            if(cbxType.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(txtKey.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên độc giả");
                    return;
                }
                record = record.Where(r => r.TenDocGia.Contains(txtKey.Text)).ToList();
            }
            if(chkHetHan.IsChecked.Value)
            {
                var now = DateTime.Now;
                record = record.Where(r => r.NgayHetHan < now).ToList();
            }
            if(chkVienChuc.IsChecked.Value)
            {
                record = record.Where(r => r.VienChuc.Value == true).ToList();
            }
            if (record.Count > 0)
            {
                List<TKDocGia> value = new List<TKDocGia>();
                foreach(var row in record)
                {
                    var item = new TKDocGia(row);
                    item.DangMuon = DangMuonCout(row);
                    value.Add(item);                  
                }
                docGiaDataGrid.ItemsSource = value;
            }
            else MessageBox.Show("Không tìm thấy độc giả");
        }
        private int DangMuonCout(DocGia value)
        {
            int number = 0;
            foreach(var muon in value.PhieuMuon)
            {
                foreach(var ct in muon.CTPhieuMuon)
                {
                    if (!ct.DaTra.Value)
                        number++;
                }
            }
            return number;
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            if (docGiaDataGrid.Items.Count > 0)
            {
                if (!WriteExcel())
                    MessageBox.Show("Lỗi xuất báo cáo");
            }
            else MessageBox.Show("Không có độc giả để xuất báo cáo");
        }
        private bool WriteExcel()
        {
            try
            {
                COMExcel.Application exApp = new COMExcel.Application();
                string workbookPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Resource\BCDocGia.xlsx");
                COMExcel.Workbook exBook = exApp.Workbooks.Open(workbookPath,
                        0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "",
                        true, false, 0, true, false, false);
                COMExcel.Worksheet exSheet = (COMExcel.Worksheet)exBook.Worksheets[1];
                ((Microsoft.Office.Interop.Excel._Worksheet)exSheet).Activate();
                // Xuất danh sách
                int i = 1;
                TKDocGia record = new TKDocGia();
                foreach(var item in docGiaDataGrid.Items)
                {
                    record = (TKDocGia)item;
                    exSheet.Cells[3 + i, 1] = i;
                    exSheet.Cells[3 + i, 2] = record.MaDocGia;
                    exSheet.Cells[3 + i, 3] = record.TenDocGia;
                    exSheet.Cells[3 + i, 4] = "'" + record.SDT;
                    exSheet.Cells[3 + i, 5] = record.Email;
                    exSheet.Cells[3 + i, 6] = record.NgayCap.Value.ToShortDateString();
                    exSheet.Cells[3 + i, 7] = record.NgayHetHan.Value.ToShortDateString();
                    exSheet.Cells[3 + i, 8] = record.NamTotNgiep;
                    if (record.VienChuc.Value)
                        exSheet.Cells[3 + i, 9] = "x";
                    exSheet.Cells[3 + i, 10] = record.DiaChi;
                    exSheet.Cells[3 + i, 11] = record.DangMuon;
                    i++;
                }
                i = i + 2;
                COMExcel.Range r = (COMExcel.Range)exSheet.get_Range("A4", "K" + i);
                r.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                r.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                // Lưu file
                SaveFileDialog dialog = new SaveFileDialog { FileName = "BCDocGia.xls", Filter = "Excel files|*.xls", DefaultExt = "xls", Title = "Chọn nơi lưu tệp báo cáo" };
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
    }
}
