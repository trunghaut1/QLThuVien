﻿using Core.Dal;
using Core.Biz;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using FlatTheme.ControlStyle;

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for DauSachView.xaml
    /// </summary>
    public partial class DauSachView : UserControl
    {
        BizDauSach db = new BizDauSach();
        BizNXB _nxb = new BizNXB();
        BizLoaiSach _loai = new BizLoaiSach();
        BizTacGia _tacgia = new BizTacGia();
        BizTrangThaiDauSach _trangthai = new BizTrangThaiDauSach();
        public DauSachView()
        {
            InitializeComponent();
            LoadComboBox(true,true,true);
            LoadSearch(true, true, true);
            LoadTrangThai();
            LoadList(null);
        }
        public DauSachView(bool child)
        {
            InitializeComponent();
            LoadComboBox(true, true, true);
            LoadSearch(true, true, true);
            LoadTrangThai();
            LoadList(null);
            if (child) btnClose2.Visibility = Visibility.Visible;
        }

        private void LoadList(List<DauSach> value)
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["dauSachViewSource"];
            myCollectionViewSource.Source = value ?? db.GetAll();
        }
        private void LoadTrangThai()
        {
            cbxMaTrangThai.ItemsSource = _trangthai.GetAll();
            cbxMaTrangThaiS.ItemsSource = _trangthai.GetAll();
        }
        private void LoadComboBox(bool loai, bool nxb, bool tacgia)
        {
            if(loai) cbxMaLoai.ItemsSource = _loai.GetAll();
            if(nxb) cbxMaNXB.ItemsSource = _nxb.GetAll();
            if(tacgia) cbxMaTacGia.ItemsSource = _tacgia.GetAll();
        }
        private void LoadSearch(bool loai, bool nxb, bool tacgia)
        {
            if (loai) cbxMaLoaiS.ItemsSource = _loai.GetAll();
            if (nxb) cbxMaNXBS.ItemsSource = _nxb.GetAll();
            if (tacgia) cbxMaTacGiaS.ItemsSource = _tacgia.GetAll();
        }
        private void AddWindow_Closed(object sender, EventArgs e)
        {
            FlatWindow window = (FlatWindow)sender;
            if (window.Name == "LoaiSach")
            {
                LoadComboBox(true,false,false);
                LoadSearch(true, false, false);
            }
            if (window.Name == "NXB")
            {
                LoadComboBox(false,true, true);
                LoadSearch(false, true, true);
            }
            if (window.Name == "TacGia")
            {
                LoadComboBox(false,false, true);
                LoadSearch(false, false, true);
            }
        }

        private void btnAddLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "QUẢN LÝ LOẠI SÁCH",
                Name = "LoaiSach",
                Content = new LoaiSachView(true),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16,
                MinHeight = 577,
                MinWidth = 1164
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }

        private void btnAddNXB_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "QUẢN LÝ NHÀ XUẤT BẢN",
                Name = "NXB",
                Content = new NXBView(true),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16,
                MinHeight = 577,
                MinWidth = 1164
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }

        private void btnAddTacGia_Click(object sender, RoutedEventArgs e)
        {
            FlatWindow window = new FlatWindow()
            {
                Title = "QUẢN LÝ TÁC GIẢ",
                Name = "TacGia",
                Content = new TacGiaView(true),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16,
                MinHeight = 577,
                MinWidth = 1164
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }

        private void dauSachDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (DauSach)dauSachDataGrid.SelectedItem;
            if (record != null)
            {
                txtMaDauSach.Text = record.MaDauSach.ToString();
                txtTenDauSach.Text = record.TenDauSach;
                txtTomTat.Text = record.TomTat;
                cbxMaLoai.SelectedValue = record.MaLoai;
                cbxMaNXB.SelectedValue = record.MaNXB;
                cbxMaTacGia.SelectedValue = record.MaTacGia;
                cbxMaTrangThai.SelectedValue = record.MaTrangThai;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenDauSach.Text = null;
            txtTomTat.Text = null;
            cbxMaLoai.SelectedIndex = -1;
            cbxMaNXB.SelectedIndex = -1;
            cbxMaTacGia.SelectedIndex = -1;
            cbxMaTrangThai.SelectedIndex = -1;
            txtMaDauSach.Text = null;
            btnAdd.IsEnabled = true;
        }
        private bool CheckNull()
        {
            if (String.IsNullOrEmpty(txtTenDauSach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đầu sách");
                return false;
            }
            if(cbxMaLoai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại sách");
                return false;
            }
            if (cbxMaNXB.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà xuất bản");
                return false;
            }
            if (cbxMaTacGia.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn tác giả");
                return false;
            }
            if (cbxMaTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new DauSach()
            {
                TenDauSach = txtTenDauSach.Text,
                MaLoai = (int)cbxMaLoai.SelectedValue,
                MaNXB = (int)cbxMaNXB.SelectedValue,
                MaTacGia = (int)cbxMaTacGia.SelectedValue,
                MaTrangThai = (int)cbxMaTrangThai.SelectedValue,
                TomTat = txtTomTat.Text
            };
            if (db.Add(record))
            {
                MessageBox.Show("Thêm thành công");
                btnResetS_Click(null, null);
                dauSachDataGrid.SelectedIndex = dauSachDataGrid.Items.Count - 1;
                dauSachDataGrid.ScrollIntoView(record);
                btnAdd.IsEnabled = false;
            }
            else MessageBox.Show("Thêm thất bại");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var record = (DauSach)dauSachDataGrid.SelectedItem;
            if (record != null)
            {
                if (!CheckNull()) return;
                record.TenDauSach = txtTenDauSach.Text;
                record.MaLoai = (int)cbxMaLoai.SelectedValue;
                record.MaNXB = (int)cbxMaNXB.SelectedValue;
                record.MaTacGia = (int)cbxMaTacGia.SelectedValue;
                record.MaTrangThai = (int)cbxMaTrangThai.SelectedValue;
                record.TomTat = txtTomTat.Text;
                if (db.Update(record))
                {
                    MessageBox.Show("Cập nhật thành công");
                    btnResetS_Click(null, null);
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            else MessageBox.Show("Vui lòng chọn đầu sách");
        }
        private void Search()
        {
            int? maloai = null, nxb = null, tacgia = null, trangthai = null;
            if (cbxMaLoaiS.SelectedIndex != -1)
                maloai = (int)cbxMaLoaiS.SelectedValue;
            if (cbxMaNXBS.SelectedIndex != -1)
                nxb = (int)cbxMaNXBS.SelectedValue;
            if (cbxMaTacGiaS.SelectedIndex != -1)
                tacgia = (int)cbxMaTacGiaS.SelectedValue;
            if (cbxMaTrangThaiS.SelectedIndex != -1)
                trangthai = (int)cbxMaTrangThaiS.SelectedValue;
            List<DauSach> record = db.Search(txtTenDauSachS.Text, maloai, nxb, tacgia, trangthai);
            LoadList(record);
        }
        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            Search();
        }

        private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void btnResetS_Click(object sender, RoutedEventArgs e)
        {
            var record = dauSachDataGrid.SelectedItem;
            txtTenDauSachS.Text = null;
            cbxMaLoaiS.SelectedIndex = -1;
            cbxMaNXBS.SelectedIndex = -1;
            cbxMaTacGiaS.SelectedIndex = -1;
            cbxMaTrangThaiS.SelectedIndex = -1;
            LoadList(null);
            if (record != null)
            {
                dauSachDataGrid.SelectedItem = record;
                dauSachDataGrid.ScrollIntoView(record);
            }
        }

        private void btnClose2_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }
    }
}
