using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class UseSqlLite
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();


        public void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=my_first_db.sqlite3;Version=3;New=False;Compress=True;");
        }


        public void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        public void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select id, desc from table1";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            Console.WriteLine(DT);
            sql_con.Close();
        }

        public void Add(string txtDesc)
        {
            string txtSQLQuery = "insert into  mains (desc) values ('" + txtDesc + "')";
            ExecuteQuery(txtSQLQuery);
        }
    }
}
