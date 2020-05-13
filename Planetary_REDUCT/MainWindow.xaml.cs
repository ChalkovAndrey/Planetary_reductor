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
using System.Configuration;
using System.Data.SqlClient;

using System.Data;
using System.Data.SQLite;

using System.IO;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Planetary_REDUCT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTable dataTable;


        public MainWindow()
        {
            InitializeComponent();

            CreateDocuments();

        }

        public void PlanetaryCall()
        {
            PlanetaryPage.Visibility = Visibility.Visible;
            PlanetaryPage.ClearFields();
        }
        public void StartPageCall()
        {
            StartPage.Visibility = Visibility.Visible;
        }
        public void WaveCall()
        {
            WavePage.Visibility = Visibility.Visible;
            WavePage.ClearFields();
        }

        private async void CreatePDFpage(Image imageObject, uint pageNumber)
        {
            string path = Environment.CurrentDirectory + "\\Documents\\ProgramGuide.pdf";
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);
            PdfDocument pdf = await PdfDocument.LoadFromFileAsync(file);
            PdfPage page = pdf.GetPage(pageNumber);
            BitmapImage image = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                await page.RenderToStreamAsync(stream);

                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream.AsStream();
                image.EndInit();
            }
            imageObject.Source = image;
        }

        private void CreateDocuments()
        {
            Style PageStyle = new Style();
            PageStyle.Setters.Add(new Setter { Property = Control.VerticalAlignmentProperty, Value = VerticalAlignment.Stretch });
            PageStyle.Setters.Add(new Setter { Property = Control.MarginProperty, Value = new Thickness(5) });
            PageStyle.Setters.Add(new Setter { Property = Control.HorizontalAlignmentProperty, Value = HorizontalAlignment.Stretch });
            PageStyle.Setters.Add(new Setter { Property = Grid.ColumnProperty, Value = 1 });

            for (int i = 0; i <= 6; i++)
            {
                Image image = new Image();
                Algoritm.Children.Add(image);
                Algoritm.RowDefinitions.Add(new RowDefinition());
                image.Style = PageStyle;
                image.SetValue(Grid.RowProperty, i);
                CreatePDFpage(image, Convert.ToUInt32(i));
            }

            for (int i = 7; i <= 13; i++)
            {
                Image image = new Image();
                MathModel.Children.Add(image);
                MathModel.RowDefinitions.Add(new RowDefinition());
                image.Style = PageStyle;
                image.SetValue(Grid.RowProperty, i - 7);
                CreatePDFpage(image, Convert.ToUInt32(i));
            }

            for (int i = 14; i <= 20; i++)
            {
                Image image = new Image();
                ProgramSpecification.Children.Add(image);
                ProgramSpecification.RowDefinitions.Add(new RowDefinition());
                image.Style = PageStyle;
                image.SetValue(Grid.RowProperty, i - 14);
                CreatePDFpage(image, Convert.ToUInt32(i));
            }

            for (int i = 21; i <= 27; i++)
            {
                Image image = new Image();
                UserGuide.Children.Add(image);
                UserGuide.RowDefinitions.Add(new RowDefinition());
                image.Style = PageStyle;
                image.SetValue(Grid.RowProperty, i - 21);
                CreatePDFpage(image, Convert.ToUInt32(i));
            }

        }

    }
}
