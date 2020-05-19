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
using UserControlFun;

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
            if (wave.Cz > 1 || wave.Cz < 0.1 || wave.Tout > 500 || wave.Tout < 30 || wave.Nout > 400
               || wave.mo < 0.1 || wave.mo > 1 || wave.mk > 1 || wave.mk < 0.1 || wave.Dr > 1000 || wave.Dr < 20)
            {
                MessageBox.Show("Данные введены неверно.");
                return;
            }
            wave.Construction();
            InputGrid.Visibility = Visibility.Collapsed;
            OutScreenPage outScreenPage = (OutScreenPage)OutputGrid.Children[0];
            outScreenPage.LoadWaveData(wave);
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

        private void ExampleClick(object sender, RoutedEventArgs e)
        {
            wave.SetExample();
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            ClearFields();
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
        public void ClearFields()
        {
            wave.ClearInput();
            ((RangeSlider)InputGrid.Children[6]).LowerValue = 0;
            ((RangeSlider)InputGrid.Children[6]).UpperValue = 0;
            for (int i = 9; i <= 13; i++) ((TextBox)InputGrid.Children[i]).Text = "";
        }
    }
}
