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
                Content = new NXBView(),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontSize = 16,
                MinHeight = 577,
                MinWidth = 1164
            };
            window.Style = Application.Current.FindResource("FlatWindow") as Style;
            window.Closed += new EventHandler(AddWindow_Closed);
            window.ShowDialog();
        }
    }
}
