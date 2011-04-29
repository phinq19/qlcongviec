using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NewProject
{
    public class Common
    {
        public static string GetResourceName(string name)
        {
            switch (name)
            {
                case "btnThem":
                    return "Thêm mới (&N)";
                case "btnXoa":
                    return "Xóa (&X)";
                case "btnSua":
                    return "Sửa (&E)";
                case "btnThoat":
                    return "Thoát (&T)";
                case "btnLuu":
                    return "Lưu (&S)";
                case "btnHuy":
                    return "Hủy (&H)";
            }
            return "";
        }
        public static object GetObjectValue(DataRow dtRow, Type type,Object objReturn)
        {
            //Object objReturn=new Object();
            Type myObjectType = type;
            System.Reflection.FieldInfo[] fieldInfo = myObjectType.GetFields();
            foreach (System.Reflection.FieldInfo info in fieldInfo)
            {
                try
                {
                    if (dtRow[info.Name] != null && dtRow[info.Name].ToString() != "")
                    {
                        info.SetValue(objReturn, dtRow[info.Name]);
                    }
                }
                catch
                {
                }

            }
            return objReturn;

        }
    }
}
