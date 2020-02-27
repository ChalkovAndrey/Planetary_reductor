using System;
using System.IO;
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

namespace Planetary_REDUCT
{
    /// <summary>
    /// Логика взаимодействия для WavePage.xaml
    /// </summary>
    public partial class WavePage : UserControl
    {
      public Wave wave;
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public WavePage()
        {
            InitializeComponent();
            wave = new Wave { };
            this.DataContext = wave;
        }
       private void CalculatingClick(Object sender, RoutedEventArgs e)
        {
            wave.Construction();
            InputGrid.Visibility = Visibility.Collapsed;
            OutScreenPage outScreenPage = (OutScreenPage)OutputGrid.Children[0];
            outScreenPage.LoadData(wave);
          OutputGrid.Visibility = Visibility.Visible;
            wave.ResetData();
            //wave = new Wave { };
            //this.DataContext = wave;
           // MessageBox.Show("Zf =" +wave.Zf.ToString() + "Zc = " + wave.Zc.ToString() + "Ngp = " +wave.Ngp.ToString() + "Modul = " + wave.modulfc);
        }
        void StartPageClick(Object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.StartPageCall();
        }

       
    }
}
