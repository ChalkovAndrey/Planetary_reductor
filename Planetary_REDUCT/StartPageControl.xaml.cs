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
    /// Логика взаимодействия для StartPageControl.xaml
    /// </summary>
    public partial class StartPageControl : UserControl
    {
        public delegate void MainFunctions();
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

       
        public StartPageControl()
        {
            InitializeComponent();
           
           
        }
      
        private void PlanetaryCallClick (Object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.PlanetaryCall();
            

            
        }
        
    }
}
