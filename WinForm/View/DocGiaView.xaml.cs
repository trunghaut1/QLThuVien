using Core.Dal;
using Core.Biz;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for DocGiaView.xaml
    /// </summary>
    public partial class DocGiaView : UserControl
    {
        BizDocGia db = new BizDocGia();
        public DocGiaView()
        {
            InitializeComponent();
            LoadList(null);
            LoadComboBox();
            LoadTime();
        }

        private void LoadList(List<DocGia> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["docGiaViewSource"];
            myCollectionViewSource.Source = value ?? db.GetAll();
        }
        private void LoadComboBox()
        {
            int year = DateTime.Now.Year;
            List<int> yearlist = new List<int>();
            for(int i = year - 2; i < year + 5; i++)
            {
                yearlist.Add(i);
            }
            cbxNamTotNghiep.ItemsSource = yearlist;
            cbxNamTotNghiepS.ItemsSource = yearlist;
        }
        private void LoadTime()
        {
            DateTime time = DateTime.Now;
            txtNgayCap.Text = time.ToShortDateString();
            time = new DateTime(time.Year + 1, time.Month, time.Day);
            txtNgayHetHan.Text = time.ToShortDateString();
        }

        private void docGiaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (DocGia)docGiaDataGrid.SelectedItem;
            if (record != null)
            {
                cbxNamTotNghiep.IsEnabled = true;
                txtMaDocGia.Text = record.MaDocGia.ToString();
                txtTenDocGia.Text = record.TenDocGia;
                txtDiaChi.Text = record.DiaChi;
                txtSDT.Text = record.SDT;
                txtEmail.Text = record.Email;
                chkVienChuc.IsChecked = record.VienChuc;
                if(record.VienChuc.Value) cbxNamTotNghiep.IsEnabled = false;
                cbxNamTotNghiep.SelectedValue = record.NamTotNgiep;
                txtNgayCap.Text = record.NgayCap.Value.ToShortDateString();
                txtNgayHetHan.Text = record.NgayHetHan.Value.ToShortDateString();
            }
        }
        private bool CheckNull()
        {
            if (String.IsNullOrEmpty(txtTenDocGia.Text))
            {
                MessageBox.Show("Vui lòng nhập tên độc giả");
                return false;
            }
            if (!String.IsNullOrEmpty(txtEmail.Text))
            {
                if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
                {
                    MessageBox.Show("Vui lòng nhập địa chỉ Email hợp lệ");
                    return false;
                }                   
            }
            if(!chkVienChuc.IsChecked.Value)
            {
                if(cbxNamTotNghiep.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn năm tốt nghiệp");
                    return false;
                }
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime time = DateTime.Now;
            if (!CheckNull()) return;
            var record = new DocGia()
            {
                TenDocGia = txtTenDocGia.Text,
                SDT = txtSDT.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text,
                NgayCap = time,
                NgayHetHan = new DateTime(time.Year + 1, time.Month, time.Day),
                VienChuc = chkVienChuc.IsChecked
            };
            if (!chkVienChuc.IsChecked.Value)
                record.NamTotNgiep = (int)cbxNamTotNghiep.SelectedValue;
            if (db.Add(record))
            {
                MessageBox.Show("Thêm thành công");
                btnResetS_Click(null, null);
                docGiaDataGrid.SelectedIndex = docGiaDataGrid.Items.Count - 1;
                docGiaDataGrid.ScrollIntoView(record);
            }
            else MessageBox.Show("Thêm thất bại");
        }

        private void chkVienChuc_Click(object sender, RoutedEventArgs e)
        {
            if(chkVienChuc.IsChecked.Value)
            {
                cbxNamTotNghiep.IsEnabled = false;
            }
            else cbxNamTotNghiep.IsEnabled = true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var record = (DocGia)docGiaDataGrid.SelectedItem;
            if (record != null)
            {
                if (!CheckNull()) return;
                record.TenDocGia = txtTenDocGia.Text;
                record.SDT = txtSDT.Text;
                record.Email = txtEmail.Text;
                record.DiaChi = txtDiaChi.Text;
                record.VienChuc = chkVienChuc.IsChecked;
                if (!chkVienChuc.IsChecked.Value)
                    record.NamTotNgiep = (int)cbxNamTotNghiep.SelectedValue;
                else record.NamTotNgiep = null;
                if (db.Update(record))
                {
                    MessageBox.Show("Cập nhật thành công");
                    btnResetS_Click(null, null);
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            else MessageBox.Show("Vui lòng chọn độc giả");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenDocGia.Text = null;
            txtSDT.Text = null;
            txtEmail.Text = null;
            txtDiaChi.Text = null;
            chkVienChuc.IsChecked = false;
            cbxNamTotNghiep.SelectedIndex = -1;
            cbxNamTotNghiep.IsEnabled = true;
        }
        private void Search()
        {
            int? nam = null;
            bool? vienchuc = null;
            if (chkVienChucS.IsChecked.Value) vienchuc = true;
            if (cbxNamTotNghiepS.SelectedIndex != -1)
                nam = (int)cbxNamTotNghiepS.SelectedValue;
            List<DocGia> record = db.Search(txtTenDocGiaS.Text, txtSDTS.Text, null, null, nam, vienchuc, dtNgayCapS.SelectedDate, dtNgayHetHanS.SelectedDate);
            LoadList(record);
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void cbxNamTotNghiepS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chkVienChucS.IsChecked.Value && cbxNamTotNghiepS.SelectedIndex != -1) chkVienChucS.IsChecked = false;
            Search();
        }

        private void Search_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void chkVienChucS_Click(object sender, RoutedEventArgs e)
        {
            if (cbxNamTotNghiepS.SelectedIndex != -1) cbxNamTotNghiepS.SelectedIndex = -1;
            Search();
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            var record = docGiaDataGrid.SelectedItem;
            txtTenDocGiaS.Text = null;
            txtSDTS.Text = null;
            if(cbxNamTotNghiepS.SelectedIndex != -1) cbxNamTotNghiepS.SelectedIndex = -1;
            if(chkVienChucS.IsChecked.Value) chkVienChucS.IsChecked = false;
            dtNgayCapS.SelectedDate = null;
            dtNgayHetHanS.SelectedDate = null;
            LoadList(null);
            if (record != null)
            {
                docGiaDataGrid.SelectedItem = record;
                docGiaDataGrid.ScrollIntoView(record);
            }
        }

        private void txtSDTS_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 ||
                key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
        }

        private void txtSDT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 ||
                key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
        }
    }
}
