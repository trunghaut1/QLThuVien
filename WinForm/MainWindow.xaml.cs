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
    }
}
