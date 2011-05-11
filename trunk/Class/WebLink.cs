using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NewProject
{
    public class WebLink
    {
        public int ID;
        public string Url;
        public string UserName;
        public string Password;
        public string Type;

        public WebLink()
        {
           
        }
        public static bool Delete(int ID)
        {
            try
            {
                string sql = "delete from WebLink where ID=" + ID + "";
                Provider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static int Insert(WebLink cus)
        {

            string sql = @"INSERT INTO WebLink
           ([Url]
           ,[UserName]
           ,[Password]
           ,[Type])
         VALUES
           ('" + cus.Url + @"'
           ,'" + cus.UserName + @"'
           ,'" + cus.Password + @"'
            ,'" + cus.Type + @"')";
            Provider.ExecuteNonQuery(sql);
            return int.Parse(Provider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(WebLink cus)
        {
            string sql = @"UPDATE [WebLink]
               SET [Url] = '" + cus.Url + @"'
                  ,[UserName] = '" + cus.UserName + @"'
                  ,[Password] = '" + cus.Password + @"'
                  ,[Type] = '" + cus.Type + @"' 
             WHERE ID=" + cus.ID;
            Provider.ExecuteNonQuery(sql);


        }

        public static WebLink Get(long ID)
        {
            Type type = typeof(WebLink);
            string sql = @"select * from WebLink where ID=" + ID;
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                WebLink cus = new WebLink();
                return (WebLink)Common.GetObjectValue(dtTable.Rows[0], type, cus);

            }
            return null;

        }
        public static DataTable GetByType(string Type)
        {
            
            string sql = @"select * from WebLink where Type='" + Type+"'";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
    }
}
