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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        MainWindow main = App.Current.MainWindow as MainWindow;
        public MainView()
        {
            InitializeComponent();
            LoadContent();
        }

        private void btnMain_Click(object sender, RoutedEventArgs e)
        {
            if (!main.btnChangeTheme.IsChecked.Value)
                main.btnChangeTheme.IsChecked = true;
            else main.btnChangeTheme.IsChecked = false;
        }
        private void LoadContent()
        {
            var dausach = new BizDauSach();
            var cuonsach = new BizCuonSach();
            var tacgia = new BizTacGia();
            var nxb = new BizNXB();
            var loai = new BizLoaiSach();
            var docgia = new BizDocGia();
            btnDauSach.Content += dausach.GetAll().Count.ToString();
            lblDuocMuon.Content += dausach.Search(null, null, null, null, 1).Count.ToString();
            lblDoc.Content += dausach.Search(null, null, null, null, 2).Count.ToString();
            lblPhoto.Content += dausach.Search(null, null, null, null, 3).Count.ToString();

            btnCuonSach.Content += cuonsach.GetAll().Count.ToString();
            lblConHang.Content += cuonsach.Search(null, 1).Count.ToString();
            lblHetHang.Content += cuonsach.Search(null, 2).Count.ToString();

            btnDocGia.Content += docgia.GetAll().Count.ToString();
            var now = DateTime.Now;
            lblHetHan.Content += docgia.Search(null, null, null, null, null, null, null, now).Count.ToString();
            lblVienChuc.Content += docgia.Search(null, null, null, null, null, true, null, null).Count.ToString();

            btnLoaiSach.Content += loai.GetAll().Count.ToString();

            btnTacGia.Content += tacgia.GetAll().Count.ToString();

            btnNXB.Content += nxb.GetAll().Count.ToString();
        }
        private void btnDauSach_Click(object sender, RoutedEventArgs e)
        {
            main.LoadDauSach();
        }

        private void btnCuonSach_Click(object sender, RoutedEventArgs e)
        {
            main.LoadCuonSach();
        }

        private void btnDocGia_Click(object sender, RoutedEventArgs e)
        {
            main.LoadDocGia();
        }

        private void btnLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            main.LoadLoaiSach();
        }

        private void btnTacGia_Click(object sender, RoutedEventArgs e)
        {
            main.LoadTacGia();
        }

        private void btnNXB_Click(object sender, RoutedEventArgs e)
        {
            main.LoadNXB();
        }

        private void btnTKCuonSach_Click(object sender, RoutedEventArgs e)
        {
            main.LoadTKCuonSach();
        }

        private void btnTKTacGia_Click(object sender, RoutedEventArgs e)
        {
            main.LoadTKTacGia();
        }

        private void btnTKDocGia_Click(object sender, RoutedEventArgs e)
        {
            main.LoadTKDocGia();
        }
    }
}
