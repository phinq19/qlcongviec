
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Web;
using System.Data;

namespace WorkLibrary
{
	public class WebPage
	{
		public long ID;
        public string Page;
	    public string Type;
	    public int Active;
        public WebPage()
		{
            Active = 0;
		}

        public static bool Delete(long ID)
        {
            try
            {
                string sql = "delete from QLCV_WebPage where ID=" + ID + "";
                ServerProvider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static DataTable GetAll()
        {

            string sql = @"select * from QLCV_WebPage";
            DataTable dtTable = ServerProvider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static DataTable GetActive(string Type)
        {

            string sql = @"select * from QLCV_WebPage where Active=1 and Type='" + NumCode.UPWEB + "'";
            DataTable dtTable = ServerProvider.ExecuteToDataTable(sql);
            return dtTable;

        }
        public static WebPage Get(long ID)
        {
            string sql = @"select * from QLCV_WebPage where ID=" + ID;
            DataTable dtTable = ServerProvider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                WebPage cus = new WebPage();
                return (WebPage)RootLibrary.GetObjectValue(dtTable.Rows[0], cus);

            }
            return null;

        }
        public static long Insert(WebPage cus)
        {

            string sql = @"INSERT INTO QLCV_WebPage
           ([Page]
           ,[Active]
           ,[Type])
         VALUES
           (" + cus.Page + @"
             ," + cus.Active + @"
            ,'" + cus.Type + @"')";
            ServerProvider.ExecuteNonQuery(sql);
            return long.Parse(ServerProvider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(WebPage cus)
        {
            string sql = @"UPDATE [QLCV_WebPage]
               SET [Page] = '" + cus.Page + @"'
                  ,[Active] = " + cus.Active + @"
                  ,[Type] = '" + cus.Type + @"' 
             WHERE ID=" + cus.ID;
            ServerProvider.ExecuteNonQuery(sql);


        }

        public static WebPage GetByPage(string page)
        {
            string sql = @"select * from QLCV_WebPage where Page='" + page + "' and Type='"+NumCode.UPWEB+"'";
            DataTable dtTable = ServerProvider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                WebPage cus = new WebPage();
                return (WebPage)RootLibrary.GetObjectValue(dtTable.Rows[0], cus);

            }
            return null;
        }
	}
}
