using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WorkLibrary
{
    public class WebLink
    {
        public long ID;
        public long PageID;
        public string Url;
        public string UrlPost;
        public string UserName;
        public string Password;
        public string Topic;
        public string IDTopic;
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
                string sql = "delete from WebUp where ID=" + ID + "";
                Provider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static WebLink Get(long ID)
        {
            Type type = typeof(WebLink);
            string sql = @"select u.ID,p.ID as PageID,p.Page as Url,u.UrlPost,p.UserName,p.Password,u.Topic,u.IDTopic,u.Note,u.Group,u.Type from WebUp u,WebReg p where u.Page=p.ID and u.ID=" + ID;
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
            try
            {
                string sql = @" select u.ID,p.ID as PageID,p.Page as Url,u.UrlPost,p.UserName,p.Password,u.Topic,u.IDTopic,u.Note,u.Group,u.Type from WebUp u,WebReg p where u.Page=p.ID and u.Type='" + Type + "'";
                DataTable dtTable = Provider.ExecuteToDataTable(sql);
                return dtTable;
            }
            catch { return null; }

        }
        public static DataTable GetByPage(string Page)
        {

            string sql = @"select u.ID,p.ID as PageID,p.Page as Url,u.UrlPost,p.UserName,p.Password,u.Topic,u.IDTopic,u.Note,u.Group,u.Type from WebUp u,WebReg p where u.Page=p.ID and p.Page='" + Page + "'";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static DataTable GetNotIn(string str,string type)
        {
            string sql = "";
            if (str == "")
            {
                sql = @"select u.ID,p.ID as PageID,p.Page as Url,u.UrlPost,p.UserName,p.Password,u.Topic,u.IDTopic,u.Note,u.Group,u.Type from WebUp u,WebReg p where u.Page=p.ID and  u.Type='" + type + "' order by p.Page";
            }
            else
            {
                sql = @"select u.ID,p.ID as PageID,p.Page as Url,u.UrlPost,p.UserName,p.Password,u.Topic,u.IDTopic,u.Note,u.Group,u.Type from WebUp u,WebReg p where u.Page=p.ID and  u.ID not in " + str + " and u.Type='" + type + "' order by p.Page";
            }
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static DataTable GetIn(string str, string type)
        {
            string sql = @" select u.ID,p.ID as PageID,p.Page as Url,u.UrlPost,p.UserName,p.Password,u.Topic,u.IDTopic,u.Note,u.Group,u.Type from WebUp u,WebReg p where u.Page=p.ID and u.ID in " + str + " and u.Type='" + type + "' order by p.Page";
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
            return dtTable;

        }
    }
}
