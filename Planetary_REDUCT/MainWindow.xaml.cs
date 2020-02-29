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
using MySql.Data.MySqlClient;
using System.Data;
using MySql.Data.MySqlClient;
namespace Planetary_REDUCT
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public  DataTable dataTable;
        MySqlConnection connectionString;
        string sQueary = "select * from piproject.planetary_tab;";
        public MainWindow()
        {
            InitializeComponent();
            //string[] value = new string[] { "test1", "test2", "test3" };
            //// MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            //// builder.Server = "127.0.0.1";
            //// builder.UserID = "root";
            //// builder.Password = "78Pubufi";
            //// builder.Database = "piproject";
            //// builder.SslMode = MySqlSslMode.None;
            //// connectionString = new MySqlConnection(builder.ToString());
            //// connectionString.Open();
            ////dataTable = new DataTable();

            //// MySqlDataAdapter myDA = new MySqlDataAdapter(sQueary, connectionString);
            //// MySqlCommandBuilder cmb = new MySqlCommandBuilder(myDA);
            //// myDA.Fill(dataTable);
            //DBHelper dB = new DBHelper();
            //dataTable = new DataTable();
            //dB.InsertParams(value);
            //dB.SetOnDatagrid(dataTable);
          
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
