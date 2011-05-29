using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WorkLibrary
{
    public class WebReg
    {
        public long ID;
        public string Page;
        public string UserName;
        public string Password;
        public string Note;
        public string Type;

        public WebReg()
        {
           
        }
        public static bool Delete(long ID)
        {
            try
            {
                string sql = "delete from WebReg where ID=" + ID + "";
                Provider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static long Insert(WebReg cus)
        {

            string sql = @"INSERT INTO WebReg
           ([Page]
           ,[UserName]
           ,[Password]
            ,[Note]
           ,[Type])
         VALUES
           ('" + cus.Page + @"'
           ,'" + cus.UserName + @"'
           ,'" + cus.Password + @"'
              ,'" + cus.Note + @"'
            ,'" + cus.Type + @"')";
            Provider.ExecuteNonQuery(sql);
            return long.Parse(Provider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(WebReg cus)
        {
            string sql = @"UPDATE [WebReg]
               SET [Page] = '" + cus.Page + @"'
                  ,[UserName] = '" + cus.UserName + @"'
                  ,[Password] = '" + cus.Password + @"'
                 ,[Note] = '" + cus.Note + @"'
                  ,[Type] = '" + cus.Type + @"' 
             WHERE ID=" + cus.ID;
            Provider.ExecuteNonQuery(sql);


        }

        public static WebReg Get(long ID)
        {
            Type type = typeof(WebReg);
            string sql = @"select * from WebReg where ID=" + ID;
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                WebReg cus = new WebReg();
                return (WebReg)Common.GetObjectValue(dtTable.Rows[0], type, cus);

            }
            return null;

        }
        public static DataTable GetByType(string Type)
        {
            
            string sql = @"select * from WebReg where Type='" + Type+"'";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
    }
}
