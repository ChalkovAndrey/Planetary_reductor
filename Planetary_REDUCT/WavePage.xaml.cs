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

namespace Planetary_REDUCT
{
    /// <summary>
    /// Логика взаимодействия для WavePage.xaml
    /// </summary>
    public partial class WavePage : UserControl
    {
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public WavePage()
        {
            InitializeComponent();
        }
       private void CalculatingClick(Object sender, RoutedEventArgs e)
        {

        }
        void StartPageClick(Object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.StartPageCall();
        }
    }
}
