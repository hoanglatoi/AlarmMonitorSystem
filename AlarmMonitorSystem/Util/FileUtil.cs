using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlarmMonitorSystem.Util
{
    public class FileUtil
    {
        private static readonly log4net.ILog _logger
           = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod()?.DeclaringType);

        /// <summary>
        /// 指定されたファイルがロックされているかどうかを返します。
        /// </summary>
        /// <param name="path">検証したいファイルへのフルパス</param>
        /// <returns>ロックされているかどうか</returns>
        static public bool IsFileLocked(string path)
        {
            FileStream? stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException se) when ((se.HResult & 0x0000FFFF) == 32)
            {
                // sharing violation. エラーでは無いがファイルが無い場合もtrueが返る
                _logger.Debug(se.Message);
                return true;
            }
            catch (System.IO.IOException ioe)
            {
                _logger.Error(ioe.Message);
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return false;
        }


        /// <summary>
        /// ファイルを移動します。
        /// </summary>
        /// <param name="src">移動元</param>
        /// <param name="dst">移動先</param>
        /// <remarks>
        /// IOException                 リンク先ファイルが既に存在します。またはsourceFileName が見つかりませんでした。
        /// ArgumentNullException       sourceFileName または destFileName が null です。
        /// ArgumentException           sourceFileName か destFileName が長さ 0 の文字列であるか、空白のみで構成されているか、または InvalidPathChars で定義されている正しくない文字を含んでいます。
        /// UnauthorizedAccessException 呼び出し元に、必要なアクセス許可がありません。
        /// PathTooLongException        指定されたパスかファイル名、またはその両方がシステム定義の最大文字数を超えています。 たとえば、Windows ベースのプラットフォームでは、パスは 248 文字、ファイル名は 260 文字未満でなければなりません。
        /// DirectoryNotFoundException  sourceFileName または destFileName で指定されたパスが正しくありません(マップされていないドライブ上のパスなど)。
        /// NotSupportedException       sourceFileName または destFileName の形式が正しくありません。
        /// </remarks>
        static public void FileMove(string src, string dst)
        {
            System.IO.File.Move(src, dst);
        }

        /// <summary>
        /// ファイルをコピーします。
        /// </summary>
        /// <param name="src">移動元</param>
        /// <param name="dst">移動先</param>
        /// <remarks>
        /// IOException                 リンク先ファイルが既に存在します。またはsourceFileName が見つかりませんでした。
        /// ArgumentNullException       sourceFileName または destFileName が null です。
        /// ArgumentException           sourceFileName か destFileName が長さ 0 の文字列であるか、空白のみで構成されているか、または InvalidPathChars で定義されている正しくない文字を含んでいます。
        /// UnauthorizedAccessException 呼び出し元に、必要なアクセス許可がありません。
        /// PathTooLongException        指定されたパスかファイル名、またはその両方がシステム定義の最大文字数を超えています。 たとえば、Windows ベースのプラットフォームでは、パスは 248 文字、ファイル名は 260 文字未満でなければなりません。
        /// DirectoryNotFoundException  sourceFileName または destFileName で指定されたパスが正しくありません(マップされていないドライブ上のパスなど)。
        /// NotSupportedException       sourceFileName または destFileName の形式が正しくありません。
        /// </remarks>
        static public void FileCopy(string src, string dst)
        {
            System.IO.File.Copy(src, dst);
        }


        /// <summary>
        /// ファイルの存在を確認します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <remarks>
        /// </remarks>
        static public bool FileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        static public System.IO.FileInfo FileInfo(string filePath)
        {
            return new System.IO.FileInfo(filePath);
        }

        static public bool CreateDirectoryIfNotExist(string folderPath)
        {
            if (false == System.IO.File.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex.Message);
                    return false;
                }
            }
            return true;
        }
        public static string? GetFolder(string fullpath)
        {
            return System.IO.Path.GetDirectoryName(fullpath);
        }

        public static string? FinalFolder(string fullpath)
        {
            return System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(fullpath));
        }

        public static string GetFileName(string fullpath)
        {
            return System.IO.Path.GetFileName(fullpath);
        }
        public static string GetFileNameWithoutExtension(string fullpath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(fullpath);
        }
        public static string GetExtension(string fullpath)
        {
            return System.IO.Path.GetExtension(fullpath);
        }
        public static bool CopyFileWithTimestamp(string src, string subfolder)
        {
            try
            {

                DateTime dt = DateTime.Now;
                string bakpath = FileUtil.GetFolder(src) + "\\";
                if (subfolder != null && subfolder.Length > 0)
                {
                    bakpath += subfolder + "\\";
                    FileUtil.CreateDirectoryIfNotExist(bakpath);
                }
                bakpath += FileUtil.GetFileNameWithoutExtension(src) + "_" + dt.ToString("yyyyMMddHHmmss");
                bakpath += FileUtil.GetExtension(src);
                FileUtil.FileCopy(src, bakpath);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// BOMからエンコーディングを判別します。
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <remarks>
        /// </remarks>
        public static System.Text.Encoding? BOMCheck(string filePath)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);

            if (bytes.Length < 2)
            {
                return null;
            }
            if ((bytes[0] == 0xfe) && (bytes[1] == 0xff))
            {
                //UTF-16 BE
                return new System.Text.UnicodeEncoding(true, true);
            }
            if ((bytes[0] == 0xff) && (bytes[1] == 0xfe))
            {
                if ((4 <= bytes.Length) &&
                    (bytes[2] == 0x00) && (bytes[3] == 0x00))
                {
                    //UTF-32 LE
                    return new System.Text.UTF32Encoding(false, true);
                }
                //UTF-16 LE
                return new System.Text.UnicodeEncoding(false, true);
            }
            if (bytes.Length < 3)
            {
                return null;
            }
            if ((bytes[0] == 0xef) && (bytes[1] == 0xbb) && (bytes[2] == 0xbf))
            {
                //UTF-8
                return new System.Text.UTF8Encoding(true, true);
            }
            if (bytes.Length < 4)
            {
                return null;
            }
            if ((bytes[0] == 0x00) && (bytes[1] == 0x00) &&
                (bytes[2] == 0xfe) && (bytes[3] == 0xff))
            {
                //UTF-32 BE
                return new System.Text.UTF32Encoding(true, true);
            }

            return null;
        }

        const int retrycnt = 3;
        const int sleeptime = 500;
        /// <summary>
        /// アトミックな操作でpathにdataを書き込む
        /// </summary>
        /// <param name="temppath"></param>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="exmsg"></param>
        /// <returns>true: 書き込み成功 false:書き込み失敗 exmsg:例外内容</returns>
        public static bool AtomicFileWrite(string temppath, string path, string data, out string exmsg)
        {
            exmsg = "";
            File.WriteAllText(temppath, data);
            for (int i = 0; i < retrycnt; i++)
            {
                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                    File.Move(temppath, path);
                    return true;
                }
                catch (IOException ex)
                {
                    if (i < retrycnt - 1)
                    {
                        System.Threading.Thread.Sleep(sleeptime);
                        continue;
                    }
                    else
                    {
                        exmsg = ex.Message;
                        if (File.Exists(temppath))
                            File.Delete(temppath);
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    exmsg = ex.ToString();
                    if (File.Exists(temppath))
                        File.Delete(temppath);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 指定ヶ月前の作成日時のファイルを一括削除
        /// 基本バックアップフォルダを指定してバックアップファイルを削除
        /// </summary>
        /// <param name="src">元ファイル</param>
        /// <param name="subfolder">削除対象フォルダパス</param>
        /// <param name="month">削除期間閾値</param>
        public static void DeleteOldFiles(string src, string subfolder, int month)
        {
            string path = FileUtil.GetFolder(src) + "\\";
            if (subfolder != null && subfolder.Length > 0)
            {
                path += subfolder + "\\";
                FileUtil.CreateDirectoryIfNotExist(path);
            }
            DateTime deleteLineDt = DateTime.Now.AddMonths(-month);
            DirectoryInfo dyInfo = new DirectoryInfo(path);
            // フォルダのファイルを取得
            foreach (FileInfo fInfo in dyInfo.GetFiles())
            {
                // 日付の比較
                if (fInfo.CreationTime < deleteLineDt)
                {
                    fInfo.Delete();
                }
            }
        }

    }
}