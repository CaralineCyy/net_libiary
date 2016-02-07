/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Text;
using System.Web;
using System.IO;

namespace DotNet.Utilities
{
    public class FileOperateHelper
    {
        #region д�ļ�
        protected void Write_Txt(string FileName, string Content)
        {
            Encoding code = Encoding.GetEncoding("gb2312");
            string htmlfilename = HttpContext.Current.Server.MapPath("Precious\\" + FileName + ".txt");��//�����ļ���·��
            string str = Content;
            StreamWriter sw = null;
            {
                try
                {
                    sw = new StreamWriter(htmlfilename, false, code);
                    sw.Write(str);
                    sw.Flush();
                }
                catch { }
            }
            sw.Close();
            sw.Dispose();

        }
        #endregion

        #region ���ļ�
        protected string Read_Txt(string filename)
        {

            Encoding code = Encoding.GetEncoding("gb2312");
            string temp = HttpContext.Current.Server.MapPath("Precious\\" + filename + ".txt");
            string str = "";
            if (File.Exists(temp))
            {
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(temp, code);
                    str = sr.ReadToEnd(); // ��ȡ�ļ�
                }
                catch { }
                sr.Close();
                sr.Dispose();
            }
            else
            {
                str = "";
            }


            return str;
        }
        #endregion

