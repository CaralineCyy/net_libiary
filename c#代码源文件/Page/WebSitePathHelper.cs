/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.Web;

namespace DotNet.Utilities
{
    /// <summary>
    /// ��վ·��������
    /// </summary>
    public static class WebSitePathHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public enum SortType
        {
            /// <summary>
            /// 
            /// </summary>
            Photo = 1,
            /// <summary>
            /// 
            /// </summary>
            Article = 5,
            /// <summary>
            /// 
            /// </summary>
            Diary = 7,
            /// <summary>
            /// 
            /// </summary>
            Pic = 2,
            /// <summary>
            /// 
            /// </summary>
            Music = 6,
            /// <summary>
            /// 
            /// </summary>
            AddressList = 4,
            /// <summary>
            /// 
            /// </summary>
            Favorite = 3,
        }
        #region ���ݸ�������Ե�ַ��ȡ��վ���Ե�ַ
        /// <summary>
        /// ���ݸ�������Ե�ַ��ȡ��վ���Ե�ַ
        /// </summary>
        /// <param name="localPath">��Ե�ַ</param>
        /// <returns>���Ե�ַ</returns>
        public static string GetWebPath(string localPath)
        {
            string path = HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            string thisLocalPath;
            //������Ǹ�Ŀ¼�ͼ���"/" ��Ŀ¼�Լ����"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            if (localPath.StartsWith("~/"))
            {
                thisLocalPath = localPath.Substring(2);
            }
            else
            {
                return localPath;
            }
            return thisPath + thisLocalPath;
        }

        #endregion

        #region ��ȡ��վ���Ե�ַ
        /// <summary>
        ///  ��ȡ��վ���Ե�ַ
        /// </summary>
        /// <returns></returns>
        public static string GetWebPath()
        {
            string path = System.Web.HttpContext.Current.Request.ApplicationPath;
            string thisPath;
            //������Ǹ�Ŀ¼�ͼ���"/" ��Ŀ¼�Լ����"/"
            if (path != "/")
            {
                thisPath = path + "/";
            }
            else
            {
                thisPath = path;
            }
            return thisPath;
        }
        #endregion

        #region �������·�������·����ȡ����·��
        /// <summary>
        /// �������·�������·����ȡ����·��
        /// </summary>
        /// <param name="localPath">���·�������·��</param>
        /// <returns>����·��</returns>
        public static string GetFilePath(string localPath)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(localPath, @"([A-Za-z]):\\([\S]*)"))
            {
                return localPath;
            }
            else
            {
                return System.Web.HttpContext.Current.Server.MapPath(localPath);
            }
        }
        #endregion
    }
}
