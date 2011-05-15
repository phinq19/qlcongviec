using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Management;
using System.IO;
using System.Xml;

namespace AutoUp
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
        public static string GetStuff()
        {
            ManagementObjectSearcher searcher;
            int i = 0;
            string kq = "";
            try
            {
                searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject wmi_HD in searcher.Get())
                {
                    PropertyDataCollection searcherProperties = wmi_HD.Properties;
                    foreach (PropertyData sp in searcherProperties)
                    {
                        if (sp.Name == "Model" || sp.Name == "Signature")
                        {
                            i++;
                            kq += sp.Value.ToString();
                            if (i == 2)
                                return kq; ;

                        }

                    }
                }
                return kq;
            }
            catch (Exception ex)
            {
                return "";
            }
            return "";
        }
        public static bool CheckRegister()
        {
            bool flag = false;
            Encryption enc = new Encryption();
            if (File.Exists("reqlkd.dll"))
            {
                try
                {
                    string sSerialnumber = GetStuff();
                    FileStream fs = new FileStream("reqlkd.dll", FileMode.Open);
                    XmlTextReader r = new XmlTextReader(fs);
                    string sGuiID = "";
                    string sSerial = "";
                    while (r.Read())
                    {
                        if (r.NodeType == XmlNodeType.Element)
                        {
                            if (r.HasAttributes)
                            {
                                sGuiID = enc.DecryptData(r.GetAttribute("GuiNumber"));
                                sSerial = enc.DecryptData(r.GetAttribute("Serialnumber"));
                                if (sGuiID != "" && sSerial == sSerialnumber)
                                {
                                    string Register = enc.DecryptData(r.GetAttribute("KeyRegister"), sSerialnumber + sGuiID);
                                    string strK = sSerialnumber + sGuiID;
                                    string strKey = strK.Substring(2, 1) + strK.Substring(6, 1) + strK.Substring(4, 1) + strK.Substring(2, 1) + strK.Substring(8, 1) + strK.Substring(6, 1) + strK.Substring(3, 1) + strK.Substring(1, 1) + strK.Substring(3, 1);
                                    if (Register == strKey)
                                    {
                                        return true;
                                    }
                                }

                            }
                        }
                    }
                    r.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {


                }
            }
            return flag;
        }
    }
}
