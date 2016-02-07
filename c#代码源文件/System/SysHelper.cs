/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Web;
using System.Threading;
using System.Diagnostics;

namespace DotNet.Utilities
{
    /// <summary>
    /// ϵͳ������صĹ�����
    /// </summary>    
    public static class SysHelper
    {
        #region ��ȡ�ļ����·��ӳ�������·��
        /// <summary>
        /// ��ȡ�ļ����·��ӳ�������·��
        /// </summary>
        /// <param name="virtualPath">�ļ������·��</param>        
        public static string GetPath(string virtualPath)
        {

            return HttpContext.Current.Server.MapPath(virtualPath);

        }
        #endregion

        #region ��ȡָ�����ò㼶�ķ�����
        /// <summary>
        /// ��ȡָ�����ò㼶�ķ�����
        /// </summary>
        /// <param name="level">���õĲ���</param>        
        public static string GetMethodName(int level)
        {
            //����һ����ջ����
            StackTrace trace = new StackTrace();

            //��ȡָ�����ò㼶�ķ�����
            return trace.GetFrame(level).GetMethod().Name;
        }
        #endregion

        #region ��ȡGUIDֵ
        /// <summary>
        /// ��ȡGUIDֵ
        /// </summary>
        public static string NewGUID
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        #endregion

        #region ��ȡ�����ַ�
        /// <summary>
        /// ��ȡ�����ַ�
        /// </summary>
        public static string NewLine
        {
            get
            {
                return Environment.NewLine;
            }
        }
        #endregion

        #region ��ȡ��ǰӦ�ó�����
        /// <summary>
        /// ��ȡ��ǰӦ�ó�����
        /// </summary>
        public static AppDomain CurrentAppDomain
        {
            get
            {
                return Thread.GetDomain();
            }
        }
        #endregion
    }
}
