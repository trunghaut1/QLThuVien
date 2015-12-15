using Core.Biz;
using Core.Dal;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for TacGiaView.xaml
    /// </summary>
    public partial class TacGiaView : UserControl
    {
        BizTacGia db = new BizTacGia();
        public TacGiaView()
        {
            InitializeComponent();
            LoadList(null);
        }
        public TacGiaView(bool child)
        {
            InitializeComponent();
            LoadList(null);
            if (child) btnClose2.Visibility = Visibility.Visible;
        }

        private void LoadList(List<TacGia> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["tacGiaViewSource"];
            myCollectionViewSource.Source = value ?? db.GetAll();
        }

        private void tacGiaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (TacGia)tacGiaDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaTacGia.Text = record.MaTacGia.ToString();
                txtTenTacGia.Text = record.TenTacGia;
                txtNoiCongTac.Text = record.NoiCongTac;
            }
        }
        private bool CheckNull()
        {
            if (String.IsNullOrEmpty(txtTenTacGia.Text))
            {
                MessageBox.Show("Vui lòng nhập tên tác giả");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new TacGia()
            {
                TenTacGia = txtTenTacGia.Text,
                NoiCongTac = txtNoiCongTac.Text
            };
            if (db.Add(record))
            {
                MessageBox.Show("Thêm thành công");
                btnResetS_Click(null, null);
                tacGiaDataGrid.SelectedIndex = tacGiaDataGrid.Items.Count - 1;
                tacGiaDataGrid.ScrollIntoView(record);
                btnAdd.IsEnabled = false;
            }
            else MessageBox.Show("Thêm thất bại");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var record = (TacGia)tacGiaDataGrid.SelectedItem;
            if (record != null)
            {
                if (!CheckNull()) return;
                record.TenTacGia = txtTenTacGia.Text;
                record.NoiCongTac = txtNoiCongTac.Text;
                if (db.Update(record))
                {
                    MessageBox.Show("Cập nhật thành công");
                    btnResetS_Click(null, null);
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            else MessageBox.Show("Vui lòng chọn tác giả");
        }
        private void ResetAll()
        {
            txtMaTacGia.Text = null;
            txtTenTacGia.Text = null;
            txtNoiCongTac.Text = null;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var record = (TacGia)tacGiaDataGrid.SelectedItem;
            if (record != null)
            {
                MessageBoxResult comf = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (!db.CheckFK(record.MaTacGia)) MessageBox.Show("Tồn tại đầu sách mang tác giả này, không thể xóa");
                else
                {
                    if (db.Delete(record.MaTacGia))
                    {
                        MessageBox.Show("Xóa thành công");
                        btnResetS_Click(null, null);
                        ResetAll();
                    }
                    else MessageBox.Show("Xóa thất bại");
                }
            }
            else MessageBox.Show("Vui lòng chọn tác giả");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtNoiCongTac.Text = null;
            txtTenTacGia.Text = null;
            txtMaTacGia.Text = null;
            btnAdd.IsEnabled = true;
        }
        private void Search()
        {
            List<TacGia> record = db.Search(txtTenTacGiaS.Text, txtNoiCongTacS.Text);
            LoadList(record);
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            var record = tacGiaDataGrid.SelectedItem;
            txtNoiCongTacS.Text = null;
            txtTenTacGiaS.Text = null;
            LoadList(null);
            if (record != null)
            {
                tacGiaDataGrid.SelectedItem = record;
                tacGiaDataGrid.ScrollIntoView(record);
            }
        }

        private void btnClose2_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }
    }
}