        #region ȡ���ļ���׺��
        /****************************************
         * �������ƣ�GetPostfixStr
         * ����˵����ȡ���ļ���׺��
         * ��    ����filename:�ļ�����
         * ����ʾ�У�
         *           string filename = "aaa.aspx";        
         *           string s = DotNet.Utilities.FileOperate.GetPostfixStr(filename);         
        *****************************************/
        /// <summary>
        /// ȡ��׺��
        /// </summary>
        /// <param name="filename">�ļ���</param>
        /// <returns>.gif|.html��ʽ</returns>
        public static string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            int length = filename.Length;
            string postfix = filename.Substring(start, length - start);
            return postfix;
        }
        #endregion

        #region д�ļ�
        /****************************************
         * �������ƣ�WriteFile
         * ����˵�������ļ�����ʱ���򴴽��ļ�����׷���ļ�
         * ��    ����Path:�ļ�·��,Strings:�ı�����
         * ����ʾ�У�
         *           string Path = Server.MapPath("Default2.aspx");       
         *           string Strings = "������д�����ݰ�";
         *           DotNet.Utilities.FileOperate.WriteFile(Path,Strings);
        *****************************************/
        /// <summary>
        /// д�ļ�
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <param name="Strings">�ļ�����</param>
        public static void WriteFile(string Path, string Strings)
        {

            if (!System.IO.File.Exists(Path))
            {
                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();
                f.Dispose();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, true, System.Text.Encoding.UTF8);
            f2.WriteLine(Strings);
            f2.Close();
            f2.Dispose();


        }
        #endregion

        #region ���ļ�
        /****************************************
         * �������ƣ�ReadFile
         * ����˵������ȡ�ı�����
         * ��    ����Path:�ļ�·��
         * ����ʾ�У�
         *           string Path = Server.MapPath("Default2.aspx");       
         *           string s = DotNet.Utilities.FileOperate.ReadFile(Path);
        *****************************************/
        /// <summary>
        /// ���ļ�
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <returns></returns>
        public static string ReadFile(string Path)
        {
            string s = "";
            if (!System.IO.File.Exists(Path))
                s = "��������Ӧ��Ŀ¼";
            else
            {
                StreamReader f2 = new StreamReader(Path, System.Text.Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }

            return s;
        }
        #endregion

        #region ׷���ļ�
        /****************************************
         * �������ƣ�FileAdd
         * ����˵����׷���ļ�����
         * ��    ����Path:�ļ�·��,strings:����
         * ����ʾ�У�
         *           string Path = Server.MapPath("Default2.aspx");     
         *           string Strings = "��׷������";
         *           DotNet.Utilities.FileOperate.FileAdd(Path, Strings);
        *****************************************/
        /// <summary>
        /// ׷���ļ�
        /// </summary>
        /// <param name="Path">�ļ�·��</param>
        /// <param name="strings">����</param>
        public static void FileAdd(string Path, string strings)
        {
            StreamWriter sw = File.AppendText(Path);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        #endregion

        #region �����ļ�
        /****************************************
         * �������ƣ�FileCoppy
         * ����˵���������ļ�
         * ��    ����OrignFile:ԭʼ�ļ�,NewFile:���ļ�·��
         * ����ʾ�У�
         *           string OrignFile = Server.MapPath("Default2.aspx");     
         *           string NewFile = Server.MapPath("Default3.aspx");
         *           DotNet.Utilities.FileOperate.FileCoppy(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// �����ļ�
        /// </summary>
        /// <param name="OrignFile">ԭʼ�ļ�</param>
        /// <param name="NewFile">���ļ�·��</param>
        public static void FileCoppy(string OrignFile, string NewFile)
        {
            File.Copy(OrignFile, NewFile, true);
        }

        #endregion

        #region ɾ���ļ�
        /****************************************
         * �������ƣ�FileDel
         * ����˵����ɾ���ļ�
         * ��    ����Path:�ļ�·��
         * ����ʾ�У�
         *           string Path = Server.MapPath("Default3.aspx");    
         *           DotNet.Utilities.FileOperate.FileDel(Path);
        *****************************************/
        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="Path">·��</param>
        public static void FileDel(string Path)
        {
            File.Delete(Path);
        }
        #endregion

        #region �ƶ��ļ�
        /****************************************
         * �������ƣ�FileMove
         * ����˵�����ƶ��ļ�
         * ��    ����OrignFile:ԭʼ·��,NewFile:���ļ�·��
         * ����ʾ�У�
         *            string OrignFile = Server.MapPath("../˵��.txt");    
         *            string NewFile = Server.MapPath("../../˵��.txt");
         *            DotNet.Utilities.FileOperate.FileMove(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// �ƶ��ļ�
        /// </summary>
        /// <param name="OrignFile">ԭʼ·��</param>
        /// <param name="NewFile">��·��</param>
        public static void FileMove(string OrignFile, string NewFile)
        {
            File.Move(OrignFile, NewFile);
        }
        #endregion

        #region �ڵ�ǰĿ¼�´���Ŀ¼
        /****************************************
         * �������ƣ�FolderCreate
         * ����˵�����ڵ�ǰĿ¼�´���Ŀ¼
         * ��    ����OrignFolder:��ǰĿ¼,NewFloder:��Ŀ¼
         * ����ʾ�У�
         *           string OrignFolder = Server.MapPath("test/");    
         *           string NewFloder = "new";
         *           DotNet.Utilities.FileOperate.FolderCreate(OrignFolder, NewFloder); 
        *****************************************/
        /// <summary>
        /// �ڵ�ǰĿ¼�´���Ŀ¼
        /// </summary>
        /// <param name="OrignFolder">��ǰĿ¼</param>
        /// <param name="NewFloder">��Ŀ¼</param>
        public static void FolderCreate(string OrignFolder, string NewFloder)
        {
            Directory.SetCurrentDirectory(OrignFolder);
            Directory.CreateDirectory(NewFloder);
        }

        /// <summary>
        /// �����ļ���
        /// </summary>
        /// <param name="Path"></param>
        public static void FolderCreate(string Path)
        {
            // �ж�Ŀ��Ŀ¼�Ƿ����������������½�֮
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);
        }

        #endregion

        #region ����Ŀ¼
        public static void FileCreate(string Path)
        {
            FileInfo CreateFile = new FileInfo(Path); //�����ļ� 
            if (!CreateFile.Exists)
            {
                FileStream FS = CreateFile.Create();
                FS.Close();
            }
        }
        #endregion

        #region �ݹ�ɾ���ļ���Ŀ¼���ļ�
        /****************************************
         * �������ƣ�DeleteFolder
         * ����˵�����ݹ�ɾ���ļ���Ŀ¼���ļ�
         * ��    ����dir:�ļ���·��
         * ����ʾ�У�
         *           string dir = Server.MapPath("test/");  
         *           DotNet.Utilities.FileOperate.DeleteFolder(dir);       
        *****************************************/
        /// <summary>
        /// �ݹ�ɾ���ļ���Ŀ¼���ļ�
        /// </summary>
        /// <param name="dir"></param>  
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //�����������ļ���ɾ��֮ 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //ֱ��ɾ�����е��ļ�                        
                    else
                        DeleteFolder(d); //�ݹ�ɾ�����ļ��� 
                }
                Directory.Delete(dir, true); //ɾ���ѿ��ļ���                 
            }
        }

        #endregion

        #region ��ָ���ļ����������������copy��Ŀ���ļ������� ��Ŀ���ļ���Ϊֻ�����Ծͻᱨ��
        /****************************************
         * �������ƣ�CopyDir
         * ����˵������ָ���ļ����������������copy��Ŀ���ļ������� ��Ŀ���ļ���Ϊֻ�����Ծͻᱨ��
         * ��    ����srcPath:ԭʼ·��,aimPath:Ŀ���ļ���
         * ����ʾ�У�
         *           string srcPath = Server.MapPath("test/");  
         *           string aimPath = Server.MapPath("test1/");
         *           DotNet.Utilities.FileOperate.CopyDir(srcPath,aimPath);   
        *****************************************/
        /// <summary>
        /// ָ���ļ����������������copy��Ŀ���ļ�������
        /// </summary>
        /// <param name="srcPath">ԭʼ·��</param>
        /// <param name="aimPath">Ŀ���ļ���</param>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // ���Ŀ��Ŀ¼�Ƿ���Ŀ¼�ָ��ַ�����������������֮
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // �ж�Ŀ��Ŀ¼�Ƿ����������������½�֮
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // �õ�ԴĿ¼���ļ��б��������ǰ����ļ��Լ�Ŀ¼·����һ������
                //�����ָ��copyĿ���ļ�������ļ���������Ŀ¼��ʹ������ķ���
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //�������е��ļ���Ŀ¼
                foreach (string file in fileList)
                {
                    //�ȵ���Ŀ¼��������������Ŀ¼�͵ݹ�Copy��Ŀ¼������ļ�

                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    //����ֱ��Copy�ļ�
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        #endregion

        #region ��ȡָ���ļ�����������Ŀ¼���ļ�(����)
        /****************************************
         * �������ƣ�GetFoldAll(string Path)
         * ����˵������ȡָ���ļ�����������Ŀ¼���ļ�(����)
         * ��    ����Path:��ϸ·��
         * ����ʾ�У�
         *           string strDirlist = Server.MapPath("templates");       
         *           this.Literal1.Text = DotNet.Utilities.FileOperate.GetFoldAll(strDirlist);  
        *****************************************/
        /// <summary>
        /// ��ȡָ���ļ�����������Ŀ¼���ļ�
        /// </summary>
        /// <param name="Path">��ϸ·��</param>
        public static string GetFoldAll(string Path)
        {

            string str = "";
            DirectoryInfo thisOne = new DirectoryInfo(Path);
            str = ListTreeShow(thisOne, 0, str);
            return str;

        }

        /// <summary>
        /// ��ȡָ���ļ�����������Ŀ¼���ļ�����
        /// </summary>
        /// <param name="theDir">ָ��Ŀ¼</param>
        /// <param name="nLevel">Ĭ����ʼֵ,����ʱ,һ��Ϊ0</param>
        /// <param name="Rn">���ڵ��ӵĴ���ֵ,һ��Ϊ��</param>
        /// <returns></returns>
        public static string ListTreeShow(DirectoryInfo theDir, int nLevel, string Rn)//�ݹ�Ŀ¼ �ļ�
        {
            DirectoryInfo[] subDirectories = theDir.GetDirectories();//���Ŀ¼
            foreach (DirectoryInfo dirinfo in subDirectories)
            {

                if (nLevel == 0)
                {
                    Rn += "��";
                }
                else
                {
                    string _s = "";
                    for (int i = 1; i <= nLevel; i++)
                    {
                        _s += "��&nbsp;";
                    }
                    Rn += _s + "��";
                }
                Rn += "<b>" + dirinfo.Name.ToString() + "</b><br />";
                FileInfo[] fileInfo = dirinfo.GetFiles();   //Ŀ¼�µ��ļ�
                foreach (FileInfo fInfo in fileInfo)
                {
                    if (nLevel == 0)
                    {
                        Rn += "��&nbsp;��";
                    }
                    else
                    {
                        string _f = "";
                        for (int i = 1; i <= nLevel; i++)
                        {
                            _f += "��&nbsp;";
                        }
                        Rn += _f + "��&nbsp;��";
                    }
                    Rn += fInfo.Name.ToString() + " <br />";
                }
                Rn = ListTreeShow(dirinfo, nLevel + 1, Rn);


            }
            return Rn;
        }



        /****************************************
         * �������ƣ�GetFoldAll(string Path)
         * ����˵������ȡָ���ļ�����������Ŀ¼���ļ�(��������)
         * ��    ����Path:��ϸ·��
         * ����ʾ�У�
         *            string strDirlist = Server.MapPath("templates");      
         *            this.Literal2.Text = DotNet.Utilities.FileOperate.GetFoldAll(strDirlist,"tpl","");
        *****************************************/
        /// <summary>
        /// ��ȡָ���ļ�����������Ŀ¼���ļ�(��������)
        /// </summary>
        /// <param name="Path">��ϸ·��</param>
        ///<param name="DropName">�����б�����</param>
        ///<param name="tplPath">Ĭ��ѡ��ģ������</param>
        public static string GetFoldAll(string Path, string DropName, string tplPath)
        {
            string strDrop = "<select name=\"" + DropName + "\" id=\"" + DropName + "\"><option value=\"\">--��ѡ����ϸģ��--</option>";
            string str = "";
            DirectoryInfo thisOne = new DirectoryInfo(Path);
            str = ListTreeShow(thisOne, 0, str, tplPath);
            return strDrop + str + "</select>";

        }

        /// <summary>
        /// ��ȡָ���ļ�����������Ŀ¼���ļ�����
        /// </summary>
        /// <param name="theDir">ָ��Ŀ¼</param>
        /// <param name="nLevel">Ĭ����ʼֵ,����ʱ,һ��Ϊ0</param>
        /// <param name="Rn">���ڵ��ӵĴ���ֵ,һ��Ϊ��</param>
        /// <param name="tplPath">Ĭ��ѡ��ģ������</param>
        /// <returns></returns>
        public static string ListTreeShow(DirectoryInfo theDir, int nLevel, string Rn, string tplPath)//�ݹ�Ŀ¼ �ļ�
        {
            DirectoryInfo[] subDirectories = theDir.GetDirectories();//���Ŀ¼

            foreach (DirectoryInfo dirinfo in subDirectories)
            {

                Rn += "<option value=\"" + dirinfo.Name.ToString() + "\"";
                if (tplPath.ToLower() == dirinfo.Name.ToString().ToLower())
                {
                    Rn += " selected ";
                }
                Rn += ">";

                if (nLevel == 0)
                {
                    Rn += "��";
                }
                else
                {
                    string _s = "";
                    for (int i = 1; i <= nLevel; i++)
                    {
                        _s += "��&nbsp;";
                    }
                    Rn += _s + "��";
                }
                Rn += "" + dirinfo.Name.ToString() + "</option>";


                FileInfo[] fileInfo = dirinfo.GetFiles();   //Ŀ¼�µ��ļ�
                foreach (FileInfo fInfo in fileInfo)
                {
                    Rn += "<option value=\"" + dirinfo.Name.ToString() + "/" + fInfo.Name.ToString() + "\"";
                    if (tplPath.ToLower() == fInfo.Name.ToString().ToLower())
                    {
                        Rn += " selected ";
                    }
                    Rn += ">";

                    if (nLevel == 0)
                    {
                        Rn += "��&nbsp;��";
                    }
                    else
                    {
                        string _f = "";
                        for (int i = 1; i <= nLevel; i++)
                        {
                            _f += "��&nbsp;";
                        }
                        Rn += _f + "��&nbsp;��";
                    }
                    Rn += fInfo.Name.ToString() + "</option>";
                }
                Rn = ListTreeShow(dirinfo, nLevel + 1, Rn, tplPath);


            }
            return Rn;
        }
        #endregion

        #region ��ȡ�ļ��д�С
        /****************************************
         * �������ƣ�GetDirectoryLength(string dirPath)
         * ����˵������ȡ�ļ��д�С
         * ��    ����dirPath:�ļ�����ϸ·��
         * ����ʾ�У�
         *           string Path = Server.MapPath("templates"); 
         *           Response.Write(DotNet.Utilities.FileOperate.GetDirectoryLength(Path));       
        *****************************************/
        /// <summary>
        /// ��ȡ�ļ��д�С
        /// </summary>
        /// <param name="dirPath">�ļ���·��</param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }
        #endregion

        #region ��ȡָ���ļ���ϸ����
        /****************************************
         * �������ƣ�GetFileAttibe(string filePath)
         * ����˵������ȡָ���ļ���ϸ����
         * ��    ����filePath:�ļ���ϸ·��
         * ����ʾ�У�
         *           string file = Server.MapPath("robots.txt");  
         *            Response.Write(DotNet.Utilities.FileOperate.GetFileAttibe(file));         
        *****************************************/
        /// <summary>
        /// ��ȡָ���ļ���ϸ����
        /// </summary>
        /// <param name="filePath">�ļ���ϸ·��</param>
        /// <returns></returns>
        public static string GetFileAttibe(string filePath)
        {
            string str = "";
            System.IO.FileInfo objFI = new System.IO.FileInfo(filePath);
            str += "��ϸ·��:" + objFI.FullName + "<br>�ļ�����:" + objFI.Name + "<br>�ļ�����:" + objFI.Length.ToString() + "�ֽ�<br>����ʱ��" + objFI.CreationTime.ToString() + "<br>������ʱ��:" + objFI.LastAccessTime.ToString() + "<br>�޸�ʱ��:" + objFI.LastWriteTime.ToString() + "<br>����Ŀ¼:" + objFI.DirectoryName + "<br>��չ��:" + objFI.Extension;
            return str;
        }
        #endregion
    }
}
