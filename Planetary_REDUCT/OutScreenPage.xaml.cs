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
using System.Globalization;
using System.Data;
namespace Planetary_REDUCT
{
    /// <summary>
    /// Логика взаимодействия для OutScreenPage.xaml
    /// </summary>
    public partial class OutScreenPage : UserControl
    {
        

        DataTable data;
        Wave wave = new Wave();
        public OutScreenPage()
        {
            InitializeComponent();
            data = GetStartEmptyTable();
           
            ResultGrid.ItemsSource = data.DefaultView;
        }
        private DataTable GetStartEmptyTable()
        {
            DataTable data = new DataTable();
            DataColumn param = new DataColumn("Вычисляемый параметр",typeof(string));
            DataColumn designation = new DataColumn("Обозначение",typeof(string));
            DataColumn ed = new DataColumn("Единицы измерения",typeof(string));
            DataColumn value=new DataColumn("Рассчитанное значение",typeof(string));
           
            data.Columns.Add(param);
            data.Columns.Add(designation);
            data.Columns.Add(ed);
            data.Columns.Add(value);
            return data;
          
          

        }
        public void LoadData()
        {
            for (int i = 0; i < wave.NameParams.Count; i++)
            {
                DataRow dataRow = data.NewRow();
                dataRow[0] = Wave.Result[i, 0];
                dataRow[3] = Wave.Result[i, 3];
                dataRow[1] = Wave.Result[i, 1];
                dataRow[2] = Wave.Result[i, 2];
             //   dataRow[1] = wave.Test[i];
                data.Rows.Add(dataRow);
            }
        }
        private void SaveResult(object sender, RoutedEventArgs e)
        {
          //SaveClick

        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            
            Grid w =(Grid) this.Parent;
            w.Visibility = Visibility.Collapsed;
            Grid s = (Grid)w.Parent;
            s.Children[0].Visibility = Visibility.Visible;
            data.Clear();

        }
    }
   
}

