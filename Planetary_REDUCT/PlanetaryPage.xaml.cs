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
        Planet planet { get;set; }
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public PlanetaryPage()
        {
            InitializeComponent();
            planet = new Planet()
            {
               
            };
           
            this.DataContext = planet;
        }
       void StartPageClick (Object sender,EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.StartPageCall();
        }
        public void CalculatingClick(Object sender, EventArgs e)
        {
            
            planet.ZTMM46();
            DBHelper dB = new DBHelper();
            dB.CreateTable();
            dB.InsertParams(Planet.Result);
            dB.UpdateDB();
            InputGrid.Visibility = Visibility.Collapsed;
            OutputGrid.Visibility = Visibility.Visible;
           
            OutScreenPage outScreenPage = (OutScreenPage)OutputGrid.Children[0];
            outScreenPage.SetTable(dB.Table);
            Planet.Result.Clear();



        }
    }
   
}
