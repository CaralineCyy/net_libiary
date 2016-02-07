/// <summary>
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System.Web;

namespace DotNet.Utilities
{
    /// <summary>
    /// Session ������
    /// 1��GetSession(string name)����session����ȡsession����
    /// 2��SetSession(string name, object val)����session
    /// </summary>
    public class SessionHelper
    {
        /// <summary>
        /// ����session����ȡsession����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }
        /// <summary>
        /// ����session
        /// </summary>
        /// <param name="name">session ��</param>
        /// <param name="val">session ֵ</param>
        public static void SetSession(string name, object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
        }

        /// <summary>
        /// ������е�Session
        /// </summary>
        /// <returns></returns>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// ɾ��һ��ָ����ession
        /// </summary>
        /// <param name="name">Session����</param>
        /// <returns></returns>
        public static void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>
        /// ɾ�����е�ession
        /// </summary>
        /// <returns></returns>
        public static void RemoveAllSession(string name)
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}
