﻿using Core.Dal;
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

namespace WinForm.View
{
    /// <summary>
    /// Interaction logic for LoaiSachView.xaml
    /// </summary>
    public partial class LoaiSachView : UserControl
    {
        BizLoaiSach db = new BizLoaiSach();
        public LoaiSachView()
        {
            InitializeComponent();
            LoadList();
        }

        private void LoadList()
        {
            CollectionViewSource myCollectionViewSource = (CollectionViewSource)this.Resources["loaiSachViewSource"];
            myCollectionViewSource.Source = db.GetAll();
        }

        private void loaiSachDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var record = (LoaiSach)loaiSachDataGrid.SelectedItem;
            if(record != null)
            {
                txtMaLoai.Text = record.MaLoai.ToString();
                txtTenLoai.Text = record.TenLoai.ToString();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtTenLoai.Text = null;
        }
        private bool CheckNull()
        {
            if(String.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckNull()) return;
            var record = new LoaiSach()
            {
                TenLoai = txtTenLoai.Text
            };
            if(db.Add(record))
            {
                MessageBox.Show("Thêm thành công");
                LoadList();
                loaiSachDataGrid.SelectedIndex = loaiSachDataGrid.Items.Count - 1;
                loaiSachDataGrid.ScrollIntoView(record);
            }
            else MessageBox.Show("Thêm thất bại");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var record = (LoaiSach)loaiSachDataGrid.SelectedItem;
            if(record != null)
            {
                if (!CheckNull()) return;
                int index = loaiSachDataGrid.SelectedIndex;
                record.TenLoai = txtTenLoai.Text;
                if(db.Update(record))
                {
                    MessageBox.Show("Cập nhật thành công");
                    LoadList();
                    loaiSachDataGrid.SelectedIndex = index;
                    loaiSachDataGrid.ScrollIntoView(record);
                }
                else MessageBox.Show("Cập nhật thất bại");
            }
            else MessageBox.Show("Vui lòng chọn loại sách");
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var record = (LoaiSach)loaiSachDataGrid.SelectedItem;
            if(record != null)
            {
                MessageBoxResult comf = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButton.YesNo);
                if(!db.CheckFK(record.MaLoai)) MessageBox.Show("Tồn tại đầu sách mang loại này, không thể xóa");
                else
                {
                    if(db.Delete(record.MaLoai))
                    {
                        MessageBox.Show("Xóa thành công");
                        LoadList();
                        ResetAll();
                    }
                    else MessageBox.Show("Xóa thất bại");
                }
            }
            else MessageBox.Show("Vui lòng chọn loại sách");
        }
        private void ResetAll()
        {
            txtMaLoai.Text = null;
            txtTenLoai.Text = null;
        }
    }
}
