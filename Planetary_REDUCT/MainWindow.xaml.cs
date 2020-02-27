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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
       
        public MainWindow()
        {
            InitializeComponent();
            
            
            
        }
        
        public void PlanetaryCall()
        {
            PlanetaryPage.Visibility = Visibility.Visible;
        }
        public void StartPageCall()
        {
            StartPage.Visibility = Visibility.Visible;
        }
        public void WaveCall()
        {
            WavePage.Visibility = Visibility.Visible;
        }
        

    }
}
