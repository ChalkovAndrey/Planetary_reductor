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
        Planet test { get;set; }
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public PlanetaryPage()
        {
            InitializeComponent();
            test = new Planet()
            {
               
            };
           
            this.DataContext = test;
        }
       void StartPageClick (Object sender,EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.StartPageCall();
        }
        public void CalculatingClick(Object sender, EventArgs e)
        {
            MessageBox.Show(test.ZaMin.ToString());
            test.ZTMM46();
            MessageBox.Show("Za = " + test.Za.ToString() + " Zb = " + test.Zb.ToString() + " /n Zg = " + test.Zg.ToString() + " Zf = " + test.Zf.ToString() + " N = " + test.N.ToString());
            test = new Planet();
            this.DataContext = test;

        }
    }
   
}
