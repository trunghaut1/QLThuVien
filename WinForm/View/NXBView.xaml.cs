using Core.Dal;
using Core.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for NXBView.xaml
    /// </summary>
    public partial class NXBView : UserControl
    {
        BizNXB db = new BizNXB();
        public NXBView()
        {
            InitializeComponent();
            LoadList(null);
        }
        public NXBView(bool child)
        {
            InitializeComponent();
            LoadList(null);
            if(child) btnClose2.Visibility = Visibility.Visible;
        }

        private void LoadList(List<NXB> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["nXBViewSource"];
            myCollectionViewSource.Source = value ?? db.GetAll();
        }

        private void nXBDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (NXB)nXBDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaNXB.Text = record.MaNXB.ToString();
                txtTenNXB.Text = record.TenNXB;
                txtSDT.Text = record.SDT;
                txtDiaChi.Text = record.DiaChi;
            }
        }
        private bool CheckNull()
        {
            if (String.IsNullOrEmpty(txtTenNXB.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà xuất bản");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new NXB()
            {
                TenNXB = txtTenNXB.Text,
                SDT = txtSDT.Text,
                DiaChi = txtDiaChi.Text
            };
            if (db.Add(record))
            {
                MessageBox.Show("Thêm thành công");
                btnResetS_Click(null, null);
                nXBDataGrid.SelectedIndex = nXBDataGrid.Items.Count - 1;
                nXBDataGrid.ScrollIntoView(record);
            }
            else MessageBox.Show("Thêm thất bại");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var record = (NXB)nXBDataGrid.SelectedItem;
            if (record != null)
            {
                if (!CheckNull()) return;
                record.TenNXB = txtTenNXB.Text;
                record.SDT = txtSDT.Text;
                record.DiaChi = txtDiaChi.Text;
                if (db.Update(record))
                {
                    MessageBox.Show("Cập nhật thành công");
                    btnResetS_Click(null, null);
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            else MessageBox.Show("Vui lòng chọn nhà xuất bản");
        }
        private void ResetAll()
        {
            txtMaNXB.Text = null;
            txtTenNXB.Text = null;
            txtSDT.Text = null;
            txtDiaChi.Text = null;
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var record = (NXB)nXBDataGrid.SelectedItem;
            if (record != null)
            {
                MessageBoxResult comf = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if (!db.CheckFK(record.MaNXB)) MessageBox.Show("Tồn tại đầu sách mang nhà xuất bản này, không thể xóa");
                else
                {
                    if (db.Delete(record.MaNXB))
                    {
                        MessageBox.Show("Xóa thành công");
                        btnResetS_Click(null, null);
                        ResetAll();
                    }
                    else MessageBox.Show("Xóa thất bại");
                }
            }
            else MessageBox.Show("Vui lòng chọn nhà xuất bản");
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenNXB.Text = null;
            txtSDT.Text = null;
            txtDiaChi.Text = null;
        }
        private void Search()
        {
            List<NXB> record = db.Search(txtTenNXBS.Text, txtSDTS.Text, txtDiaChiS.Text);
            LoadList(record);
        }
        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            var record = nXBDataGrid.SelectedItem;
            txtTenNXBS.Text = null;
            txtSDTS.Text = null;
            txtDiaChiS.Text = null;
            LoadList(null);
            if (record != null)
            {
                nXBDataGrid.SelectedItem = record;
                nXBDataGrid.ScrollIntoView(record);
            }
        }

        private void btnClose2_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        private void txtSDT_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 ||
                key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
        }

        private void txtSDTS_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 ||
                key == 2 || key == 6 || key == 23 || key == 25 || key == 32);
        }
    }
}
