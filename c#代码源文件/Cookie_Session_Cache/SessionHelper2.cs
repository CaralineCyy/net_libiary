/// <summary>
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.Web;

namespace DotNet.Utilities
{
    public static class SessionHelper2
    {
        /// <summary>
        /// ���Session��������Ч��Ϊ20����
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        /// <param name="strValue">Sessionֵ</param>
        public static void Add(string strSessionName, string strValue)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = 20;
        }

        /// <summary>
        /// ���Session��������Ч��Ϊ20����
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        /// <param name="strValues">Sessionֵ����</param>
        public static void Adds(string strSessionName, string[] strValues)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = 20;
        }

        /// <summary>
        /// ���Session
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        /// <param name="strValue">Sessionֵ</param>
        /// <param name="iExpires">������Ч�ڣ����ӣ�</param>
        public static void Add(string strSessionName, string strValue, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// ���Session
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        /// <param name="strValues">Sessionֵ����</param>
        /// <param name="iExpires">������Ч�ڣ����ӣ�</param>
        public static void Adds(string strSessionName, string[] strValues, int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValues;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// ��ȡĳ��Session����ֵ
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        /// <returns>Session����ֵ</returns>
        public static string Get(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return HttpContext.Current.Session[strSessionName].ToString();
            }
        }

        /// <summary>
        /// ��ȡĳ��Session����ֵ����
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        /// <returns>Session����ֵ����</returns>
        public static string[] Gets(string strSessionName)
        {
            if (HttpContext.Current.Session[strSessionName] == null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[strSessionName];
            }
        }

        /// <summary>
        /// ɾ��ĳ��Session����
        /// </summary>
        /// <param name="strSessionName">Session��������</param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }
    }
}