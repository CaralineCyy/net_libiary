/// <summary>
/// ��˵����SharpZip
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

///ѹ������ѹ����
namespace DotNet.Utilities
{
    public class SharpZip
    {
        public SharpZip()
        { }

        /// <summary>
        /// ѹ��
        /// </summary> 
        /// <param name="filename"> ѹ������ļ���(��������·��)</param>
        /// <param name="directory">��ѹ�����ļ���(��������·��)</param>
        public static void PackFiles(string filename, string directory)
        {
            try
            {
                FastZip fz = new FastZip();
                fz.CreateEmptyDirectories = true;
                fz.CreateZip(filename, directory, true, "");
                fz = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ��ѹ��
        /// </summary>
        /// <param name="file">����ѹ�ļ���(��������·��)</param>
        /// <param name="dir"> ��ѹ���ĸ�Ŀ¼��(��������·��)</param>
        public static bool UnpackFiles(string file, string dir)
        {
            try
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                ZipInputStream s = new ZipInputStream(File.OpenRead(file));
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (directoryName != String.Empty)
                    {
                        Directory.CreateDirectory(dir + directoryName);
                    }
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(dir + theEntry.Name);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
                s.Close();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ClassZip
    {
        #region ˽�з���
        /// <summary>
        /// �ݹ�ѹ���ļ��з���
        /// </summary>
        private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/"));
                s.PutNextEntry(entry);
                s.Flush();
                filenames = Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip) + "/" + Path.GetFileName(file)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            folders = Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, Path.Combine(ParentFolderName, Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }
            return res;
        }

        /// <summary>
        /// ѹ��Ŀ¼
        /// </summary>
        /// <param name="FolderToZip">��ѹ�����ļ��У�ȫ·����ʽ</param>
        /// <param name="ZipedFile">ѹ������ļ�����ȫ·����ʽ</param>
        private static bool ZipFileDictory(string FolderToZip, string ZipedFile, int level)
        {
            bool res;
            if (!Directory.Exists(FolderToZip))
            {
                return false;
            }
            ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
            s.SetLevel(level);
            res = ZipFileDictory(FolderToZip, s, "");
            s.Finish();
            s.Close();
            return res;
        }

        /// <summary>
        /// ѹ���ļ�
        /// </summary>
        /// <param name="FileToZip">Ҫ����ѹ�����ļ���</param>
        /// <param name="ZipedFile">ѹ�������ɵ�ѹ���ļ���</param>
        private static bool ZipFile(string FileToZip, string ZipedFile, int level)
        {
            if (!File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("ָ��Ҫѹ�����ļ�: " + FileToZip + " ������!");
            }
            FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;
            bool res = true;
            try
            {
                ZipFile = File.OpenRead(FileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();

                ZipFile = File.Create(ZipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                ZipEntry = new ZipEntry(Path.GetFileName(FileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(level);

                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return res;
        }
        #endregion

        /// <summary>
        /// ѹ��
        /// </summary>
        /// <param name="FileToZip">��ѹ�����ļ�Ŀ¼</param>
        /// <param name="ZipedFile">���ɵ�Ŀ���ļ�</param>
        /// <param name="level">6</param>
        public static bool Zip(String FileToZip, String ZipedFile, int level)
        {
            if (Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, level);
            }
            else if (File.Exists(FileToZip))
            {
                return ZipFile(FileToZip, ZipedFile, level);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ��ѹ
        /// </summary>
        /// <param name="FileToUpZip">����ѹ���ļ�</param>
        /// <param name="ZipedFolder">��ѹĿ����Ŀ¼</param>
        public static void UnZip(string FileToUpZip, string ZipedFolder)
        {
            if (!File.Exists(FileToUpZip))
            {
                return;
            }
            if (!Directory.Exists(ZipedFolder))
            {
                Directory.CreateDirectory(ZipedFolder);
            }
            ZipInputStream s = null;
            ZipEntry theEntry = null;
            string fileName;
            FileStream streamWriter = null;
            try
            {
                s = new ZipInputStream(File.OpenRead(FileToUpZip));
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = Path.Combine(ZipedFolder, theEntry.Name);
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }
                        streamWriter = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }
    }

    public class ZipHelper
    {
        #region ˽�б���
        String the_rar;
        RegistryKey the_Reg;
        Object the_Obj;
        String the_Info;
        ProcessStartInfo the_StartInfo;
        Process the_Process;
        #endregion

        /// <summary>
        /// ѹ��
        /// </summary>
        /// <param name="zipname">Ҫ��ѹ���ļ���</param>
        /// <param name="zippath">Ҫѹ�����ļ�Ŀ¼</param>
        /// <param name="dirpath">��ʼĿ¼</param>
        public void EnZip(string zipname, string zippath, string dirpath)
        {
            try
            {
                the_Reg = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\Shell\Open\Command");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                the_Info = " a    " + zipname + "  " + zippath;
                the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                the_StartInfo.WorkingDirectory = dirpath;
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ��ѹ��
        /// </summary>
        /// <param name="zipname">Ҫ��ѹ���ļ���</param>
        /// <param name="zippath">Ҫ��ѹ���ļ�·��</param>
        public void DeZip(string zipname, string zippath)
        {
            try
            {
                the_Reg = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRar.exe\Shell\Open\Command");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                the_Info = " X " + zipname + " " + zippath;
                the_StartInfo = new ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                the_Process = new Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}