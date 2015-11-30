using Core.Dal;
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
    /// Interaction logic for CTDauSachView.xaml
    /// </summary>
    public partial class CTDauSachView : UserControl
    {
        public CTDauSachView()
        {
            InitializeComponent();
        }
        public CTDauSachView(DauSach value)
        {
            InitializeComponent();
            lblMaDauSach.Content = value.MaDauSach;
            lblTenDauSach.Content = value.TenDauSach;
            lblMaLoai.Content = value.LoaiSach.TenLoai;
            lblMaNXB.Content = value.NXB.TenNXB;
            lblMaTacGia.Content = value.TacGia.TenTacGia;
            lblMaTrangThai.Content = value.TrangThaiDauSach.TenTrangThai;
            lblTomTat.Text = value.TomTat;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }
    }
}
