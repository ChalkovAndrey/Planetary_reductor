using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
namespace Planetary_REDUCT
{
    class DBHelper
    {
        private  MySqlConnection mysqlConnection;
        private  MySqlCommand command;
         public DataTable Table;
        private  MySqlDataAdapter adapter;
        string squary = "Select * from piproject.planetary_tab";
      public  DBHelper()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "127.0.0.1";
            builder.UserID = "root";
            builder.Password = "78Pubufi";
            builder.Database = "piproject";
            builder.SslMode = MySqlSslMode.None;
            mysqlConnection = new MySqlConnection(builder.ToString());
            mysqlConnection.Open();
          
            
        }
      
        public void UpdateDB()
        {
            MySqlCommandBuilder comandbuilder = new MySqlCommandBuilder(adapter);
            adapter.Update(Table);
        }
        public DataTable CreateTable()
        {
            Table = new DataTable();
            adapter = new MySqlDataAdapter(squary, mysqlConnection);
            MySqlCommandBuilder commandBuilder= new MySqlCommandBuilder(adapter);
            adapter.Fill(Table);
            return Table;
        }
         public void InsertParams (List<string> value)
        {
            

            for (int i = 0; i < value.Count; i++)
            {
                //non insert
                string quary = "Update piproject.planetary_tab Set ParamValue = '" + value[i].ToString() + "' where (id= " + (i+1).ToString() + ")";
                command = new MySqlCommand(quary);
                command.Connection = mysqlConnection;
                //command.Prepare();
                command.ExecuteNonQuery();
            }
        }
    }
}
