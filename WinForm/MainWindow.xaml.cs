using FlatTheme.Code;
using FlatTheme.ControlStyle;
using System;
using System.Windows;
using WinForm.View;

namespace WinForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FlatWindow
    {
        string _title = " - QUẢN LÝ THƯ VIỆN";
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChangeTheme_Checked(object sender, RoutedEventArgs e)
        {
            ChangeTheme.Change("MaterialDark");
        }

        private void btnChangeTheme_Unchecked(object sender, RoutedEventArgs e)
        {
            ChangeTheme.Change("MaterialLight");
        }

        private void mnLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new LoaiSachView());
            iHome.SetResourceReference(StyleProperty, "LoaiSachIcon");
            this.Title = "LOẠI SÁCH" + _title;
        }

        private void mnTacGia_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new TacGiaView());
            iHome.SetResourceReference(StyleProperty, "TacGiaIcon");
            this.Title = "TÁC GIẢ" + _title;
        }

        private void mnNXB_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new NXBView());
            iHome.SetResourceReference(StyleProperty, "NXBIcon");
            this.Title = "NHÀ XUẤT BẢN" + _title;
        }

        private void mnDauSach_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new DauSachView());
            iHome.SetResourceReference(StyleProperty, "DauSachIcon");
            this.Title = "ĐẦU SÁCH" + _title;
        }

        private void mnCuonSach_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new CuonSachView());
            iHome.SetResourceReference(StyleProperty, "CuonSachIcon");
            this.Title = "CUỐN SÁCH" + _title;
        }

        private void mnDocGia_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new DocGiaView());
            iHome.SetResourceReference(StyleProperty, "DocGiaIcon");
            this.Title = "ĐỘC GIẢ" + _title;
        }

        private void mnTKCuonSach_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new TKCuonSachView());
            iHome.SetResourceReference(StyleProperty, "ThongKeIcon");
            this.Title = "THỐNG KÊ SỐ CUỐN SÁCH" + _title;
        }
    }
}
