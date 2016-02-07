/// <summary>
/// ��˵����Assistant
/// �� �� �ˣ��շ�
/// ��ϵ��ʽ��361983679  
/// ������վ��http://www.sufeinet.com/thread-655-1-1.html
/// </summary>
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.Utilities
{
    /// <summary>
    /// ҳ�泣�÷�����װ
    /// </summary>
    public class ShowMessageBox
    {
        #region ��Ϣ��ʾ

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMG(string message)
        {
            WriteScript("alert('" + message + "');");
        }


        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowMessage(string message)
        {
            ShowMessage("ϵͳ��ʾ", 180, 120, message);
        }


        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="message">��ʾ��Ϣ</param>
        public static void ShowMessage_link(string message, string linkurl)
        {
            ShowMessage_link("ϵͳ��ʾ", 180, 120, message, linkurl, 8000, -1);
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message">��ʾ��Ϣ</param>
        private static void ShowMessage(string title, int width, int height, string message)
        {
            ShowMessage(title, width, height, message, 3000, -1);
        }

        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        /// <param name="delayms"></param>
        /// <param name="leftSpace"></param>
        private static void ShowMessage(string title, int width, int height, string message, int delayms, int leftSpace)
        {
            WriteScript(string.Format("popMessage({0},{1},'{2}','{3}',{4},{5});", width, height, title, message, delayms, leftSpace == -1 ? "null" : leftSpace.ToString()));
        }


        /// <summary>
        /// ��ʾ��ʾ��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        /// <param name="delayms"></param>
        /// <param name="leftSpace"></param>
        private static void ShowMessage_link(string title, int width, int height, string message, string linkurl, int delayms, int leftSpace)
        {
            WriteScript(string.Format("popMessage2({0},{1},'{2}','{3}','{4}',{5},{6});", width, height, title, message, linkurl, delayms, leftSpace == -1 ? "null" : leftSpace.ToString()));
        }


        #endregion

        #region ��ʾ�쳣��Ϣ

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowExceptionMessage(Exception ex)
        {
            ShowExceptionMessage(ex.Message);
        }

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="message"></param>
        public static void ShowExceptionMessage(string message)
        {
            WriteScript("alert('" + message + "');");
            //PageHelper.ShowExceptionMessage("������ʾ", 210, 125, message);
        }

        /// <summary>
        /// ��ʾ�쳣��Ϣ
        /// </summary>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        private static void ShowExceptionMessage(string title, int width, int height, string message)
        {
            WriteScript(string.Format("setTimeout(\"showAlert('{0}',{1},{2},'{3}')\",100);", title, width, height, message));
        }
        #endregion

        #region ��ʾģ̬����

        /// <summary>
        /// ���ذ�ָ�����ӵ�ַ��ʾģ̬���ڵĽű�
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        public static string GetShowModalWindowScript(string wid, string title, int width, int height, string url)
        {
            return string.Format("setTimeout(\"showModalWindow('{0}','{1}',{2},{3},'{4}')\",100);", wid, title, width, height, url);
        }

        /// <summary>
        /// ��ָ�����ӵ�ַ��ʾģ̬����
        /// </summary>
        /// <param name="wid">����ID</param>
        /// <param name="title">����</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        /// <param name="url">���ӵ�ַ</param>
        public static void ShowModalWindow(string wid, string title, int width, int height, string url)
        {
            WriteScript(GetShowModalWindowScript(wid, title, width, height, url));
        }

        /// <summary>
        /// Ϊָ���ؼ���ǰ̨�ű�����ʾģ̬����
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        public static void ShowCilentModalWindow(string wid, WebControl control, string eventName, string title, int width, int height, string url, bool isScriptEnd)
        {
            string script = isScriptEnd ? "return false;" : "";
            control.Attributes[eventName] = string.Format("showModalWindow('{0}','{1}',{2},{3},'{4}');" + script, wid, title, width, height, url);
        }

        /// <summary>
        /// Ϊָ���ؼ���ǰ̨�ű�����ʾģ̬����
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="eventName"></param>
        /// <param name="wid"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="url"></param>
        /// <param name="isScriptEnd"></param>
        public static void ShowCilentModalWindow(string wid, TableCell cell, string eventName, string title, int width, int height, string url, bool isScriptEnd)
        {
            string script = isScriptEnd ? "return false;" : "";
            cell.Attributes[eventName] = string.Format("showModalWindow('{0}','{1}',{2},{3},'{4}');" + script, wid, title, width, height, url);
        }
        #endregion

        #region ��ʾ�ͻ���ȷ�ϴ���
        /// <summary>
        /// ��ʾ�ͻ���ȷ�ϴ���
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(WebControl control, string eventName, string message)
        {
            ShowCilentConfirm(control, eventName, "ϵͳ��ʾ", 210, 125, message);
        }

        /// <summary>
        /// ��ʾ�ͻ���ȷ�ϴ���
        /// </summary>
        /// <param name="control"></param>
        /// <param name="eventName"></param>
        /// <param name="title"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="message"></param>
        public static void ShowCilentConfirm(WebControl control, string eventName, string title, int width, int height, string message)
        {
            control.Attributes[eventName] = string.Format("return showConfirm('{0}',{1},{2},'{3}','{4}');", title, width, height, message, control.ClientID);
        }


        #endregion

        /// <summary>
        /// дjavascript�ű�
        /// </summary>
        /// <param name="script">�ű�����</param>
        public static void WriteScript(string script)
        {
            Page page = GetCurrentPage();

            // NDGridViewScriptFirst(page.Form.Controls, page);

            page.ClientScript.RegisterStartupScript(page.GetType(), System.Guid.NewGuid().ToString(), script, true);

        }

        /// <summary>
        /// �õ���ǰҳ����ʵ��
        /// </summary>
        /// <returns></returns>
        public static Page GetCurrentPage()
        {
            return (Page)HttpContext.Current.Handler;
        }


    }
}
