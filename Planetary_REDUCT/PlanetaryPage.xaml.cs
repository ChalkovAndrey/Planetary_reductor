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
            if (planet.M1 > 1 || planet.M2 > 1 || planet.ZgMin < 8 || planet.ZfMin < 8 || planet.ZaMin < 8 || planet.UT > 16 || planet.UT < 4 || planet.ag < 10 || planet.ag > 600 || planet.CZ > 1 || planet.CZ < 0.1 || planet.NMin < 1 || planet.NMax > 11)
            {
                MessageBox.Show("Данные введены неверно.");
                return;
            }
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

        public void ClearFields()
        {
            planet.ClearInput();
            for (int i = 15; i <= 30; i++) ((TextBox)InputGrid.Children[i]).Text = "";
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
        }
        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".")
               && (!(((TextBox)sender).Text.Contains("."))
               && e.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
        private void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }

}

