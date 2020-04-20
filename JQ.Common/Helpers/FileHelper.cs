using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace JQ.Common.Helpers
{
    /// <summary>
    /// 文件操作助手类 有空在进行补充
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 获取文件内的数据进行返回
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string FileToString(string filePath)
        {
            StreamReader reader = new StreamReader(filePath, Encoding.Default);
            try
            {
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
        }


        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>        
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        /// <summary>
        /// 同步标识
        /// </summary>
        private static Object sync = new object();

        /// <summary>
        /// 获取目录下全部文件的文件名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetAllFileName(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            return root.GetFiles().Select(d => d.Name).ToList();
        }

        /// <summary>
        /// 从文件绝对路径中获取目录路径
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetDirectoryFromFilePath(string filePath)
        {
            //实例化文件
            FileInfo file = new FileInfo(filePath);
            //获取目录信息
            DirectoryInfo directory = file.Directory;
            //返回目录路径
            return directory.FullName;
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool CreateFile(IFormFile file, string fileDir, string fileNameFill = "")
        {
            var isSucc = false;

            try
            {
                if (file != null)
                {
                    CreateDirectory(fileDir);
                    var filePath = fileDir + fileNameFill + file.FileName;
                    using (FileStream fs = File.Create(filePath))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    isSucc = true;
                }
            }
            catch
            {
            }
            return isSucc;
        }


        /// <summary>
        /// 创建一个文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                if (!IsExistFile(filePath))
                {
                    string directoryPath = GetDirectoryFromFilePath(filePath);
                    CreateDirectory(directoryPath);
                    lock (sync)
                    {
                        //创建文件                    
                        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 创建一个文件,并将字符串写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">字符串数据</param>
        /// <param name="encoding">字符编码</param>
        public static void CreateFile(string filePath, string text, Encoding encoding)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath))
                {
                    //获取文件目录路径
                    string directoryPath = GetDirectoryFromFilePath(filePath);
                    //如果文件的目录不存在，则创建目录
                    CreateDirectory(directoryPath);
                    //创建文件
                    FileInfo file = new FileInfo(filePath);
                    using (FileStream stream = file.Create())
                    {
                        using (StreamWriter writer = new StreamWriter(stream, encoding))
                        {
                            //写入字符串     
                            writer.Write(text);
                            //输出
                            writer.Flush();
                        }
                    }
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>        
        public static void WriteText(string filePath, string text)
        {
            WriteText(filePath, text, Encoding.UTF8);
        }
        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath, string text, Encoding encoding)
        {
            //向文件写入内容
            File.WriteAllText(filePath, text, encoding);
        }

        public static string FileDelete(string filePath)
        {
            string result = "";
            try
            {
                if (IsExistFile(filePath))
                {
                    File.Delete(filePath);
                    result = "success";
                }
                else
                {
                    result = "文件不存在";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 将源文件的内容复制到目标文件中
        /// </summary>
        /// <param name="sourceFilePath">源文件的绝对路径</param>
        /// <param name="destFilePath">目标文件的绝对路径</param>
        public static bool Copy(string sourceFilePath, string destFilePath)
        {
            //有效性检测
            if (!IsExistFile(sourceFilePath))
            {
                return false;
            }
            try
            {
                //检测目标文件的目录是否存在，不存在则创建
                string destDirectoryPath = GetDirectoryFromFilePath(destFilePath);
                CreateDirectory(destDirectoryPath);
                //复制文件
                FileInfo file = new FileInfo(sourceFilePath);
                try
                {
                    file.CopyTo(destFilePath, true);
                }
                catch
                {

                }
                return true;
            }
            catch
            {

                return false;
            }
        }


        /// <summary>
        /// 删除目录下除了notDeleteFileName的所有文件
        /// </summary>
        /// <param name="srcPath"></param>
        public static string DelectDir(string srcPath, string notDeleteFileName = "", string notDeleteDirName = "")
        {
            string result = "";
            if (Directory.Exists(srcPath))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)
                        {
                            if (i.FullName != notDeleteDirName)
                            {
                                DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                                subdir.Delete(true);
                            }
                        }
                        else
                        {
                            if (i.Name != notDeleteFileName)
                            {
                                File.Delete(i.FullName);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    result = "删除失败!" + e.Message;
                }
            }
            return result;
        }

        public static string DelectDirectoryInfo(string srcPath, string notDeleteDirName = "")
        {
            string result = "";
            if (Directory.Exists(srcPath))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)
                        {
                            if (i.Name != notDeleteDirName)
                            {
                                DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                                subdir.Delete(true);
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    result = "删除失败!" + e.Message;
                }
            }
            return result;
        }
        public static string DelectDir(string srcPath, List<string> notDeleteFileNameList)
        {
            string result = "";
            if (Directory.Exists(srcPath))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);
                        }
                        else
                        {
                            if (!notDeleteFileNameList.Contains(i.Name))
                            {
                                File.Delete(i.FullName);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    result = "删除失败!" + e.Message;
                }
            }
            return result;
        }





    }
}
