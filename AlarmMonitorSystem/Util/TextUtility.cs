using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Util
{
    public static class TextUtility
    {
        /// <summary>
        /// テキスト書込み
        /// </summary>
        /// <param name="path">末尾「\\」まで記述</param>
        /// <param name="fileName"></param>
        /// <param name="dataStr"></param>
        public static void Write(string path, string fileName, string dataStr)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using (var writer = new StreamWriter(path + fileName, false, Encoding.UTF8))
            {
                writer.Write(dataStr);
                writer.Close();
            }
        }

        public static string GetMD5HashString(string text)
        {
            // 文字列をバイト型配列に変換する
            byte[] data = Encoding.UTF8.GetBytes(text);

            // MD5ハッシュアルゴリズム生成
            var algorithm = new MD5CryptoServiceProvider();

            // ハッシュ値を計算する
            byte[] bs = algorithm.ComputeHash(data);

            // リソースを解放する
            algorithm.Clear();

            // バイト型配列を16進数文字列に変換
            var result = new StringBuilder();
            foreach (byte b in bs)
            {
                result.Append(b.ToString("X2"));
            }
            return result.ToString();
        }

        public static string TruncateUTF8StringByByteSize(string input, int maxByte)
        {
            // 2019.2.25 H.Naito null対策追加
            if (input == null)
            {
                input = "";
            }

            Encoding encoding = Encoding.GetEncoding(65001,
                EncoderFallback.ExceptionFallback, // エンコード時のエラーは例外に
                new DecoderReplacementFallback("")　// デコード時のエラーは空文字列に置換
            );

            byte[] bytes = encoding.GetBytes(input);
            string output = encoding.GetString(bytes, 0, Math.Min(maxByte, bytes.Length));

            return output;
        }

        /// <summary>
        /// 先頭の文字を小文字に変換する
        /// </summary>
        /// <param name="convertText">変換する文字列</param>
        /// <returns>変換後の文字列</returns>
        public static string ConvertFirstTextLowerCase(string convertText)
        {
            var returnText = string.Empty;
            for (var i = 0; i < convertText.Length; i++)
            {
                if (i == 0) returnText += convertText[i].ToString().ToLower();
                else returnText += convertText[i];
            }

            return returnText;
        }

        public static string DataTableToCSV(DataTable dt)
        {
            if (dt == null || dt.Rows == null || dt.Rows.Count == 0)
                return "";

            StringBuilder csv = new StringBuilder(4096);

            bool first = true;
            foreach (DataRow row in dt.Rows)
            {
                if (first == true)
                {
                    for (int i = 0; i < row.Table.Columns.Count; i++)
                    {
                        if (i != 0)
                            csv.Append(",");

                        csv.Append(row.Table.Columns[i]);
                    }
                    csv.Append("\r\n");
                    first = false;
                }
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (i != 0)
                        csv.Append(",");
                    csv.Append(row.ItemArray[i]!.ToString());
                }
                csv.Append("\r\n");
            }

            return csv.ToString();
        }
    }
}