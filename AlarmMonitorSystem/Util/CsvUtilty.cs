using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Util
{
    public static class CsvUtilty
    {
        /// <summary>
        /// ファイルパスを取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subFolser"></param>
        /// <returns></returns>
        public static string GetFilePath(string fileName, string subFolser)
        {
            Assembly? myAssembly = Assembly.GetEntryAssembly();
            //SAPTestServerの場合、取得できないので追加
            if (null == myAssembly) myAssembly = Assembly.GetExecutingAssembly();
            string path = myAssembly.Location;
            string filePath = "";
            while (true)
            {
                var dirInfo = Directory.GetParent(path);
                if (null == dirInfo)
                    return "";
                path = dirInfo.FullName;
                filePath = path + "\\" + fileName;
                if (File.Exists(filePath)) return filePath;

                filePath = path + "\\" + subFolser + "\\" + fileName;
                if (File.Exists(filePath)) return filePath;
                if (fileName.Length == 0)
                {
                    if (Directory.Exists(filePath)) return filePath;
                }
            }
        }

        /// <summary>
        /// ファイルの更新時刻を取得
        /// </summary>
        /// <returns></returns>
        public static DateTime? GetFileUpdateTime(string fileName, string subFolser)
        {
            try
            {
                var path = CsvUtilty.GetFilePath(fileName, subFolser);
                return File.GetLastWriteTime(path);
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// CSV文字列の取得
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subFolser"></param>
        /// <returns></returns>
        public static string GetCsvString(string fileName, string subFolser)
        {
            string text = "";
            using (FileStream stream = new FileStream(CsvUtilty.GetFilePath(fileName, subFolser), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    text = sr.ReadToEnd();

                    sr.Close();
                }
            }
            return text;
        }
        /*
                public static bool WriteClassData<T>(string path, List<List<string>> headers, List<T> classlist)
                {
                    StringBuilder csv = new StringBuilder(4096);
                    for (int i = 0; i < headers.Count; i++)
                    {
                        for (int j = 0; j < headers[i].Count; j++)
                        {
                            if (j != 0)
                                csv.Append(",");
                            csv.Append(headers[i][j]);
                        }
                        csv.Append("\r\n");
                    }
                    for (int i = 0; i < classlist.Count; i++)
                    {
                        for ( int j=0; j < classlist[i].Count; j++)
                        {

                        csv.Append(newRows[i].ToCSV());
                        csv.Append("\r\n");
                    }
                    return true;
                }
        */
        /// <summary>
        /// CSVデータを書込む
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="subFolser"></param>
        /// <param name="csvString"></param>
        public static void WriteFile(string fileName, string subFolser, string csvString)
        {
            Assembly? myAssembly = Assembly.GetEntryAssembly()!;
            var path = GetFilePath("", subFolser);
            if (path.Length == 0)
                path = Directory.GetParent(myAssembly.Location)?.FullName + "\\" + subFolser + "\\";
            TextUtility.Write(path, fileName, csvString);
        }

        public static string ToCSVItem(string item)
        {
            if (item == null)
                return "";
            if (item.IndexOf(",") == -1 && item.IndexOf("\"") == -1)
                return item;
            return "\"" + item.Replace("\"", "\"\"") + "\"";
        }

        /// <summary>
        /// １行のCSVデータを分割する
        /// </summary>
        /// <param name="line"></param>
        /// <param name="delimiter"></param>
        public static string[] Split(string line, string? delimiter = null)
        {
            if (null == delimiter || delimiter.Length == 0)
                delimiter = Delimiter.ToString();

            Regex reg = new Regex(delimiter + "(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
            string[] elem = reg.Split(line);
            for (int i = 0; i < elem.Length; i++)
            {
                if (elem[i].IndexOf("\"") != -1)
                {
                    elem[i] = elem[i].Replace("\"\"", "\"");
                    elem[i] = elem[i].TrimStart('"');
                    elem[i] = elem[i].TrimEnd('"');
                }
            }
            /*
            Regex regdq = new Regex("\\s*\"(?<text>[^\"]*)\"\\s*$");
            for (int i = 0; i < elem.Length; i++)
            {
                Match match = regdq.Match(elem[i]);
                if (match.Success == true)
                {
                    elem[i] = match.Groups["text"].Value;
                }
            }
            */
            return elem;
        }

        /// <summary>
        /// クラスデータを取得する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="subFolser"></param>
        /// <param name="headerIndex"></param>
        /// <param name="func">クラス変換の処理</param>
        /// <returns></returns>
        public static List<T> GetClassData<T>(string fileName, string subFolser, int headerIndex, out List<List<string>> headers, Func<string, string, T> func) where T : class, new()
        {
            headers = new List<List<String>>();
            var result = new List<T>();
            //int index = 1;

            var path = GetFilePath(fileName, subFolser);
            if (0 == path.Length)
                return result;

            return GetClassData<T>(path, headerIndex, out headers, func);
        }

        /// <summary>
        /// クラスデータを取得する（CSVファイルのフルパスを指定）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="headerIndex"></param>
        /// <param name="func">クラス変換の処理</param>
        /// <returns></returns>
        public static List<T> GetClassData<T>(string path, int headerIndex, out List<List<string>> headers, Func<string, string, T> func) where T : class, new()
        {
            headers = new List<List<String>>();
            var result = new List<T>();
            int index = 1;

            string? headerStr = string.Empty;

            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(stream))
                {
                    // ストリームの末尾まで繰り返す
                    bool first = true;
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine() ?? "";
                        if (first)
                        {
                            if (index <= headerIndex)
                            {
                                index++;
                                headerStr = line;

                                headers.Add(Split(line).ToList());
                                //headers.Add(line.Split(Delimiter).ToList());
                                continue;
                            }
                            first = false;
                        }

                        var values = line;

                        result.Add(func(headerStr, values));
                    }
                }
            }

            return result;
        }

        public static char Delimiter = ',';

        /// <summary>
        /// 汎用クラス変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="headerStr"></param>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static T? CreateClassData<T>(string headerStr, string valueStr) where T : class, new()
        {
            var result = new T();

            var headers = Split(headerStr);
            var values = Split(valueStr);
            //var headers = headerStr.Split(Delimiter);
            //var values = valueStr.Split(Delimiter);

            if (values.Length != headers.Length) return null;

            var dataIndex = 0;

            foreach (var header in headers)
            {
                // プロパティ情報の取得
                var property = typeof(T).GetProperty(header);
                if (property == null) continue;

                object? value = null;

                if (property.PropertyType.IsGenericType)
                {
                    if (property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (string.IsNullOrEmpty(values[dataIndex])) continue;

                        var orgPropertyType = Nullable.GetUnderlyingType(property.PropertyType)!;
                        value = Convert.ChangeType(values[dataIndex], orgPropertyType);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    value = Convert.ChangeType(values[dataIndex], property.PropertyType);
                }

                // インスタンスに値を設定
                property.SetValue(result, value);

                dataIndex++;
            }

            return result;
        }

        /// <summary>
        /// SOAP用クラス変換
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="headerStr"></param>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static T? CreateSoapClassData<T>(string headerStr, string valueStr) where T : class, new()
        {
            var result = new T();

            var headers = Split(headerStr);
            var values = Split(valueStr);
            //var headers = headerStr.Split(Delimiter);
            //var values = valueStr.Split(Delimiter);

            if (values.Length != headers.Length) return null;

            var dataIndex = 0;

            foreach (var header in headers)
            {
                // プロパティ情報の取得
                var property = typeof(T).GetProperty(header);
                if (property == null) continue;

                object? value = null;

                if (property.PropertyType.IsGenericType)
                {
                    if (property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (string.IsNullOrEmpty(values[dataIndex])) continue;

                        var orgPropertyType = Nullable.GetUnderlyingType(property.PropertyType)!;
                        value = Convert.ChangeType(values[dataIndex], orgPropertyType);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    //数値型のデータ有無フラグの設定
                    var specifiedProperty = typeof(T).GetProperty(header + "Specified");
                    if (null != specifiedProperty)
                    {
                        if (!string.IsNullOrEmpty(values[dataIndex]))
                        {
                            specifiedProperty.SetValue(result, true);
                            value = Convert.ChangeType(values[dataIndex], property.PropertyType);
                        }
                        else
                        {
                            specifiedProperty.SetValue(result, false);
                        }
                    }
                    else
                    {
                        value = Convert.ChangeType(values[dataIndex], property.PropertyType);
                    }
                }

                // インスタンスに値を設定
                property.SetValue(result, value);

                dataIndex++;
            }

            return result;
        }
    }
}