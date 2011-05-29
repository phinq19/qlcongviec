using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WorkLibrary
{
    public class WebUp
    {
        public long ID;
        public long Page;
        public string UrlPost;
        public string Topic;
        public string IDTopic;
        public string Note;
        public int Group;
        public string Type;

        public WebUp()
        {
           
        }
        public static bool Delete(long ID)
        {
            try
            {
                string sql = "delete from WebUp where ID=" + ID + "";
                Provider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static long Insert(WebUp cus)
        {

            string sql = @"INSERT INTO WebUp
           ([Page]
           ,[UrlPost]
           ,[Topic]
           ,[IDTopic]
           ,[Note]
           ,[Group]
           ,[Type])
         VALUES
           (" + cus.Page + @"
            ,'" + cus.UrlPost + @"'
            ,'" + cus.Topic+ @"'
            ,'" + cus.IDTopic + @"'
            ,'" + cus.Note + @"'
             ," + cus.Group + @"
            ,'" + cus.Type + @"')";
            Provider.ExecuteNonQuery(sql);
            return long.Parse(Provider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(WebUp cus)
        {
            string sql = @"UPDATE [WebUp]
               SET [Page] = " + cus.Page + @"
                    ,[UrlPost] = '" + cus.UrlPost + @"'
                    ,[Note] = '" + cus.Note + @"'
                     ,[Topic] = '" + cus.Topic + @"'
                    ,[IDTopic] = '" + cus.IDTopic + @"'
                  ,[Group] = " + cus.Group + @"
                  ,[Type] = '" + cus.Type + @"' 
             WHERE ID=" + cus.ID;
            Provider.ExecuteNonQuery(sql);


        }

        public static WebUp Get(long ID)
        {
            Type type = typeof(WebUp);
            string sql = @"select * from WebUp where ID=" + ID;
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                WebUp cus = new WebUp();
                return (WebUp)Common.GetObjectValue(dtTable.Rows[0], type, cus);

            }
            return null;

        }
        public static DataTable GetByType(string Type)
        {
            
            string sql = @"select * from WebUp where Type='" + Type+"'";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static DataTable GetByPage(string Page)
        {

            string sql = @"select * from WebUp where Url='" + Page + "'";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static DataTable GetNotIn(string str,string type)
        {
            string sql = "";
            if (str == "")
            {
                sql = @"select * from WebUp where Type='" + type+"' order by Url";
            }
            else
            {
                sql = @"select * from WebUp where ID not in " + str + " and Type='" + type+"' order by Url";
            }
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static DataTable GetIn(string str, string type)
        {
            string sql = @"select * from WebUp where ID in " + str + " and Type='" + type + "' order by Url";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
    }
}
