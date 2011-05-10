using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Collections;

namespace UpdateWIN
{
    public class Provider
    {
        public static OleDbConnection myOleDbConnection;
        //public Provider()
        //{
        //    string path = System.Windows.Forms.Application.StartupPath;
        //    string ConnectString = @"PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "/database.mdb";
        //    myOleDbConnection = new OleDbConnection(ConnectString);
        //    //OleDbConnection myOleDbConnectionORAACB = new OleDbConnection(ConnectString.ORAACBConnectString);
        //}
        public void Close()
        {
            myOleDbConnection.Close();
        }
        public static bool Connect()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath;
                string ConnectString = @"PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "/database.mdb";
                myOleDbConnection = new OleDbConnection(ConnectString);
                myOleDbConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static int ExecuteNonQuery(string sql)
        {
            if (myOleDbConnection == null)
                Connect();
            OleDbCommand myOleDbCommand = new OleDbCommand(sql, myOleDbConnection);
           return myOleDbCommand.ExecuteNonQuery();
            
        }
        public static DataTable ExecuteToDataTable(string sql)
        {
            if (myOleDbConnection == null)
                Connect();
            DataSet dtset = new DataSet();
            OleDbDataAdapter myOleDataAdapter = new OleDbDataAdapter(sql, myOleDbConnection);
            myOleDataAdapter.Fill(dtset);
            return dtset.Tables[0];
        }
        public static OleDbDataReader ExecuteToDataReader(string sql)
        {
            if (myOleDbConnection == null)
                Connect();
            OleDbCommand myOleDbCommand = new OleDbCommand(sql, myOleDbConnection);
            return myOleDbCommand.ExecuteReader();

        }
        public static object ExecuteScalar(string sql)
        {
            if (myOleDbConnection == null)
                Connect();
            OleDbCommand myOleDbCommand = new OleDbCommand(sql, myOleDbConnection);
            return myOleDbCommand.ExecuteScalar();
        }
        public static void ExecuteStore(string sqlStore, ArrayList parameter)
        {
            if (myOleDbConnection == null)
                Connect();
            OleDbCommand myOleDbCommand = new OleDbCommand(sqlStore, myOleDbConnection);

            myOleDbCommand.CommandType = CommandType.StoredProcedure;
            int i = 0;
            foreach (object obj in parameter)
            {
                myOleDbCommand.Parameters.Add(new OleDbParameter(i.ToString(),obj));
                i++;
            }
            
            myOleDbCommand.ExecuteNonQuery();

         }
    }
}
