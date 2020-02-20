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
    /// Логика взаимодействия для PlanetaryPage.xaml
    /// </summary>
    public partial class PlanetaryPage : UserControl
    {
        public delegate void MainFunctions();
        Test test { get;set; }
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public PlanetaryPage()
        {
            InitializeComponent();
            test = new Test()
            {
               
            };
           
            this.DataContext = test;
        }
       void StartPageClick (Object sender,EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.StartPageCall();
        }
        public void CalculatingClick(Object sender,EventArgs e)
        {
           
            Zymin.Text = test.test1;
            
        }
    }
    class Test
    {
        public string test1 { get; set; }
        public string test2;
    }
}
