using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace WorkLibrary
{
    public class ServerProvider
    {
        public static SqlConnection mySqlConnection;
        //public Provider()
        //{
        //    string path = System.Windows.Forms.Application.StartupPath;
        //    string ConnectString = @"PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "/database.mdb";
        //    myOleDbConnection = new OleDbConnection(ConnectString);
        //    //OleDbConnection myOleDbConnectionORAACB = new OleDbConnection(ConnectString.ORAACBConnectString);
        //}
        public void Close()
        {
            mySqlConnection.Close();
        }
        public static bool Connect()
        {
            try
            {
                string ConnectString=@"user id=viettin;
                                       password=123456;server=112.213.89.70;
                                       Trusted_Connection=no;
                                       database=viettin_db; 
                                       connection timeout=30";
                mySqlConnection = new SqlConnection(ConnectString);
                mySqlConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int ExecuteNonQuery(string sql)
        {
            if (mySqlConnection == null)
                Connect();
            SqlCommand myOleDbCommand = new SqlCommand(sql, mySqlConnection);
           return myOleDbCommand.ExecuteNonQuery();
            
        }
        public static DataTable ExecuteToDataTable(string sql)
        {
            if (mySqlConnection == null)
                Connect();
            DataSet dtset = new DataSet();
            SqlDataAdapter myOleDataAdapter = new SqlDataAdapter(sql, mySqlConnection);
            myOleDataAdapter.Fill(dtset);
            return dtset.Tables[0];
        }
        public static SqlDataReader ExecuteToDataReader(string sql)
        {
            if (mySqlConnection == null)
                Connect();
            SqlCommand myOleDbCommand = new SqlCommand(sql, mySqlConnection);
            return myOleDbCommand.ExecuteReader();

        }
        public static object ExecuteScalar(string sql)
        {
            if (mySqlConnection == null)
                Connect();
            SqlCommand myOleDbCommand = new SqlCommand(sql, mySqlConnection);
            return myOleDbCommand.ExecuteScalar();
        }
        public static void ExecuteStore(string sqlStore, ArrayList parameter)
        {
            if (mySqlConnection == null)
                Connect();
            SqlCommand myOleDbCommand = new SqlCommand(sqlStore, mySqlConnection);

            myOleDbCommand.CommandType = CommandType.StoredProcedure;
            int i = 0;
            foreach (object obj in parameter)
            {
                myOleDbCommand.Parameters.Add(new SqlParameter(i.ToString(),obj));
                i++;
            }
            
            myOleDbCommand.ExecuteNonQuery();

         }
    }
}
