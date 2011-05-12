using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace NewProject
{
    public class FieldSetting
    {
        public int ID;
        public string Control;
        public string Attribute;
        public string Value;
        public string Field;
        public FieldSetting()
		{

		}
        public static bool Delete(int ID)
        {
            try
            {
                string sql = "delete from FieldSetting where ID=" + ID + "";
                Provider.ExecuteNonQuery(sql);
                return true;
            }
            catch { return false; }


        }
        public static int Insert(FieldSetting cus)
        {

            string sql = @"INSERT INTO FieldSetting
           ([Control]
           ,[Attribute]
           ,[Value]
           ,[Field])
         VALUES
           ('" + cus.Control + @"'
           ,'" + cus.Attribute + @"'
           ,'" + cus.Value + @"'
            ,'" + cus.Field + @"')";
            Provider.ExecuteNonQuery(sql);
            return int.Parse(Provider.ExecuteScalar("Select @@IDENTITY").ToString());

        }
        public static void Update(FieldSetting cus)
        {
            string sql = @"UPDATE [FieldSetting]
               SET [Control] = '" + cus.Control + @"'
                  ,[Attribute] = '" + cus.Attribute + @"'
                  ,[Value] = '" + cus.Value + @"'
                  ,[Field] = '" + cus.Field + @"' 
             WHERE ID=" + cus.ID;
            Provider.ExecuteNonQuery(sql);


        }

        public static FieldSetting Get(long ID)
        {
            Type type = typeof(FieldSetting);
            string sql = @"select * from FieldSetting where ID=" + ID;
            DataTable dtTable = Provider.ExecuteToDataTable(sql);
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
                string strSQL = "Select * From FieldForum";
                return Provider.ExecuteToDataTable(strSQL);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataTable GetByFieldDataTable(string Field)
        {
            try
            {
                string sql = @"select * from FieldSetting where Field='" + Field + "'";
                return Provider.ExecuteToDataTable(sql);
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<HControl> GetByField(string Field)
        {
            try
            {
                List<HControl> fields = new List<HControl>();
                string sql = @"select * from FieldSetting where Field='" + Field+"'";
                DataTable table = Provider.ExecuteToDataTable(sql);
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
