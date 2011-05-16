using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WorkLibrary
{
    public class RootLibrary
    {
        public static bool IsIdentity(Object obj)
        {
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                return attribute.Identity;

            }
            return false;
        }
        public static string GetScriptInsert(Object obj)
        {
            string sqlScript="";
            string field="";
            string values="";
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                System.Reflection.FieldInfo[] fieldInfo = obj.GetType().GetFields();
                
                if (attribute.Identity == false)
                {
                    foreach (System.Reflection.FieldInfo info in fieldInfo)
                    {
                        if (info.GetValue(obj) != null)
                        {
                            field = field + "[" + info.Name + "]" + ",";
                            values = values + GetStringValue(info.FieldType, info.GetValue(obj)) + ",";
                        }

                    }
                }
                else
                {
                    foreach (System.Reflection.FieldInfo info in fieldInfo)
                    {
                        if (info.GetValue(obj) != null && info.Name!=attribute.PrimaryKey)
                        {
                            field = field + "[" + info.Name + "]" + ",";
                            values = values + GetStringValue(info.FieldType, info.GetValue(obj)) + ",";
                        }

                    }
                }
                field = field.Trim(',');
                values = values.Trim(',');
                sqlScript = "Insert into [" + attribute.TableName+"]("+field+")" + " values("+values+")";
                
            }
            return sqlScript;
        }
        public static string GetScriptUpdate(Object obj)
        {
            string sqlScript = "";
            string setvalue = "";
            string where = "";
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                string PrimaryKeyList = "," + attribute.PrimaryKey + ",";
                System.Reflection.FieldInfo[] fieldInfo = obj.GetType().GetFields();
                    foreach (System.Reflection.FieldInfo info in fieldInfo)
                    {
                        if (info.GetValue(obj) != null)
                        {
                            if (PrimaryKeyList.IndexOf("," + info.Name + ",") < 0)
                            {
                                setvalue = setvalue + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj)) + ",";
                            }
                            else
                            {
                                if (where == "")
                                {
                                    where = " Where " + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj));
                                }
                                else
                                {
                                    where = where + " And " + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj));
                                }
                            }
                        }

                    }
                    setvalue = setvalue.Trim(',');
                    sqlScript = "Update [" + attribute.TableName + "] Set " + setvalue + where;

            }
            return sqlScript;
        }
        public static string GetScriptDelete(Object obj)
        {
            string sqlScript = "";
            string where = "";
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                string PrimaryKeyList = "," + attribute.PrimaryKey + ",";
                System.Reflection.FieldInfo[] fieldInfo = obj.GetType().GetFields();
                foreach (System.Reflection.FieldInfo info in fieldInfo)
                {
                    if (info.GetValue(obj) != null)
                    {

                        if (PrimaryKeyList.IndexOf("," + info.Name + ",") >= 0)
                        {
                            if (where == "")
                            {
                                where = " Where " + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj));
                            }
                            else
                            {
                                where = where + " And " + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj));
                            }
                        }
                    }

                }
                sqlScript = "Delete From [" + attribute.TableName + "] " + where;

            }
            return sqlScript;
        }
        public static string GetScriptGet(Object obj)
        {
            string sqlScript = "";
            string where = "";
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                string PrimaryKeyList = "," + attribute.PrimaryKey + ",";
                System.Reflection.FieldInfo[] fieldInfo = obj.GetType().GetFields();
                foreach (System.Reflection.FieldInfo info in fieldInfo)
                {
                    if (info.GetValue(obj) != null)
                    {

                        if (PrimaryKeyList.IndexOf("," + info.Name + ",") >= 0)
                        {
                            if (where == "")
                            {
                                where = " Where " + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj));
                            }
                            else
                            {
                                where = where + " And " + "[" + info.Name + "]" + " = " + GetStringValue(info.FieldType, info.GetValue(obj));
                            }
                        }
                    }

                }
                sqlScript = "Select * From [" + attribute.TableName + "] " + where;

            }
            return sqlScript;
        }
        public static string GetScriptGetAll(Object obj)
        {
            string sqlScript = "";
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                sqlScript = "Select * From [" + attribute.TableName + "] ";

            }
            return sqlScript;
        }
        public static string GetScriptGetByField(Object obj,String[] Field,Object[] Value)
        {
            string sqlScript = "";
            string strWhere = "";
            CustomAttributes attribute = (CustomAttributes)GetCustomAttributes(obj);
            if (attribute != null)
            {
                for (int i = 0; i < Field.Length; i++)
                {
                    if (strWhere == "")
                    {
                        strWhere = " Where " + "[" + Field[i] + "]" + " = " + GetStringValue(Value[i].GetType(),Value[i]);
                    }
                    else
                    {
                        strWhere = strWhere + " And " + "[" + Field[i] + "]" + " = " + GetStringValue(Value[i].GetType(), Value[i]);
                    }
                }
                sqlScript = "Select * From [" + attribute.TableName + "] " + strWhere;

            }
            return sqlScript;
        }
        private static String GetStringValue(Type type,Object value)
        {
            String strValue = "";
            if (value != null)
            {
                if (type.Name.ToUpper() == "STRING")
                {
                    strValue= "'" + value.ToString() + "'";
                }
                else if (type.Name.ToUpper() == "BOOLEAN")
                {
                    strValue = (((bool)value == true) ? "1" : "0").ToString();
                }
                else if (type.Name.ToUpper() == "DATETIME")
                {
                    strValue = "'" + value.ToString() + "'";
                }
                else
                {
                    strValue = value.ToString();
                }
            }
            return strValue;
        }
        private static String GetStringValue(Object value)
        {
            Type type = value.GetType();
            String strValue = "";
            if (value != null)
            {
                if (type.Name.ToUpper() == "STRING")
                {
                    strValue = "'" + value.ToString() + "'";
                }
                else if (type.Name.ToUpper() == "BOOLEAN")
                {
                    strValue = (((bool)value == true) ? "1" : "0").ToString();
                }
                else if (type.Name.ToUpper() == "DATETIME")
                {
                    strValue = "'" + value.ToString() + "'";
                }
                else
                {
                    strValue = value.ToString();
                }
            }
            return strValue;
        }
        private static Object GetCustomAttributes(Object obj)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(obj.GetType());
            foreach (Attribute attribute in attributes)
            {
                if (attribute.TypeId.ToString().IndexOf("CustomAttributes") >= 0)
                {
                    CustomAttributes customAttribute = (CustomAttributes)attribute;
                    return customAttribute;
                }
            }
            return null;
        }
        public static object GetObjectValue(DataRow dtRow, Object objReturn)
        {
            
            Type myObjectType = objReturn.GetType();
            Object obj = Activator.CreateInstance(myObjectType);
            System.Reflection.FieldInfo[] fieldInfo = myObjectType.GetFields();
            foreach (System.Reflection.FieldInfo info in fieldInfo)
            {
                try
                {
                    if (dtRow[info.Name] != null && dtRow[info.Name].ToString() != "")
                    {
                        info.SetValue(obj, dtRow[info.Name]);
                    }
                }
                catch
                {
                }

            }
            return obj;

        }
    }
}
