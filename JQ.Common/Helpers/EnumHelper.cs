using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JQ.Common.Helpers
{
    /// <summary>
    /// 枚举操作助手
    /// </summary>
    public class EnumHelper
    {

        /// <summary>
        /// 根据枚举类型返回类型中的所有值，文本及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns>返回三列数组，第0列为Description,第1列为Value，第2列为Text</returns>
        public static List<string[]> GetEnumList(Type type)
        {
            List<string[]> Strs = new List<string[]>();
            FieldInfo[] fields = type.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                string[] strEnum = new string[3];
                FieldInfo field = fields[i];
                strEnum[1] = ((int)Enum.Parse(type, field.Name)).ToString();
                strEnum[2] = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    strEnum[0] = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    strEnum[0] = da.Description;
                }

                Strs.Add(strEnum);
            }
            return Strs;
        }

        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumDescription(object enumSubitem)
        {
            enumSubitem = (Enum)enumSubitem;
            string strValue = enumSubitem.ToString();
            try
            {
                FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);

                if (fieldinfo != null)
                {

                    Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (objs == null || objs.Length == 0)
                    {
                        return strValue;
                    }
                    else
                    {
                        DescriptionAttribute da = (DescriptionAttribute)objs[0];
                        return da.Description;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

        }

        /// <summary>
        ///  枚举转成dictionary类， 取name 为key  取desc为value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, object> EnumListDic<T>()
        {
            Dictionary<string, object> dicEnum = new Dictionary<string, object>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }

            string[] fieldstrs = Enum.GetNames(enumType);
            foreach (var item in fieldstrs)
            {
                string description = string.Empty;
                var field = enumType.GetField(item);
                object[] arr = field.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (arr != null && arr.Length > 0)
                {
                    description = ((DescriptionAttribute)arr[0]).Description;
                }
                else
                {
                    description = item;
                }
                dicEnum.Add(item, description);
            }
            return dicEnum;
        }

        /// <summary>
        /// 将枚举的值和描述转成Dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> EnumDic<T>()
        {
            Dictionary<int, string> dicEnum = new Dictionary<int, string>();
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                return dicEnum;
            }

            string desc = string.Empty;
            foreach (var e in Enum.GetValues(enumType))
            {
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    desc = da.Description;
                }
                dicEnum.Add(Convert.ToInt32(e), desc);

            }
            return dicEnum;
        }


        /// <summary>
        /// 枚举转成实体类集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<EnumEntity> EnumToList<T>()
        {
            List<EnumEntity> list = new List<EnumEntity>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                EnumEntity m = new EnumEntity();
                object[] objArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objArr != null && objArr.Length > 0)
                {
                    DescriptionAttribute da = objArr[0] as DescriptionAttribute;
                    m.EnumDesc = da.Description;
                }
                m.EnumValue = Convert.ToInt32(e);
                m.EnumName = e.ToString();
                list.Add(m);
            }
            return list;
        }
    }



    public class EnumDescription : Attribute
    {
        private string _text;
        public string Text
        {
            get { return _text; }
        }

        public EnumDescription(string text)
        {
            _text = text;
        }
    }

    public class EnumEntity
    {
        public string EnumDesc { get; set; }

        public string EnumName { get; set; }

        public int EnumValue { get; set; }
    }
}
