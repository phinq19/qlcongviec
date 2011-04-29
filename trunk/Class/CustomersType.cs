
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Web;
using System.Data;

namespace NewProject
{
	public class CustomersType
	{
		public long Code;
        public string Name;
        public string Note;
		
		#region Constructors
		
		public CustomersType()
		{

		}


		#endregion
		

		public static DataTable GetAll()
        {
            string sql = "select * from CustomersType ";
            return Provider.ExecuteToDataTable(sql);
        }
        public static void Delete(long Code)
        {
            string sql = "delete from CustomersType where Code=" + Code + "";
            Provider.ExecuteNonQuery(sql);

            
            
        }
        public static long Insert(CustomersType cus)
        {
            string sql = "insert into [CustomersType]([Name],[Note]) values('" + cus.Name + "','" + cus.Note + "')";
            Provider.ExecuteNonQuery(sql);
            return long.Parse(Provider.ExecuteScalar("Select @@IDENTITY").ToString());


        }
        public static void Update(CustomersType cus)
        {
            string sql = "Update [CustomersType] set [Name]='" + cus.Name + "',[Note]='" + cus.Note + "' where [Code]=" + cus.Code + "";
            Provider.ExecuteNonQuery(sql);
        }
        public static CustomersType Get(long ID)
        {
            string sql = @"select * from CustomersType where Code=" + ID;
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                return GetObject(dtTable.Rows[0]);

            }
            return null;

        }
        public static CustomersType GetObject(DataRow dtRow)
        {
            CustomersType cus = new CustomersType();
            if (dtRow["Code"] != null && dtRow["Code"].ToString() != "")
                cus.Code = long.Parse(dtRow["Code"].ToString());
            if (dtRow["Name"] != null && dtRow["Name"].ToString() != "")
                cus.Name = dtRow["Name"].ToString();
             if (dtRow["Note"] != null && dtRow["Note"].ToString() != "")
                cus.Note = (dtRow["Note"].ToString());
            return cus;
        }

	}
}
