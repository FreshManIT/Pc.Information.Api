using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Pc.Information.Utility.DataConvert
{
    public class DataTypeConvertHelper
    {
        /// <summary>
        /// convert to bool
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ToBool(object val)
        {
            if ((val == null) || (val == DBNull.Value))
            {
                return false;
            }
            if (val is bool)
            {
                return (bool)val;
            }
            return ((val.ToString().ToLower() == "true") || (val.ToString().ToLower() == "1"));
        }

        /// <summary>
        /// 转换成byte
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte ToByte(object val)
        {
            return ToByte(val, 0);
        }

        /// <summary>
        /// 转换成byte
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(object val, byte defaultValue)
        {
            byte num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is byte)
            {
                return (byte)val;
            }
            return !byte.TryParse(val.ToString(), out num) ? defaultValue : num;
        }

        /// <summary>
        /// 转换成byte?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static byte? ToByteNullable(object val)
        {
            var num = ToByte(val);
            if (num.Equals(0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成DateTime，转换失败返回1900-1-1
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object val)
        {
            DateTime time;
            if ((val == null) || (val == DBNull.Value))
            {
                return new DateTime(0x76c, 1, 1);
            }
            if (val is DateTime)
            {
                return (DateTime)val;
            }
            return !DateTime.TryParse(val.ToString(), out time) ? new DateTime(0x76c, 1, 1) : time;
        }

        /// <summary>
        /// 转换成DateTime?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(object val)
        {
            var time = ToDateTime(val);
            if (time.Equals(new DateTime(0x76c, 1, 1)))
            {
                return null;
            }
            return time;
        }

        /// <summary>
        /// 转换为DateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">返回的默认值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            var result = defaultValue;
            if (obj == null)
            {
                return result;
            }
            if (!DateTime.TryParse(obj.ToString().Trim(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        ///  根据数据日期类型 转化日期
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <param name="dateFormat">输入日期格式 比如  yyyyMMdd</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj, DateTime defaultValue, string dateFormat)
        {
            var result = defaultValue;

            if (obj == null)
            {
                return result;
            }
            //日期验证
            IFormatProvider ifp = new CultureInfo("zh-TW");
            DateTime.TryParseExact(obj.ToString(), dateFormat, ifp, DateTimeStyles.None, out result);
            return result;
        }

        /// <summary>
        /// 转换成decimal 默认保留2位小数点
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object val)
        {
            return ToDecimal(val, 0M, 2);
        }

        /// <summary>
        /// 转换成decimal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object val, int decimals)
        {
            return ToDecimal(val, 0M, decimals);
        }

        /// <summary>
        /// 转换成decimal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object val, decimal defaultValue, int decimals)
        {
            var result = defaultValue;
            if (val == null || val == DBNull.Value)
            {
                return result;
            }
            if (!decimal.TryParse(val.ToString().Trim(), out result))
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转换成decimal?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static decimal? ToDecimalNullable(object val)
        {
            decimal num = ToDecimal(val);
            if (num.Equals(0.0M))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成double 默认保留2位小数点
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double ToDouble(object val)
        {
            return ToDouble(val, 0.0, 2);
        }

        /// <summary>
        /// 转换成double
        /// </summary>
        /// <param name="val"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static double ToDouble(object val, int digits)
        {
            return ToDouble(val, 0.0, digits);
        }

        /// <summary>
        /// 转换成double
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static double ToDouble(object val, double defaultValue, int digits)
        {
            double num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is double)
            {
                return Math.Round((double)val, digits);
            }
            if (!double.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return Math.Round(num, digits);
        }

        /// <summary>
        /// 转换成double?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static double? ToDoubleNullable(object val)
        {
            double num = ToDouble(val);
            if (num.Equals(0.0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成float 默认保留2位小数点
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float ToFloat(object val)
        {
            return ToFloat(val, 0f);
        }

        /// <summary>
        /// 转换成float
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float ToFloat(object val, float defaultValue)
        {
            float num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is float)
            {
                return (float)val;
            }
            if (!float.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成float?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static float? ToFloatNullable(object val)
        {
            float num = ToFloat(val);
            if (num.Equals(0f))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(object val)
        {
            return ToInt(val, 0);
        }

        /// <summary>
        /// 转换成int
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(object val, int defaultValue)
        {
            int num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is int)
            {
                return (int)val;
            }
            if (!int.TryParse(val.ToString().Trim(), NumberStyles.Number, null, out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成int?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int? ToIntNullable(object val)
        {
            int num = ToInt(val);
            if (num.Equals(0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成long
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long ToLong(object val)
        {
            return ToLong(val, 0L);
        }

        /// <summary>
        /// 转换成long
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ToLong(object val, long defaultValue)
        {
            long num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is long)
            {
                return (long)val;
            }
            if (!long.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成long?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long? ToLongNullable(object val)
        {
            long num = ToLong(val);
            if (num.Equals(0L))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成sbyte
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static sbyte ToSbyte(object val)
        {
            return ToSbyte(val, 0);
        }

        /// <summary>
        /// 转换成sbyte
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static sbyte ToSbyte(object val, sbyte defaultValue)
        {
            sbyte num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is sbyte)
            {
                return (sbyte)val;
            }
            if (!sbyte.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成sbyte?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static sbyte? ToSbyteNullable(object val)
        {
            sbyte num = ToSbyte(val);
            if (num.Equals(0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成short
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static short ToShort(object val)
        {
            return ToShort(val, 0);
        }

        /// <summary>
        /// 转换成short
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToShort(object val, short defaultValue)
        {
            short num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is short)
            {
                return (short)val;
            }
            if (!short.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成short?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static short? ToShortNullable(object val)
        {
            short num = ToShort(val);
            if (num.Equals(0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToString(object val)
        {
            if ((val == null) || (val == DBNull.Value))
            {
                return string.Empty;
            }
            if (val.GetType() == typeof(byte[]))
            {
                return Encoding.ASCII.GetString((byte[])val, 0, ((byte[])val).Length);
            }
            return val.ToString();
        }

        /// <summary>
        /// 转换成string
        /// </summary>
        /// <param name="val"></param>
        /// <param name="replace"></param>
        /// <returns></returns>
        public static string ToString(object val, string replace)
        {
            string str = ToString(val);
            return (string.IsNullOrEmpty(str) ? replace : str);
        }

        /// <summary>
        /// 转换成string
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToStringNullable(object val)
        {
            string str = ToString(val);
            return (string.IsNullOrEmpty(str) ? null : str);
        }

        /// <summary>
        /// 转换成uint
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static uint ToUint(object val)
        {
            return ToUint(val, 0);
        }

        /// <summary>
        /// 转换成uint
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static uint ToUint(object val, uint defaultValue)
        {
            uint num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is uint)
            {
                return (uint)val;
            }
            if (!uint.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成uint?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static uint? ToUintNullable(object val)
        {
            uint num = ToUint(val);
            if (num.Equals(0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 转换成ushort
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ushort ToUshort(object val)
        {
            return ToUshort(val, 0);
        }

        /// <summary>
        /// 转换成ushort
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static ushort ToUshort(object val, ushort defaultValue)
        {
            ushort num;
            if ((val == null) || (val == DBNull.Value))
            {
                return defaultValue;
            }
            if (val is ushort)
            {
                return (ushort)val;
            }
            if (!ushort.TryParse(val.ToString(), out num))
            {
                return defaultValue;
            }
            return num;
        }

        /// <summary>
        /// 转换成ushort?
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ushort? ToUshortNullable(object val)
        {
            ushort num = ToUshort(val);
            if (num.Equals(0))
            {
                return null;
            }
            return num;
        }

        /// <summary>
        /// 根据日期获取星期几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToWeekByDate(DateTime date)
        {
            string weekstr = date.DayOfWeek.ToString();
            switch (weekstr)
            {
                case "Monday": weekstr = "星期一"; break;
                case "Tuesday": weekstr = "星期二"; break;
                case "Wednesday": weekstr = "星期三"; break;
                case "Thursday": weekstr = "星期四"; break;
                case "Friday": weekstr = "星期五"; break;
                case "Saturday": weekstr = "星期六"; break;
                case "Sunday": weekstr = "星期日"; break;
            }
            return weekstr;
        }

        /// <summary>
        /// 根据日期获取周几
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToWeekDescByDate(DateTime date)
        {
            string weekstr = date.DayOfWeek.ToString();
            switch (weekstr)
            {
                case "Monday": weekstr = "周一"; break;
                case "Tuesday": weekstr = "周二"; break;
                case "Wednesday": weekstr = "周三"; break;
                case "Thursday": weekstr = "周四"; break;
                case "Friday": weekstr = "周五"; break;
                case "Saturday": weekstr = "周六"; break;
                case "Sunday": weekstr = "周日"; break;
            }
            return weekstr;
        }

        /// <summary>
        /// http协议转化成Https协议  实例：http:// to  https://
        /// </summary>
        /// <returns></returns>
        public static string ConvertHttpToHttps(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return string.Empty;
            }
            if (val.ToLower().Contains("http://"))
            {
                val = "//" + val.Substring(7, val.Length - 7);
            }
            return val;
        }

        /// <summary>
        /// string类型数组转化为int类型数组
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static int[] StringsToInts(string[] strs)
        {
            //处理字符串数据为空情况
            int[] ints = new int[0];
            if (strs != null && strs.Any())
            {
                ints = new int[strs.Length];
                for (int i = 0; i < ints.Length; i++)
                {
                    ints[i] = ToInt(strs[i], 0);
                }
                return ints;
            }
            return ints;
        }

        /// <summary>
        ///     把string转化成listint
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static List<int> StringToListInt(string value, char separator)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            var list = new List<int>();
            foreach (var item in value.Split(separator))
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                var id = ToInt(item);
                if (list.Contains(id))
                {
                    continue;
                }
                list.Add(id);
            }

            return list;
        }

        /// <summary>
        /// 把List转化成string类型
        /// </summary>
        /// <param name="valList">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="nDefault">默认值</param>
        /// <returns></returns>
        public static string ListIntToString(List<int> valList, char separator = ',', string nDefault = "")
        {
            if (valList == null || !valList.Any())
            {
                return nDefault;
            }

            var str = valList.Aggregate(string.Empty, (current, item) => current + (item + separator.ToString()));
            return str.TrimEnd(separator);
        }

        /// <summary>
        /// 字符串加*
        /// </summary>
        /// <param name="str">数据源</param>
        /// <param name="leftLength">左边取值</param>
        /// <param name="rightLength">右边取值</param>
        /// <param name="encryptType">加密字符</param>
        /// <returns></returns>
        public static string ConvertStrToEncrypt(string str, int leftLength, int rightLength, string encryptType = "*")
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (leftLength < 1)
            {
                leftLength = 1;
            }
            if (rightLength < 1)
            {
                rightLength = 1;
            }
            var length = leftLength + rightLength;
            var strLength = str.Length;
            if (strLength <= length)
            {
                return str;
            }
            if (string.IsNullOrEmpty(encryptType))
            {
                encryptType = "*";
            }
            var strLeft = str.Substring(0, leftLength);
            var strRight = str.Substring(strLength - rightLength);
            var strMid = string.Empty;
            var midLength = strLength - length;

            if (midLength > 0)
            {
                for (var i = 0; i < midLength; i++)
                {
                    strMid += encryptType;
                }
            }
            return strLeft + strMid + strRight;
        }

        /// <summary>
        /// 数字转化成汉字的方法
        /// </summary>
        /// <param name="n">数字</param>
        /// <param name="fang">是否返回繁体字</param>
        /// <returns></returns>
        public static string ToChinese(int n, bool fang)
        {
            string strn = n.ToString();
            string str = "";
            string nn = "零壹贰叁肆伍陆柒捌玖";
            string ln = "零一二三四五六七八九";

            string mm = "  拾佰仟萬拾佰仟亿拾佰仟萬兆拾佰仟萬亿";
            string lm = "  十百千万十百千亿十百千万兆十百千万亿";

            int i = 0;
            while (i < strn.Length)//>>>>>>>>>>>>>>>>出现空格
            {

                int m = int.Parse(strn.Substring(i, 1));
                if (fang)//返回繁体字
                {
                    str += nn.Substring(m, 1);
                    if (lm.Substring(strn.Length - i, 1) != " ")
                    { str += mm.Substring(strn.Length - i, 1); }
                }
                else//返回简体字
                {
                    str += ln.Substring(m, 1);
                    if (lm.Substring(strn.Length - i, 1) != " ")
                    {
                        str += lm.Substring(strn.Length - i, 1);
                    }
                }
                i++;
            }
            if (str.Substring(str.Length - 1) == "零")
            { str = str.Substring(0, str.Length - 1); }
            if (str.Length > 1 && str.Substring(0, 2) == "一十")
            { str = str.Substring(1); }
            if (str.Length > 1 && str.Substring(0, 2) == "壹拾")
            { str = str.Substring(1); }

            return str;
        }
    }
}
