using Core.Biz;
using Core.Dal;
using FlatTheme.ControlStyle;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for CuonSachView.xaml
    /// </summary>
    public partial class CuonSachView : UserControl
    {
        BizCuonSach db = new BizCuonSach();
        BizDauSach _dausach = new BizDauSach();
        BizTinhTrangCuonSach _tinhtrang = new BizTinhTrangCuonSach();
        public CuonSachView()
        {
            InitializeComponent();
            LoadTinhTrang();
            LoadComboBox();
            LoadSearch();
            LoadList(null);            
        }

        private void LoadList(List<CuonSach> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["cuonSachViewSource"];
            myCollectionViewSource.Source = value ?? db.GetAll();
        }
        private void LoadTinhTrang()
        {
            cbxMaTinhTrang.ItemsSource = _tinhtrang.GetAll();
            cbxMaTinhTrangS.ItemsSource = _tinhtrang.GetAll();
        }
        private void LoadComboBox()
        {
            cbxMaDauSach.ItemsSource = _dausach.GetAll();
        }
        private void LoadSearch()
        {
            cbxMaDauSachS.ItemsSource = _dausach.GetAll();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadSearch();
        }

        private void btnAddDauSach_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "QUẢN LÝ ĐẦU SÁCH",
                Name = "DauSach",
                Content = new DauSachView(true),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16,
                MinHeight = 577,
                MinWidth = 1164
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }

        private void cuonSachDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (CuonSach)cuonSachDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaCuonSach.Text = record.MaCuonSach.ToString();
                cbxMaTinhTrang.SelectedValue = record.MaTinhTrang;
                cbxMaDauSach.SelectedValue = record.MaDauSach;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cbxMaDauSach.SelectedIndex = -1;
            cbxMaTinhTrang.SelectedIndex = -1;
            txtMaCuonSach.Text = null;
            btnAdd.IsEnabled = true;
        }
        private bool CheckNull()
        {
            if (cbxMaDauSach.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đầu sách");
                return false;
            }
            if (cbxMaTinhTrang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tình trạng");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new CuonSach()
            {
                MaDauSach = (int)cbxMaDauSach.SelectedValue,
                MaTinhTrang = (int)cbxMaTinhTrang.SelectedValue
            };
            if (db.Add(record))
            {
                MessageBox.Show("Thêm thành công");
                btnResetS_Click(null, null);
                cuonSachDataGrid.SelectedIndex = cuonSachDataGrid.Items.Count - 1;
                cuonSachDataGrid.ScrollIntoView(record);
                btnAdd.IsEnabled = false;
            }
            else MessageBox.Show("Thêm thất bại");
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            var record = cuonSachDataGrid.SelectedItem;
            cbxMaDauSachS.SelectedIndex = -1;
            cbxMaTinhTrangS.SelectedIndex = -1;
            LoadList(null);
            if(record != null)
            {
                cuonSachDataGrid.SelectedItem = record;
                cuonSachDataGrid.ScrollIntoView(record);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var record = (CuonSach)cuonSachDataGrid.SelectedItem;
            if (record != null)
            {
                if (!CheckNull()) return;
                record.MaDauSach = (int)cbxMaDauSach.SelectedValue;
                record.MaTinhTrang = (int)cbxMaTinhTrang.SelectedValue;
                if (db.Update(record))
                {
                    MessageBox.Show("Cập nhật thành công");
                    btnResetS_Click(null, null);
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            else MessageBox.Show("Vui lòng chọn cuốn sách");
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {
            var record = (DauSach)cbxMaDauSach.SelectedItem;
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
            else MessageBox.Show("Vui lòng chọn đầu sách");
        }
        private void Search()
        {
            int? dausach = null, tinhtrang = null;
            if (cbxMaDauSachS.SelectedIndex != -1)
                dausach = (int)cbxMaDauSachS.SelectedValue;
            if (cbxMaTinhTrangS.SelectedIndex != -1)
                tinhtrang = (int)cbxMaTinhTrangS.SelectedValue;
            List<CuonSach> record = db.Search(dausach, tinhtrang);
            LoadList(record);
        }

        private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }
    }
}
