using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WorkLibrary
{
    public class WebLink
    {
        public long ID;
        public string Url;
        public string UrlPost;
        public string UserName;
        public string Password;
        public string Topic;
        public string Note;
        public int Group;
        public string Type;

        public WebLink()
        {
           
        }
        public static bool Delete(long ID)
        {
            try
            {
                string sql = "delete from WebLink where ID=" + ID + "";
                Provider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static long Insert(WebLink cus)
        {

            string sql = @"INSERT INTO WebLink
           ([Url]
           ,[UrlPost]
           ,[UserName]
           ,[Password]
           ,[Topic]
           ,[Note]
           ,[Group]
           ,[Type])
         VALUES
           ('" + cus.Url + @"'
            ,'" + cus.UrlPost + @"'
           ,'" + cus.UserName + @"'
           ,'" + cus.Password + @"'
            ,'" + cus.Topic+ @"'
            ,'" + cus.Note + @"'
             ," + cus.Group + @"
            ,'" + cus.Type + @"')";
            Provider.ExecuteNonQuery(sql);
            return long.Parse(Provider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(WebLink cus)
        {
            string sql = @"UPDATE [WebLink]
               SET [Url] = '" + cus.Url + @"'
                    ,[UrlPost] = '" + cus.UrlPost + @"'
                  ,[UserName] = '" + cus.UserName + @"'
                  ,[Password] = '" + cus.Password + @"'
                    ,[Note] = '" + cus.Note + @"'
                     ,[Topic] = '" + cus.Topic + @"'
                  ,[Group] = " + cus.Group + @"
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
        public static DataTable GetNotIn(string str,string type)
        {
            string sql = "";
            if (str == "")
            {
                sql = @"select * from WebLink where Type='" + type+"' order by Url";
            }
            else
            {
                sql = @"select * from WebLink where ID not in " + str + " and Type='" + type+"' order by Url";
            }
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
    }
}
