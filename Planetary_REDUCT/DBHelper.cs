using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
namespace Planetary_REDUCT
{
    class DBHelper
    {
        private  SQLiteConnection sqlConnection;
        private SQLiteCommand command;
         public DataTable Table;
        private  SQLiteDataAdapter adapter;
        string squary = "Select * from Planetary_tab";
      public  DBHelper()
        {

      
             sqlConnection = new SQLiteConnection("Data Source = planetarydb.db; Version=3;");
            sqlConnection.Open();

        }
      
        public void UpdateDB()
        {
            SQLiteCommandBuilder comandbuilder = new SQLiteCommandBuilder(adapter);
           adapter.Fill(Table);
          //  adapter.Update(Table); //some problems
        }
        public DataTable CreateTable()
        {
            Table = new DataTable("Planetary_tab");
            command = new SQLiteCommand(squary, sqlConnection);
            command.ExecuteNonQuery();
            adapter = new SQLiteDataAdapter(command);
            //MySqlCommandBuilder commandBuilder= new MySqlCommandBuilder(adapter);
          //  adapter.Fill(Table);
            return Table;
        }
         public void InsertParams (List<string> value)
        {
            
          

            for (int i = 0; i < value.Count; i++)
            {
                
                //non insert
                string quary = "Update Planetary_tab Set ParamValue = ('" + value[i].ToString() + "') where (id= " + (i+1).ToString() + ")";
                
                command = new SQLiteCommand(quary);
                command.Connection = sqlConnection;
                //command.Prepare();
                command.ExecuteNonQuery();
            }
        }
    }
}
