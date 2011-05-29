using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WorkLibrary
{
    public class FieldSetting
    {
        public int ID;
        public string Control;
        public string Attribute;
        public string Value;
        public string Field;
        public string Type;
        public FieldSetting()
		{

		}
        public static bool Delete(int ID)
        {
            try
            {
                string sql = "delete from QLCV_FieldSetting where ID=" + ID + "";
                ServerProvider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static int Insert(FieldSetting cus)
        {

            string sql = @"INSERT INTO QLCV_FieldSetting
           ([Control]
           ,[Attribute]
           ,[Value]
           ,[Field],[Type])
         VALUES
           (N'" + cus.Control + @"'
           ,N'" + cus.Attribute + @"'
           ,N'" + cus.Value + @"'
            ,N'" + cus.Field + @"'
            ,N'" + cus.Type + @"')";
            ServerProvider.ExecuteNonQuery(sql);
            return int.Parse(ServerProvider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(FieldSetting cus)
        {
            string sql = @"UPDATE [QLCV_FieldSetting]
               SET [Control] = N'" + cus.Control + @"'
                  ,[Attribute] = N'" + cus.Attribute + @"'
                  ,[Value] = N'" + cus.Value + @"'
                  ,[Field] = N'" + cus.Field + @"' 
                ,[Type] = '" + cus.Type + @"' 
             WHERE ID=" + cus.ID;
            ServerProvider.ExecuteNonQuery(sql);


        }

        public static FieldSetting Get(long ID)
        {
            Type type = typeof(FieldSetting);
            string sql = @"select * from QLCV_FieldSetting where ID=" + ID;
            DataTable dtTable = ServerProvider.ExecuteToDataTable(sql);
            if (dtTable.Rows.Count > 0)
            {
                FieldSetting cus = new FieldSetting();
                return (FieldSetting)Common.GetObjectValue(dtTable.Rows[0], type, cus);

            }
            return null;

        }
        public static DataTable GetFieldForum()
        {
            try
            {
                string strSQL = "Select * From QLCV_FieldForum";
                return ServerProvider.ExecuteToDataTable(strSQL);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetByFieldDataTable(string Field, string Type)
        {
            try
            {
                string sql = @"select * from QLCV_FieldSetting where Field='" + Field + "' and Type='" + Type + "'";
                return ServerProvider.ExecuteToDataTable(sql);
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<HControl> GetByField(string Field,string Type)
        {
            try
            {
                List<HControl> fields = new List<HControl>();
                string sql = @"select * from QLCV_FieldSetting where Field='" + Field + "' and Type='" + Type + "'";
                DataTable table = ServerProvider.ExecuteToDataTable(sql);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        HControl ctr = new HControl();
                        ctr.Control = row["Control"].ToString();
                        ctr.Attribute = row["Attribute"].ToString();
                        ctr.Value = row["Value"].ToString();

                        fields.Add(ctr);
                    }
                }

                return fields;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
