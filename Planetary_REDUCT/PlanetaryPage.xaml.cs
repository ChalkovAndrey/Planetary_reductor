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
        OutScreenPage outScreenPage;
        DBHelper dB;
        public delegate void MainFunctions();
        Planet planet { get; set; }
        public MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
        public PlanetaryPage()
        {
            InitializeComponent();
            outScreenPage = (OutScreenPage)OutputGrid.Children[0];
            planet = new Planet()
            {

            };
            dB = new DBHelper();

            dB.CreateTable();
            outScreenPage.SetTable(dB.Table);
            this.DataContext = planet;
        }
        void StartPageClick(Object sender, EventArgs e)
        {
            Visibility = Visibility.Collapsed;
            mainWindow.StartPageCall();
        }
        public void CalculatingClick(Object sender, EventArgs e)
        {
            planet.ZTMM46();


            dB.InsertParams(Planet.Result);
            outScreenPage.SetTable(dB.Table);
            dB.UpdateDB();

            InputGrid.Visibility = Visibility.Collapsed;


            OutputGrid.Visibility = Visibility.Visible;



            Planet.Result.Clear();
        }

        private void ExampleClick(object sender, RoutedEventArgs e)
        {

            planet.SetExample();
        }

        private void ltr_Checked(object sender, RoutedEventArgs e)
        {
            planet.LTR = true;
        }

        private void ltr_Unchecked(object sender, RoutedEventArgs e)
        {
            planet.LTR = false;
        }
    }

}
